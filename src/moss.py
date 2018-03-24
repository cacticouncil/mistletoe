import socket
import string
import os

class Client:
    def __init__(self):
        self.server = "moss.stanford.edu"
        self.port = 7690
        self.userId = 12345
        self.language = "c"
        self.maxMatchesPerPassage = 10
        self.isComparingDirectories = 0
        self.isUsingExperimentalServer = 0
        self.comment = ""
        self.maxResults = 65536
        self.isConnected = False

    def __del__(self):
        if self.isConnected:
            try:
                self.Shutdown()
            except Exception:
                None

    def RecvLine(self):
        """ Receives a single line of text from the server. 
        Returns:
            Single line of text sent by the server.
        Raises:
            Exception: Error occurred when receiving data.
        """
        messageFromServer = b""

        while True:
            recvBuff = self.sock.recv(1)
            if len(recvBuff) == 0 or recvBuff.decode('utf-8') == '\n':
                break
            messageFromServer += recvBuff

        return messageFromServer.decode("utf-8")

    def SendAll(self, contents):
        """ Sends contents to the server
        Raises:
            Exception: Error occurred when sending data.
        """
        self.sock.sendall(bytes(contents, 'UTF-8', errors='replace'))
        

    def Shutdown(self):
        """ Closes our connection to the server """
        if not self.isConnected:
            return
        self.isConnected = False
        self.sock.shutdown(socket.SHUT_RDWR)
        self.sock.close()

    def Output(self, message):
        """ Processes any output messages. Meant to be overridden by library user.
        Args:
            message: Output message
        """
        None

    def OnWarning(self, message):
        """ Processes any output messages. Meant to be overridden by library user.
        Args:
            message: Output message
        """
        None

    def OnSuccess(self, result):
        """ Called when we successfully use the MOSS system. Meant to be overridden by library user.
        Args:
            result: Link to our results stored on the MOSS server
        """
        None

    def OnFailure(self, message):
        """ Called when we fail to successfully use the MOSS system. Meant to be overridden by library user.
        Args:
            message: Failure message
        """
        None

    def MossUploadFile(self, filePath, fileIndex):
        """ Uploads a file to the MOSS server
        Args:
            filePath: Full path to the text file being uploaded.
            fileIndex: Index of student file being uploaded (starting from 1) or 0 if it's a base file.
        Raises:
            Exception: Failed to read file and send contents to server.
        """
        fileContents = None

        try:
            fileHandle = open(filePath, 'r', encoding="utf-8")
            print(os.fstat(fileHandle.fileno()).st_size) #TO-DO: Implement this as a check for file size (this gives file_size in bytes)
            fileContents = fileHandle.read()
            fileHandle.close()
        except Exception as ex:
            raise RuntimeWarning('Error loading file ' + filePath)

        if fileContents == None:
            fileSize = 0
        else:
            fileSize = len(fileContents)

        self.SendAll("file {} {} {} {}\n".format(fileIndex, self.language, fileSize, filePath.replace(" ", "_")))
        self.SendAll(fileContents)

    def MossSendHeader(self):
        """ Sends all required headers to MOSS server. MossConfirmLanguage
        Raises:
            Exception: Failed to send all headers to server.
        """
        self.SendAll("moss {}\n".format(self.userId))
        self.SendAll("directory {}\n".format(self.isComparingDirectories))
        self.SendAll("X {}\n".format(self.isUsingExperimentalServer))
        self.SendAll("maxmatches {}\n".format(self.maxMatchesPerPassage))
        self.SendAll("show {}\n".format(self.maxResults))

    def MossConfirmLanguage(self):
        """ Confirms that our currently selected language is supported by the MOSS server.
        Returns:
            True if language is supported by the MOSS server.
        Raises:
            Exception: Failed to send request to server or receive response from server.
        """
        self.SendAll("language {}\n".format(self.language))
        languageExists = self.RecvLine()
        print('Result:', languageExists)
        return languageExists == "yes"

    def MossSubmit(self):
        """ Tells the MOSS server we're done uploading files to it. Server will send us a single line containing the results URL when it's done.
        Raises:
            Exception: Failed to send 'query' request to server.        
        """
        self.SendAll("query 0 {}\n".format(self.comment))

    def MossEnd(self):
        """ Tells the MOSS server we're done communicating with it. Must be the last message sent to the server. 
        Raises:
            Exception: Failed to send 'end' request to server.
        """
        self.SendAll("end\n")

    def Run(self, studentFiles, baseFiles):
        """ Connects to the MOSS server and waits for the server to compute the results.
        Args:
            studentFiles: List of paths to student files to be sent to the server.
            baseFiles: List of paths to base files to be sent to the server.
        """
        exceptioninfo = None
        if len(studentFiles) == 0:
            self.OnFailure("Missing student files.")
            return

        try:
            ###########################################
            # Establish connection
            ###########################################
            self.sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            self.sock.connect((self.server, self.port))

            ###########################################
            # Initialize MOSS session
            ###########################################
            self.MossSendHeader()
            #self.Output("Verifying server supports language '{}'...".format(self.language))
            #if not self.MossConfirmLanguage():
            #    self.OnFailure("Language {} not supported".format(self.language))
            #    self.MossEnd()
            #    self.Shutdown()
            #    return

            ###########################################
            # Upload our files
            ###########################################
            # TO-DO: Potentially chunk this into bits to not overload server
            numBaseFiles = len(baseFiles)
            self.Output("Uploading base files...")
            for i in range (0, numBaseFiles):
                self.Output("Uploading '{}'".format(baseFiles[i]))
                try:
                    self.MossUploadFile(baseFiles[i], 0)
                except Warning as warning:
                    self.OnWarning("WARNING: {}".format(str(warning)))

            numStudentFiles = len(studentFiles)
            self.Output("Uploading student files...")
            for i in range (0, numStudentFiles):
                self.Output("Uploading '{}'".format(studentFiles[i]))
                try:
                    self.MossUploadFile(studentFiles[i], i + 1)
                except Warning as warning:
                    self.OnWarning("WARNING: {}".format(str(warning)))

            ###########################################
            # Request results
            ###########################################
            self.MossSubmit()

            self.Output("Query submitted. Waiting for the server's response.")
            resultUrl = self.RecvLine()
            self.Output("OK")
            self.MossEnd()
            self.Shutdown()
            if len(resultUrl) == 0:
                self.OnFailure("Server failed return a result.")
                return
            else:
                self.OnSuccess(resultUrl)
                return resultUrl

        except socket.timeout as ex:
            self.OnFailure("Timed out connecting to server: {}".format(str(ex)))
            return
        except OSError as ex:
            self.OnFailure("OS error: {} [{}]".format(str(ex), ex.errno))
            return
        except Exception as ex:
            self.OnFailure("Unknown exception: {}".format(str(ex)))

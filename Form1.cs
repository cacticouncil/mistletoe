using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Text;
using System.Threading;
using System.ComponentModel;
using ICSharpCode.SharpZipLib.Zip;

namespace WinMoss
{
    public partial class WinMoss : Form
    {
        private string strTempPath = "";
        private string strLastDir = "";
        private int MaxPathLength;
        public WinMoss()
        {
            InitializeComponent();
            Program.theDoc.MossScriptLocation = Properties.Settings.Default.strMossScriptLocation;
            Program.theDoc.PerlLocation = Properties.Settings.Default.strPerlLocation;
            Program.theDoc.Language = Properties.Settings.Default.strLanguage;
            Program.theDoc.Comment = Properties.Settings.Default.strComment;
            Program.theDoc.IgnoreCount = Properties.Settings.Default.numIgnoreCount;
            strLastDir = Properties.Settings.Default.strLastDir;
            pgMoss.SelectedObject = Program.theDoc;
            addFilter.Text = Document.strarrLanguagesFilter[Convert.ToInt32(Enum.Parse(typeof(Document.LanguagesEnum), Program.theDoc.Language))] + " *.zip";
            ignFilter.Text = Properties.Settings.Default.strIgnoreFilter;
            strTempPath = Path.Combine(Path.GetTempPath(), "WinMossTemp");
            cbtnDirectories.Checked = Properties.Settings.Default.chkboxDirs;

            // If there is no Moss file location provided, assume we should use the
            // one created by the installer, located in the same directory as the exe
            if (0 == Program.theDoc.MossScriptLocation.Length)
            {
                Program.theDoc.MossScriptLocation = Directory.GetCurrentDirectory() + "\\mosswin.pl";
            }

            // Thank you Internet: reflection
            var maxPathField = typeof(Path).GetField("MaxPath",
                System.Reflection.BindingFlags.Static |
                System.Reflection.BindingFlags.GetField |
                System.Reflection.BindingFlags.NonPublic);
            // invoke the field gettor, which returns 260
            MaxPathLength = (int)maxPathField.GetValue(null);
        }

        private bool bClosing = false;
        private bool bDeleteAttemptFailed = false;
        private void WinMoss_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if we are in the process of deleting temp files and closing
            //don't let them be impatient bastards and try to close again
            if (this.bClosing)
            {
                e.Cancel = true;
                return;
            }


            Properties.Settings.Default.strLastDir = strLastDir;
            Properties.Settings.Default.strMossScriptLocation = Program.theDoc.MossScriptLocation;
            Properties.Settings.Default.strPerlLocation = Program.theDoc.PerlLocation;
            Properties.Settings.Default.strLanguage = Program.theDoc.Language;
            Properties.Settings.Default.strComment = Program.theDoc.Comment;
            Properties.Settings.Default.numIgnoreCount = Program.theDoc.IgnoreCount;
            Properties.Settings.Default.strIgnoreFilter = ignFilter.Text;
            Properties.Settings.Default.chkboxDirs = cbtnDirectories.Checked;
            Properties.Settings.Default.Save();
            closeWaitHandle.WaitOne(-1);

            if (Directory.Exists(strTempPath) && !this.bDeleteAttemptFailed)
            {
                //open a new worker thread to delete our temp files, as this can lag the ui significantly
                //if a large amount of files were processed
                this.bClosing = true;
                BackgroundWorker deleteWorker = new BackgroundWorker();
                deleteWorker.WorkerSupportsCancellation = true;
                deleteWorker.WorkerReportsProgress = true;
                deleteWorker.DoWork += new DoWorkEventHandler(deleteWork);
                deleteWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(deleteWorkComplete);
                deleteWorker.RunWorkerAsync();

                //we will tell the program to close again once we are done
                this.statusLabel.Text = "Deleting temporary files, please wait...";
                e.Cancel = true;
            }


        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void rtxtOutput_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void Files_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = (e.Data.GetDataPresent(DataFormats.FileDrop) == true) ? DragDropEffects.Move : DragDropEffects.None;
        }


        private void Files_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) == true)
            {
                // Remove the instructions
                if (sender == lbStudentFiles)
                    studentInstructions.Text = "";
                else if (sender == lbBaseFiles)
                    baseInstructions.Text = "";

                string[] strarrDropData;
                string[] strarrFiles;

                strarrDropData = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string strDropData in strarrDropData)
                {
                    if (Directory.Exists(strDropData))
                    {
                        // when directory flag is on, add filtered temp directory to listing
                        if (cbtnDirectories.Checked == true && sender == lbStudentFiles)
                        {
                            AddFilteredDirectory(strDropData);
                        }
                        // otherwise add individual files
                        else
                        {
                            strarrFiles = MultipleFileFilter(strDropData, SearchOption.AllDirectories);
                            foreach (string strPath in strarrFiles)
                            {
                                //unzip any archives
                                if (".zip" == Path.GetExtension(strPath))
                                    AddZip(sender, strPath);
                                else
                                    AddCodeFile(sender, strPath);
                            }
                        }
                    }
                    else if (Path.HasExtension(strDropData))
                    {
                        string extensions = addFilter.Text.Replace("*", "").ToLower();
                        if (".zip" == Path.GetExtension(strDropData).ToLower())
                            AddZip(sender, strDropData);
                        else if (extensions.Contains(Path.GetExtension(strDropData).ToLower()))
                        {
                            string[] badFilter = ignFilter.Text.Split(new char[] { ' ' });
                            bool failsauce = false;

                            foreach (string filter in badFilter)
                            {
                                if (strDropData.ToLower().Contains(filter.ToLower()))
                                {
                                    failsauce = true;
                                    break;
                                }
                            }
                            if (!failsauce)
                                AddCodeFile(sender, strDropData);
                        }
                    }
                }


                //if we told it to autorun, and there aren't any zips awaiting decompression
                if (this.cbtnAutoRun.Checked == true && this.nCurrentZip == this.nTotalZips)
                {
                    AutoRunDialog ard = new AutoRunDialog();
                    ard.FormClosed += new FormClosedEventHandler(ard_FormClosed);
                    ard.Show(this);
                }
            }

        }

        private bool AddFilteredDirectory(string strDropData)
        {
            // dropdata = "Z:\\FullSail\\WINMOSS\\mossdata\\Fischer_Ian_Lab6"
            // path = "C:\\Users\\Administrator\\AppData\\Local\\Temp\\WinMossTemp\\FullSail\\WINMOSS\\mossdata\\Fischer_Ian_Lab6"
            string strFolder = Path.GetFileName(strDropData).Replace(' ', '_');
            string strTargetDir = Path.Combine(strTempPath, strFolder);

            if (lbStudentFiles.Items.Contains(strTargetDir) == false)
            {
                // create dir
                Directory.CreateDirectory(strTargetDir);

                string[] strarrFiles = MultipleFileFilter(strDropData, SearchOption.AllDirectories);

                // make sure there are files in the directory
                if (strarrFiles.Length == 0)
                    return false;

                foreach (string strPath in strarrFiles)
                {
                    // change file name to droppath\nestedºnestedºfilename.ext

                    // remove root path
                    string newPath = strPath.Replace(strDropData + "\\", "");

                    // remove spaces from filenames
                    newPath = newPath.Replace(' ', '_');

                    // replace nested directory separators with º and attach temp directory
                    newPath = Path.Combine(strTargetDir, newPath.Replace("\\", "º"));

                    // copy files
                    SmarterCopy(strPath, newPath);
                }

                // add base item to list
                lbStudentFiles.Items.Add("/" + strFolder + "/*.*");
            }

            return true;
        }


        private bool AddCodeFile(object sender, string filename)
        {
            string strFile;
            string strFileNoSpace;
            string strFileNoExt;
            string strDirSub;
            string strDirSubNoSpace;
            string strPathNoSpace;
            string strPathNoSpaceNoTemp;

            strFile = Path.GetFileName(filename);
            strFileNoSpace = strFile.Replace(' ', '_');
            strFileNoExt = Path.GetFileNameWithoutExtension(filename);
            strDirSub = Path.GetDirectoryName(filename);
            strDirSubNoSpace = strDirSub.Replace(' ', '_');
            strPathNoSpace = Path.Combine(strDirSubNoSpace, strFileNoSpace);

            CreateTempDirectoryForFile(strDirSubNoSpace);

            strPathNoSpace = strPathNoSpace.Replace(Path.GetPathRoot(strPathNoSpace), "");
            strPathNoSpace = Path.Combine(strTempPath, strPathNoSpace);
            strPathNoSpaceNoTemp = strPathNoSpace.Replace(strTempPath, "");
            SmarterCopy(filename, strPathNoSpace);

            if ((sender == lbStudentFiles || sender == btnAddStudentFiles) && lbStudentFiles.Items.Contains(strPathNoSpaceNoTemp) == false)
            {
                lbStudentFiles.Items.Add(strPathNoSpaceNoTemp);
            }
            else if ((sender == lbBaseFiles || sender == btnAddBaseFiles) && lbBaseFiles.Items.Contains(strPathNoSpaceNoTemp) == false)
            {
                lbBaseFiles.Items.Add(strPathNoSpaceNoTemp);
            }

            return true;
        }

        private void pgMoss_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "Language")
            {
                addFilter.Text = Document.strarrLanguagesFilter[Convert.ToInt32(Enum.Parse(typeof(Document.LanguagesEnum), Program.theDoc.Language))] + " *.zip";
            }
        }

        /// <summary>
        /// Creates the directory based off of the file path name and working directory
        /// </summary>
        /// <param name="strDirSubNoSpace">The directory portion of the file to used to create the temp path, 
        /// must contain no spaces</param>
        /// <returns>Returns the resulting path</returns>
        private string CreateTempDirectoryForFile(string strDirSubNoSpace)
        {
            strDirSubNoSpace = strDirSubNoSpace.Replace(Path.GetPathRoot(strDirSubNoSpace), "");
            strDirSubNoSpace = Path.Combine(strTempPath, strDirSubNoSpace);
            Directory.CreateDirectory(strDirSubNoSpace);
            return strDirSubNoSpace;
        }

        /// <summary>
        /// A helper function for the Directory command GetFiles, allows the use of multiple
        /// format arguments. Arguments must be seperated by a space.
        /// </summary>
        /// <param name="rootPath">The directory to search</param>
        /// <param name="searchOptions">One of the System.IO.SearchOption values that specifies whether the search
        ///     operation should include all subdirectories or only the current directory.</param>
        /// <returns>A String array containing the names of files in the specified directory that
        ///     match the specified search pattern. File names include the full path.</returns>
        private string[] MultipleFileFilter(string rootPath, SearchOption searchOptions)
        {
            // Split our extensions into multiple strings
            string[] extFilter = addFilter.Text.Split(new char[] { ' ' });
            string[] badFilter = ignFilter.Text.Split(new char[] { ' ' });

            //ArrayList to hold the file names
            ArrayList strings = new ArrayList();

            //loop through each extension in the filter
            foreach (string extension in extFilter)
            {
                //add all the files names that match our valid extensions
                //by using AddRange of the ArrayList
                strings.AddRange(Directory.GetFiles(rootPath, extension, searchOptions));
            }

            // remove anything that contains one of the ignore words
            ArrayList ignores = new ArrayList();
            foreach (string ign in badFilter)
            {
                if (ign != "")
                    foreach (string str in strings)
                    {
                        if (str.ToLower().Contains(ign.ToLower()))
                            ignores.Add(str);
                    }
            }
            foreach (string ign in ignores) strings.Remove(ign);

            // Convert to a standard string array
            string[] retStrings = new string[strings.Count];
            for (int i = 0; i < strings.Count; i++)
            {
                retStrings[i] = (string)strings[i];
            }
            return retStrings;
        }

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd;
            DialogResult retvalSD;
            string strDirRoot;

            // Remove the instructions
            if (sender == btnAddStudentFiles)
                studentInstructions.Text = "";
            else if (sender == btnAddBaseFiles)
                baseInstructions.Text = "";

            fbd = new FolderBrowserDialog();
            fbd.Description = "Select a directory containing code files to be submitted to Moss:";
            fbd.ShowNewFolderButton = false;
            if (String.IsNullOrEmpty(strLastDir) == false)
            {
                fbd.SelectedPath = strLastDir;
            }
            retvalSD = fbd.ShowDialog();
            if (retvalSD == DialogResult.OK)
            {
                strLastDir = fbd.SelectedPath;
                strDirRoot = fbd.SelectedPath;

                if (cbtnDirectories.Checked == true && sender == btnAddStudentFiles)
                {
                    AddFilteredDirectory(strDirRoot);
                }
                else
                {
                    string[] strarrFiles;
                    strarrFiles = MultipleFileFilter(strDirRoot, SearchOption.AllDirectories);
                    foreach (string strPath in strarrFiles)
                    {
                        //unzip any archives
                        if (".zip" == Path.GetExtension(strPath))
                            AddZip(sender, strPath);
                        else
                            AddCodeFile(sender, strPath);
                    }
                }
            }
        }

        private void btnRemoveFiles_Click(object sender, EventArgs e)
        {
            while (lbStudentFiles.SelectedItems.Count > 0)
            {
                lbStudentFiles.Items.Remove(lbStudentFiles.SelectedItems[0]);
            }
        }

        private void btnSaveQuery_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveOutput_Click(object sender, EventArgs e)
        {

        }

        private void btnClearQuery_Click(object sender, EventArgs e)
        {
            rtxtOutput.Clear();
            lbBaseFiles.Items.Clear();
            lbStudentFiles.Items.Clear();
            studentInstructions.Text = "Drag Student Files Here";
            baseInstructions.Text = "Drag Base Files Here";

            if (Directory.Exists(strTempPath))
            {
                Directory.Delete(strTempPath, true);
            }
        }

        private void btnClearOutput_Click(object sender, EventArgs e)
        {
            rtxtOutput.Clear();
        }


        private void btnClearStudentFiles_Click(object sender, EventArgs e)
        {
            if (cbtnDirectories.Checked == true)
            {
                foreach (string file in lbStudentFiles.Items)
                {
                    string dir = Path.GetDirectoryName(strTempPath + file);
                    if (Directory.Exists(dir))
                        Directory.Delete(dir, true);
                }
            }

            lbStudentFiles.Items.Clear();
            studentInstructions.Text = "Drag Student Files Here";
        }

        private void btnClearBaseFiles_Click(object sender, EventArgs e)
        {
            baseInstructions.Text = "Drag Base Files Here";
            lbBaseFiles.Items.Clear();
        }

        private bool SmarterCopy(string pathSrc, string pathDest)
        {
            if (pathSrc.Length > MaxPathLength || pathDest.Length > MaxPathLength)
            {
                rtxtOutput.SelectionColor = rtxtOutput.ForeColor;
                rtxtOutput.AppendText("Skipping overlong source filename \""
                    + pathSrc + "\"..." + Environment.NewLine);
                return false;
            }
            SmartCopy(pathSrc, pathDest);
            return true;
        }

        public static void SmartCopy(string pathSrc, string pathDest)
        {
            Encoding encSrc = GetFileEncoding(pathSrc);

            if (encSrc == Encoding.Default)
            {
                File.Copy(pathSrc, pathDest, true);
            }
            else
            {
                using (TextReader input = new StreamReader(new FileStream(pathSrc, FileMode.Open), encSrc))
                {
                    using (TextWriter output = new StreamWriter(new FileStream(pathDest, FileMode.Create), Encoding.UTF8))
                    {
                        // Create the buffer
                        char[] buffer = new char[8096];
                        int len;

                        // Repeatedly copy data until we've finished
                        while ((len = input.Read(buffer, 0, 8096)) > 0)
                        {
                            output.Write(buffer, 0, len);
                        }
                    }
                }
            }
        }
        // Return the Encoding of a text file.  Return Encoding.Default if no Unicode
        // BOM (byte order mark) is found.
        public static Encoding GetFileEncoding(String FileName)
        {
            Encoding Result = null;
            FileInfo FI = new FileInfo(FileName);
            FileStream FS = null;

            try
            {
                FS = FI.OpenRead();

                Encoding[] UnicodeEncodings = { Encoding.BigEndianUnicode, Encoding.Unicode, Encoding.UTF8 };

                for (int i = 0; Result == null && i < UnicodeEncodings.Length; i++)
                {
                    FS.Position = 0;

                    byte[] Preamble = UnicodeEncodings[i].GetPreamble();

                    bool PreamblesAreEqual = true;

                    for (int j = 0; PreamblesAreEqual && j < Preamble.Length; j++)
                    {
                        PreamblesAreEqual = Preamble[j] == FS.ReadByte();
                    }

                    if (PreamblesAreEqual)
                    {
                        Result = UnicodeEncodings[i];
                    }
                }
            }
            catch (System.IO.IOException)
            {
            }
            finally
            {
                if (FS != null)
                {
                    FS.Close();
                }
            }
            if (Result == null)
            {
                Result = Encoding.Default;
            }
            return Result;
        }

        /// <summary>
        /// Experimental threading code which should stop the GUI from freezing after submitting a query.
        /// </summary>
        private StringBuilder sbStdout;
        private StringBuilder sbStderr;
        private Object lockStdout;
        private Object lockStderr;
        private ThreadStart threadstartScript;
        private Thread threadScript;
        private System.Windows.Forms.Timer timerUpdateOutput;

        public void ScriptThread()
        {
            Process moss;
            moss = new Process();
            moss.StartInfo.WorkingDirectory = strTempPath;
            moss.StartInfo.CreateNoWindow = true;
            if (String.IsNullOrEmpty(Program.theDoc.PerlLocation) == true)
            {
                return;
            }
            if (!File.Exists(Program.theDoc.PerlLocation))
            {
                lock (lockStderr)
                {
                    sbStderr.Append("Can't find Perl - please update the 'PerlLocation' Path"
                        + Environment.NewLine);
                }
                return;
            }
            moss.StartInfo.FileName = "\"" + Program.theDoc.PerlLocation + "\"";
            if (String.IsNullOrEmpty(Program.theDoc.MossScriptLocation) == true)
            {
                return;
            }
            moss.StartInfo.Arguments = "\"" + Program.theDoc.MossScriptLocation + "\"";
            if (String.IsNullOrEmpty(Program.theDoc.Language) == true)
            {
                return;
            }

            // Write this to the file instead of passing it as arguments
            string strFile = "-l#" + Document.strarrLanguagesCmdLine[Convert.ToInt32(Enum.Parse(typeof(Document.LanguagesEnum), Program.theDoc.Language))];
            strFile += "#-m#" + Program.theDoc.IgnoreCount;
            if (String.IsNullOrEmpty(Program.theDoc.Comment) == false)
            {
                strFile += "#-c#\"" + Program.theDoc.Comment + "\"";
            }
            strFile += "#-n#" + (lbStudentFiles.Items.Count * (lbStudentFiles.Items.Count - 1) / 2);
            if (cbtnDirectories.Checked == true)
                strFile += "#-d";
            for (int i = 0; i < lbBaseFiles.Items.Count; i++)
            {
                strFile += "#-b#." + lbBaseFiles.Items[i];
            }
            for (int i = 0; i < lbStudentFiles.Items.Count; i++)
            {
                strFile += "#." + lbStudentFiles.Items[i];
            }

            FileInfo newFile = new FileInfo(moss.StartInfo.WorkingDirectory + "\\Arguments.txt");
            StreamWriter writer = new StreamWriter(newFile.Create(), Encoding.ASCII);
            writer.Write(strFile);
            writer.Close();

            moss.StartInfo.UseShellExecute = false;
            moss.StartInfo.RedirectStandardOutput = true;
            moss.StartInfo.RedirectStandardError = true;
            moss.OutputDataReceived += new DataReceivedEventHandler(OnStdout);
            moss.ErrorDataReceived += new DataReceivedEventHandler(OnStderr);
            moss.Start();
            moss.BeginOutputReadLine();
            moss.BeginErrorReadLine();
            moss.WaitForExit();
            moss.Close();
        }

        public void OnStdout(object sender, DataReceivedEventArgs e)
        {
            if (String.IsNullOrEmpty(e.Data) == false)
            {
                lock (lockStdout)
                {
                    sbStdout.Append(e.Data + Environment.NewLine);
                }
            }
        }
        public void OnStderr(object sender, DataReceivedEventArgs e)
        {
            if (String.IsNullOrEmpty(e.Data) == false)
            {
                lock (lockStderr)
                {
                    sbStderr.Append(e.Data + Environment.NewLine);
                }
            }
            if (null != e.Data && e.Data.Contains("167"))
            {
                //updateMossScriptToolStripMenuItem_Click(sender, e);
                lock (lockStderr)
                {
                    sbStderr.Append(
                        "Use the 'Update User ID' option under the 'Moss Script' file menu for help with this error"
                        + Environment.NewLine);
                }
            }
        }

        private void btnRunMossEx_Click(object sender, EventArgs e)
        {

            if (lbStudentFiles.Items.Count < 2)
            {
                rtxtOutput.SelectionColor = Color.Red;
                rtxtOutput.AppendText("Error: Must have more than 1 file in the student data listing to upload." + Environment.NewLine);
                return;
            }

            sbStdout = new StringBuilder();
            sbStderr = new StringBuilder();
            lockStderr = new Object();
            lockStdout = new Object();
            threadstartScript = new ThreadStart(ScriptThread);
            threadScript = new Thread(threadstartScript);
            threadScript.Start();
            timerUpdateOutput = new System.Windows.Forms.Timer();
            timerUpdateOutput.Interval = 100;
            timerUpdateOutput.Tick += new EventHandler(timerUpdateOutput_Tick);
            timerUpdateOutput.Start();
            btnRunMoss.Enabled = false;
        }

        void timerUpdateOutput_Tick(object sender, EventArgs e)
        {
            lock (lockStdout)
            {
                if (String.IsNullOrEmpty(sbStdout.ToString()) == false)
                {
                    rtxtOutput.SelectionColor = Color.Black;
                    rtxtOutput.AppendText(sbStdout.ToString());
                    rtxtOutput.ScrollToCaret();
                    sbStdout.Length = 0;
                }
            }
            lock (lockStderr)
            {
                if (String.IsNullOrEmpty(sbStderr.ToString()) == false)
                {
                    rtxtOutput.SelectionColor = Color.Red;
                    rtxtOutput.AppendText(sbStderr.ToString());
                    rtxtOutput.ScrollToCaret();
                    sbStderr.Length = 0;
                }
            }
            if (threadScript.ThreadState == System.Threading.ThreadState.Stopped)
            {
                timerUpdateOutput.Stop();
                btnRunMoss.Enabled = true;
                return;
            }
        }

        private void updateMossScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateUserID userIDdia = new UpdateUserID(Program.theDoc.MossScriptLocation);
            userIDdia.Show();
        }

        private void cbtnDirectories_Changed(object sender, EventArgs e)
        {
            btnClearQuery_Click(sender, e);
        }

        private void studentInstructionsDragEnter(object sender, DragEventArgs e)
        {
            studentInstructions.Text = "";
        }

        private void baseInstructionsDragEnter(object sender, DragEventArgs e)
        {
            baseInstructions.Text = "";
        }


        #region ZIP_STUFF
        //zip stuff
        private AutoResetEvent zipWaitHandle = new AutoResetEvent(true);
        private AutoResetEvent closeWaitHandle = new AutoResetEvent(true);
        private int nTotalZips = 0;
        private int nCurrentZip = 0;
        private void AddZip(object sender, string filename)
        {
            this.nTotalZips++;

            string directoryName = strTempPath + "\\unzip\\" + Path.GetFileNameWithoutExtension(filename);

            //if the directory exists, try it with some appended numbers until it's unique
            for (int i = 0; Directory.Exists(directoryName); i++)
            {
                directoryName += i.ToString();
            }

            //append the title of our folder to the strTempPath path and create a new folder there
            Directory.CreateDirectory(directoryName);


            //pack all of this info into an UnzipInfo
            UnzipInfo uzi = new UnzipInfo(filename, directoryName, sender);

            //make a new BackgroundWorker thread to do the unzipping, in case our file is very big
            //or the very likely situation that our computer is very slowwww and we don't want our ui to hang
            BackgroundWorker unzipWorker = new BackgroundWorker();
            unzipWorker.WorkerSupportsCancellation = true;
            unzipWorker.WorkerReportsProgress = true;
            unzipWorker.DoWork += new DoWorkEventHandler(unzipWork);
            unzipWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(unzipWorkComplete);
            unzipWorker.RunWorkerAsync(uzi);

            if (this.nCurrentZip == 0)
                this.statusLabel.Text = (this.nCurrentZip + 1) + "/" + this.nTotalZips + " archives decompressed.";
            this.cbtnDirectories.Enabled = false;
            this.btnClearQuery.Enabled = false;

        }

        private void unzipFolder(ZipInputStream zifs, String outDirectory)
        {
            ZipEntry zEntry = zifs.GetNextEntry();
            Directory.CreateDirectory(outDirectory);

            //our buffer size and buffer itself, used to read chunks
            //of info from the archive at a time
            int bufferSize = 1024;
            byte[] dataBuffer = new byte[bufferSize];
            int numBytesRead = 0;

            //continue while entries are still valid
            while (null != zEntry)
            {
                //it will give us all files throughout the entire folder hierarchy
                //so there is no need for any recursive action on our part, we just
                //need to see if the current entry is a directory...if it is then we
                //create it, and subsequent entries will point to files inside of it
                //
                //IMPORTANT!...this MAY NOT give you a directory before giving you a
                //file that is contained within that directory...so we have to cover
                //our ass in both cases...don't recreate directories if they already
                //exist...and if the directory our file is in doesn't exist yet then
                //make it.
                if (zEntry.IsDirectory)
                {
                    if (!Directory.Exists(Path.Combine(outDirectory, zEntry.Name)))
                        Directory.CreateDirectory(Path.Combine(outDirectory, zEntry.Name));
                }
                else
                {
                    if (!Directory.Exists(Path.Combine(outDirectory, Path.GetDirectoryName(zEntry.Name))))
                        Directory.CreateDirectory(Path.Combine(outDirectory, Path.GetDirectoryName(zEntry.Name)));

                    FileStream ofs = File.Create(Path.Combine(outDirectory, zEntry.Name));

                    while (true)
                    {
                        //decompress and read a chunk of bytes up to bufferSize in length
                        //Read returns the number of bytes it actually read
                        numBytesRead = zifs.Read(dataBuffer, 0, bufferSize);

                        //if we read anything, write that much to the output file
                        if (numBytesRead > 0)
                            ofs.Write(dataBuffer, 0, numBytesRead);
                        else
                            break;

                        //relinquish the rest of our time slice
                        Thread.Sleep(0);
                    }

                    //close our newly created files
                    ofs.Close();
                }

                //move on to the next entry
                zEntry = zifs.GetNextEntry();
            }
        }

        private void unzipWork(object sender, DoWorkEventArgs e)
        {
            UnzipInfo uzi = e.Argument as UnzipInfo;
            e.Result = null;

            //this makes it so that when we start to close, we can block
            //these workers from trying to create new files while we are trying to delete
            closeWaitHandle.WaitOne(-1);

            //open a stream to our file and send it to the ZipInputStream
            FileStream ifs = null;
            ZipInputStream zifs = null;

            try
            {
                ifs = new FileStream(uzi.ZipArchive, FileMode.Open, FileAccess.Read);
                zifs = new ZipInputStream(ifs);
                //let our helper take care of this
                // zipWaitHandle.WaitOne(-1);
                unzipFolder(zifs, uzi.DestinationFolder);
            }
            catch (System.Exception)
            {
                //if there is a problem, set our result to the path of the problematic archive
                e.Result = uzi.ZipArchive;
            }
            finally
            {
                zifs.Close();
                ifs.Close();
            }


            closeWaitHandle.Set();

            if (null == e.Result)
                e.Result = uzi;
        }

        private void unzipWorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            //the UnzipInfo object is stored in e.Result
            //and now we can do whatever with the files

            UnzipInfo uzi = e.Result as UnzipInfo;

            //result will be the name of the file if something bad happened
            if (null == uzi)
            {
                //notify the user of the failure in the output window
                rtxtOutput.SelectionColor = Color.Red;
                rtxtOutput.AppendText("Error: Archive " + e.Result.ToString() + " failed to unzip.  It will be skipped." + Environment.NewLine);

                //update our current total and reset our controls if we are done
                //with this batch
                if (this.nCurrentZip + 1 == nTotalZips)
                {
                    this.nTotalZips = this.nCurrentZip = 0;
                    //put these controls back to the way they were
                    this.statusLabel.Text = "Ready";
                    this.cbtnDirectories.Enabled = true;
                    this.btnClearQuery.Enabled = true;

                    //if autorun is checked, start the process of running winmoss
                    if (cbtnAutoRun.Checked)
                    {
                        AutoRunDialog ard = new AutoRunDialog();
                        ard.FormClosed += new FormClosedEventHandler(ard_FormClosed);
                        ard.Show(this);
                    }
                }
                else
                {
                    this.nCurrentZip++;
                    this.statusLabel.Text = (this.nCurrentZip + 1) + "/" + this.nTotalZips + " archives decompressed.";
                }

                zipWaitHandle.Set();

                return;
            }



            //now we can add all of the files to the box that they belong in
            string[] strarrFiles;
            string directoryName = uzi.DestinationFolder;

            //this code stolen from the directory drag and drop handler
            if (Directory.Exists(uzi.DestinationFolder))
            {
                // when directory flag is on, add filtered temp directory to listing
                if (cbtnDirectories.Checked == true && uzi.Sender == lbStudentFiles)
                {
                    AddFilteredDirectory(directoryName);
                }
                // otherwise add individual files
                else
                {
                    strarrFiles = MultipleFileFilter(directoryName, SearchOption.AllDirectories);
                    foreach (string strPath in strarrFiles)
                    {
                        //unzip any archives
                        if (".zip" == Path.GetExtension(strPath))
                            AddZip(sender, strPath);
                        else
                            AddCodeFile(uzi.Sender, strPath);
                    }
                }
            }


            //update our current total and reset our controls if we are done
            //with this batch
            if (this.nCurrentZip + 1 == nTotalZips)
            {
                this.nTotalZips = this.nCurrentZip = 0;
                //put these controls back to the way they were
                this.statusLabel.Text = "Ready";
                this.cbtnDirectories.Enabled = true;
                this.btnClearQuery.Enabled = true;

                //if autorun is checked, start the process of running winmoss
                if (cbtnAutoRun.Checked)
                {
                    AutoRunDialog ard = new AutoRunDialog();
                    ard.FormClosed += new FormClosedEventHandler(ard_FormClosed);
                    ard.Show(this);
                }

            }
            else
            {
                this.nCurrentZip++;
                this.statusLabel.Text = (this.nCurrentZip + 1) + "/" + this.nTotalZips + " archives decompressed.";
            }

            zipWaitHandle.Set();
        }
        #endregion

        void ard_FormClosed(object sender, FormClosedEventArgs e)
        {
            AutoRunDialog ard = sender as AutoRunDialog;

            if (null != ard && ard.DialogResult == DialogResult.OK)
                this.btnRunMoss.PerformClick();
        }

        #region TEMP_FILE_DELETION_STUFF
        /// File Deletion Background worker code
        /// 
        private void deleteWork(object sender, DoWorkEventArgs e)
        {
            e.Result = null;
            try
            {
                Directory.Delete(strTempPath, true);
            }
            catch
            {
                e.Result = "Deleting temporary files failed.  Program closing now.";
            }
        }

        private void deleteWorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (null == e.Result)
                this.statusLabel.Text = "Ready";
            else
            {
                this.bDeleteAttemptFailed = true;
                this.statusLabel.Text = e.Result.ToString();
            }

            this.bClosing = false;
            closeWaitHandle.Set();
            this.Close();

        }
        #endregion

    }
}

import mosswin

###########################################
# Testing purposes only
###########################################

def onMossOutput(message):
	print 'Out: ' +message
def onMossFailure(message):
	print 'FAIL: ' + message
def onMossSuccess(message):
	print 'Success: ' + message

def main():
	client = mosswin.Client()
	client.OnFailure = onMossFailure
	client.OnSuccess = onMossSuccess
	client.Output = onMossOutput
	client.language = "cc"
	client.RunAsync(["/tmp/test.cpp", "/tmp/test2.cpp"], ["/tmp/base1.cpp"])
	raw_input("Press enter to continue...\n")	

if __name__ == "__main__":
	main()
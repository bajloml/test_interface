The client and the server used to simulate KUKA code.
.NET framework -> 4.6.1

The "CrossCommServer" is a console app and should run on the VM where is the KUKA simulator, the VM should have the IP 192.168.1.15 and it listens on the port 10000

Client apps:
	- The "CrossComm_Client" is a console app client which has been used just for testing if the communication is working.
	- The "WPF_CrossComm_Client_V0002" is WPF app which is used to toggle, read or write variables to simulation.
The client app should run on the host, the communication between the client and the server is TCP/IP. The client is sending the parameters and values with the command to read or to write, the server is listening and acts on the given command. 

![alt text][sample]

[sample]: https://github.com/bajloml/KUKA_interface/blob/master/sample.gif "sample"
The client and the server used to simulate KUKA code.
.NET framework -> 4.6.1

The "Server" is a console app and should run on the VM, the VM should have the IP 192.168.1.15 and it listens on the port 10000 (it is hardcode just for testing)

Client apps:\
	- The "Client" is a console app client which has been used just for testing if the communication is working.\
	- The "WPF_Client" is WPF app which is used to toggle, read or write variables for simulation.\
The client app should run on the host, the communication between the client and the server is TCP/IP. The client is writing/reading the parameters values with the command to read or to write, the server is listening and acts on the given command. 

![alt text][sample]

[sample]: https://github.com/bajloml/KUKA_interface/blob/master/sample.gif "sample"
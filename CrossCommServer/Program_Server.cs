using System;
using System.Text;
using CrossCommEXENET;
using System.Net.Sockets;
using System.Net;

public class CrossCommServer
{
    #region CrossComm declarations
    private const int portNum = 10000;
    private const string IP_Kuka = "192.168.1.15";//   "127.0.0.1"
    private static IPAddress ipKUKA_PC = IPAddress.Parse(IP_Kuka);

    private const string Robot = "KRC1";
    private const string connectionName = Robot;
    private const short connectionMode = -1;

    private const string hostName = Robot;
    private static String StringReceived;
    private static String[] a_StringReceived;

    private static int numOfStringsReceived;
    private static String firstString;
    private static String VariableToWriteRead;
    private static String ValueToWrite;
    private static String ValueReadFromKUKA;

    const int readTimeOut = 1000;
    #endregion

    public static int Main(String[] args)
    {
        Console.Title = Robot + "CrossCommServer";

        CrossCommand crossServer = new CrossCommand();
        Object Cross = new Object();

        Console.WriteLine("initialization");
        crossServer.Init(Cross);

        Console.WriteLine("trying to connect to CrossComm");
        crossServer.ConnectToCross(connectionName, connectionMode);


        bool done = false;

        while (crossServer.CrossIsConnected)
        {

            Console.WriteLine("Create TCP listener...");
            TcpListener listener = new TcpListener(ipKUKA_PC, portNum);
            listener.Start();

            byte[] byteReceived = new byte[1024];
            int i_bytesReceived;

            while (!done)
            {
                Console.Write("Waiting for connection...");
                TcpClient client = listener.AcceptTcpClient();

                Console.WriteLine("Connection accepted.");
                NetworkStream ns_ReadWriteVariables = client.GetStream();

                try
                {
                    StringReceived                  = "";
                    numOfStringsReceived            = 0;
                    i_bytesReceived                 = ns_ReadWriteVariables.Read(byteReceived, 0, byteReceived.Length);
                    StringReceived                  = Encoding.ASCII.GetString(byteReceived, 0, i_bytesReceived);

                    Console.WriteLine("String Received: " + StringReceived);

                    if (StringReceived != "")
                    {

                        a_StringReceived = StringReceived.Split(',');

                        firstString                     = a_StringReceived[0];
                        VariableToWriteRead             = a_StringReceived[1];
                        ValueToWrite                    = a_StringReceived[2];
                        numOfStringsReceived            = Convert.ToInt32(a_StringReceived[3]);

                       Console.WriteLine(a_StringReceived[0] + a_StringReceived[1] + a_StringReceived[2] + a_StringReceived[3]);

                        Console.WriteLine("Number Of variables: " + numOfStringsReceived.ToString());
                        // Writing to the KRC1
                        if (firstString == "write")
                    {
                        if (VariableToWriteRead == "empty" || ValueToWrite == "empty")
                        {
                            continue;
                        }
                        else if ((!(VariableToWriteRead == "empty")) && (!(ValueToWrite == "empty")))
                        {
                         // KOD ZA PISANJE U KUKU
                           crossServer.SetVar(VariableToWriteRead, ValueToWrite);
                        }

                        Console.WriteLine("WRITING..." + "                 " + DateTime.Now.TimeOfDay.ToString());
                        Console.WriteLine(VariableToWriteRead + " " + ValueToWrite + "                 " + DateTime.Now.TimeOfDay.ToString());
                    }

                        // Reading from the KRC1
                        if (firstString == "read") 
                    {
                        String[] a_readVariables                    = new String[numOfStringsReceived];
                        String[] a_readValues                       = new String[numOfStringsReceived];
                        String[] a_readVarValues_ToSendBackToClient = new String[numOfStringsReceived];
                        String readValues                           = "";

                        Console.WriteLine("READING... " + "                 " + DateTime.Now.TimeOfDay.ToString());

                        if (VariableToWriteRead == "empty")
                        {
                            continue;
                        }
                        else if (!(VariableToWriteRead == "empty"))
                        {
                            //citanje sa KUKE
                            crossServer.ShowVar(VariableToWriteRead, ref ValueReadFromKUKA);
                        }

                        //Sending back values which have been read
                        a_readVarValues_ToSendBackToClient[0] = "readFromServer,";
                        a_readVarValues_ToSendBackToClient[1] = VariableToWriteRead + ",";
                        a_readVarValues_ToSendBackToClient[2] = ValueReadFromKUKA + ",";
                        a_readVarValues_ToSendBackToClient[3] = numOfStringsReceived.ToString() + ",";

                        Console.WriteLine(VariableToWriteRead + " " + ValueReadFromKUKA + "                 " + DateTime.Now.TimeOfDay.ToString());
                      
                        foreach (String s in a_readVarValues_ToSendBackToClient)
                        {
                            readValues += s;
                        }

                        Console.WriteLine("Sending back read values...     " + DateTime.Now.TimeOfDay.ToString());

                        byte[] byteReadValues = Encoding.ASCII.GetBytes(readValues);

                        try
                        {
                            ns_ReadWriteVariables.Write(byteReadValues, 0, byteReadValues.Length);
                            ns_ReadWriteVariables.Close();
                            client.Close();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                        StringReceived = "";
                        numOfStringsReceived = 0;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Console.ReadLine();
                }
            }

            listener.Stop();  
        }
        return 0;
    } 
}



using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WPF_CrossComm_Client
{
    class TCP_StringHandler
    {
        /// <summary>
        /// transforms String to a String array using given separator (for example ','(char))
        /// </summary>
        /// <param name="s"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public String[] StringToStringArray(String s, char separator)
        {
            String[] stringArray;
            stringArray = s.Split(separator);
            return stringArray;
        }

        public String StringArrayToString(String[] Sa)
        {
            String s= "";
            foreach (String SinSa in Sa)
            {
                s += SinSa;
            }
            return s;
        }

        /// <summary>
        /// Sends the data to the KUKA
        /// </summary>
        /// <param name="source1"> Variable name</param>
        /// <param name="source2"> Variable value</param>
        /// <param name="readwrite">command read or write</param>
        /// <returns></returns>
        public String[] CombineStringsToArray_WriteRead(String source1, String source2, String readwrite)
        {
            if (readwrite == "write")
            {
                String[] combinedStringsWrite = new String[ 2 + 2]; //2+2 Means:
                                                                    //0.String variable name,
                                                                    //1.String Variable Value, 
                                                                    //2. Number of variables to send(this one is left from the old function),
                                                                    //3.Number of variables for the server
                combinedStringsWrite[0] = "write,";
                combinedStringsWrite[1] = source1 + ",";
                combinedStringsWrite[2] = source2 + ",";
                combinedStringsWrite[3] = "4";
                return combinedStringsWrite;
            }

            if (readwrite == "read")
            {
                String[] combinedStringsRead = new String[2 + 2];  // 2 + 2 Means:
                                                                   //0.String variable name,
                                                                   //1.String Variable Value, 
                                                                   //2. Number of variables to send(this one is left from the old function),
                                                                   //3.Number of variables for the server
                combinedStringsRead[0] = "read,";
                combinedStringsRead[1] = source1 + ",";
                combinedStringsRead[2] = source2 + ",";
                combinedStringsRead[3] = "4";
                return combinedStringsRead;
            }
            else
            {
                String[] Empty = new String[2 + 2];
                return Empty;
            }
        }

        public String ReadVariableFromServer(String[] a_ReceivedReadVarVal, int placeToRead)
        {
            return a_ReceivedReadVarVal[placeToRead];
        }
    }

    class TCP_Write_Read
    {
        private readonly String IP_address;
        private readonly String Port;
        private String Status = "";
        private String Exception ="";

        //Constructor
        public TCP_Write_Read(String IP, String port)
        {
            this.IP_address = IP;
            this.Port = port;
        }
        //TextBox Accesors
        public String StringStatus
        {
            get
            {
                return this.Status;
            }
            set
            {
            }
        }

        public String StringException
        {
            get
            {
                return this.Exception;
            }
            set
            {
            }
        }

        //writes variable value combination to KUKA server
        public void Write_VariableValueString(String VarToWrite, String ValueToWrite)
        {
            Status = "";
            Exception = "";

            // Create Local objects
            TcpClient TCP_Client;
            TCP_StringHandler StringHandler = new TCP_StringHandler();
            String[] StringArray_toWrite = new String[2 + 2];
            String String_toWrite = "";
            try
            {   // Update Status texbox
                Status = Status + ("Trying to connect..." + " " + DateTime.Now.TimeOfDay.ToString() + "\n");
                //Create TCP client and connect
                TCP_Client = new TcpClient(IP_address, Convert.ToInt32(Port));

                while (TCP_Client.Connected)
                {
                    Status = Status + ("TCP client connected" + " " + DateTime.Now.TimeOfDay.ToString() + "\n");
                    Status = Status + ("writing..." + " " + DateTime.Now.TimeOfDay.ToString() + "\n");

                    StringArray_toWrite = StringHandler.CombineStringsToArray_WriteRead(VarToWrite, ValueToWrite, "write");
                    String_toWrite = StringHandler.StringArrayToString(StringArray_toWrite);

                    byte[] bytesToSend = Encoding.ASCII.GetBytes(String_toWrite);

                    NetworkStream ns = TCP_Client.GetStream();
                    ns.Write(bytesToSend, 0, bytesToSend.Length);
                    String StringSent = Encoding.ASCII.GetString(bytesToSend);

                    Status = Status + (StringSent + " At time:" + DateTime.Now.TimeOfDay.ToString() + "\n");

                    String_toWrite = "";
                    TCP_Client.Close();
                }
                TCP_Client.Close();
            }
            catch (Exception exc)
            {
                //update excecption textboxes
                Exception = Exception + (exc.ToString() + " " + DateTime.Now.TimeOfDay.ToString());
                Exception = Exception + ("\n");
                Exception = Exception + ("CHECK IS SERVER IS RUNNING AND LISTENING" + " " + DateTime.Now.TimeOfDay.ToString() + "\n");
                Exception = Exception + ("CHECK IP ADDRESS" + " " + DateTime.Now.TimeOfDay.ToString() + "\n");
                Exception = Exception + ("\n");
            }
        }

        public String Read_VariableValueString(String VarToRead)
        {
            Status = "";
            Exception = "";

            // Create Local objects
            TcpClient TCP_Client;
            TCP_StringHandler StringHandler = new TCP_StringHandler();
            clientWPFObjectHandler ObjectHandler = new clientWPFObjectHandler();
            String[] StringArray_toRead = new String[2 + 2];
            String[] a_ReceivedReadVarVal = new String[2 + 2];
            String String_toRead = "";
            try
                {
                Status = Status + ("Trying to connect..." + " " + DateTime.Now.TimeOfDay.ToString() + "\n");
                TCP_Client = new TcpClient(IP_address, Convert.ToInt32(Port));

                    while (TCP_Client.Connected)
                    {
                        //Update Status TextBox
                        Status = Status + ("TCP client connected" + " " + DateTime.Now.TimeOfDay.ToString() + "\n");
                        Status = Status + ("reading..." + " " + DateTime.Now.TimeOfDay.ToString() + "\n");
                        // Combine strins to array using a separator
                        StringArray_toRead   = StringHandler.CombineStringsToArray_WriteRead(VarToRead, "ReadOnly", "read");
                        // convert string array to string which will be sent to the Server
                        String_toRead        = StringHandler.StringArrayToString(StringArray_toRead);

                        //print in a status text box variables which you want to read
                        Status = Status + (VarToRead + " " + DateTime.Now.TimeOfDay.ToString() + "\n");

                        byte[] byteToSend = Encoding.ASCII.GetBytes(String_toRead);
                        NetworkStream ns = TCP_Client.GetStream();
                        ns.Write(byteToSend, 0, byteToSend.Length);
                        String StringSent = Encoding.ASCII.GetString(byteToSend);
                        String_toRead = "";

                            //getting read values back from the server
                            try
                            {
                                byte[] bytesReceived = new byte[1024];
                                int i_bytesReceived = ns.Read(bytesReceived, 0, bytesReceived.Length);

                                String readValues = Encoding.ASCII.GetString(bytesReceived, 0, i_bytesReceived);

                                a_ReceivedReadVarVal = StringHandler.StringToStringArray(readValues,',');

                                Status = Status + ("READ VALUES FROM THE SERVER " + DateTime.Now.TimeOfDay.ToString() + "\n");

                                //print in status text box read variables and values
                                Status = Status + (StringHandler.ReadVariableFromServer(a_ReceivedReadVarVal, 1) + DateTime.Now.TimeOfDay.ToString() + "\n");
   
                            TCP_Client.Close();
                            }
                           catch (Exception exc)
                            {
                                //Update exception TextBox
                                Exception = Exception + (exc.ToString() + " " + DateTime.Now.TimeOfDay.ToString());
                                Exception = Exception + ("CANNOT READ VALUES\n");
                                Exception = Exception + ("CHECK IS SERVER IS RUNNING AND LISTENING" + " " + DateTime.Now.TimeOfDay.ToString() + "\n");
                                Exception = Exception + ("CHECK IP ADDRESS" + " " + DateTime.Now.TimeOfDay.ToString() + "\n");
                                Exception = Exception + ("\n");
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        //Update exception TextBox
                        Exception = Exception + (exc.ToString() + " " + DateTime.Now.TimeOfDay.ToString());
                        Exception = Exception + ("\n");
                        Exception = Exception + ("CHECK IS SERVER IS RUNNING AND LISTENING" + " " + DateTime.Now.TimeOfDay.ToString() + "\n");
                        Exception = Exception + ("CHECK IP ADDRESS" + " " + DateTime.Now.TimeOfDay.ToString() + "\n");
                        Exception = Exception + ("\n");
                        String_toRead = "";
                    }
            return StringHandler.ReadVariableFromServer(a_ReceivedReadVarVal, 2);
        }

        public Boolean CheckConnection()
        {
            Status = "";
            Exception = "";
            // Create Local objects
            Boolean ClientConnected             = false;
            TcpClient TCP_Client;

            if (!ClientConnected)
            {
                try
                {
                    Status = Status + ("Trying to connect..." + " " + DateTime.Now.TimeOfDay.ToString() + "\n");
                    TcpClient client = new TcpClient(IP_address, Convert.ToInt32(Port));

                    if (client.Connected)
                    {
                        //update Status
                        ClientConnected = true;
                        Status = Status + ("TCP client connected" + " " + DateTime.Now.TimeOfDay.ToString() + "\n");
                        Status = Status + ("Connection works" + " " + DateTime.Now.TimeOfDay.ToString() + "\n");
                        client.Close();

                        Status = Status + ("Client closed, connection disconnected" + " " + DateTime.Now.TimeOfDay.ToString() + "\n");
                    }
                }
                catch (Exception exc)
                {
                    Exception = Exception + (exc.ToString() + " " + DateTime.Now.TimeOfDay.ToString());
                    Exception = Exception + ("\n");
                    Exception = Exception + ("CHECK IS SERVER IS RUNNING AND LISTENING" + " " + DateTime.Now.TimeOfDay.ToString() + "\n");
                    Exception = Exception + ("CHECK IP ADDRESS" + " " + DateTime.Now.TimeOfDay.ToString() + "\n");
                    Exception = Exception + ("\n");
                }
            }

            if (ClientConnected)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public String returnVarValue(String Variable, char split1, char split2)
        {
            Status = "";
            Exception = "";

            try
            {
                Variable = Variable.Split(split1)[1];
                Variable = Variable.Split(split2)[1];
            }
            catch (Exception exc)
            {
                //Update exception TextBox
                Exception = Exception + (exc.ToString() + " " + DateTime.Now.TimeOfDay.ToString());
                Exception = Exception + ("Cannot write Format Parameter value\n");
                Exception = Exception + ("Check split1 and split2 charachters\n");
                Exception = Exception + ("CHECK IS SERVER IS RUNNING AND LISTENING" + " " + DateTime.Now.TimeOfDay.ToString() + "\n");
                Exception = Exception + ("CHECK IP ADDRESS" + " " + DateTime.Now.TimeOfDay.ToString() + "\n");
                Exception = Exception + ("\n");
            }
            return Variable;
        }

    }
}

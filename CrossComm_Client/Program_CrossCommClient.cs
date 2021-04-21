using System;
using System.Net.Sockets;
using System.Text;
using System.Net;

public class TcpTimeClient
{
    private const int portNum = 10000;
    private static IPAddress ipKUKA_PC = IPAddress.Parse("127.0.0.1");//"192.168.1.15"
    private static string StringSent;

    public static int Main(String[] args)
    {
        Console.Title = "CrossCommClient";
        while (true)
        { 
            try
            {
                TcpClient client = new TcpClient(ipKUKA_PC.ToString(), portNum);
                Console.WriteLine("TCP client connected:");
                NetworkStream ns = client.GetStream();

                Console.WriteLine("String to send to server:");
                String toSend = Console.ReadLine();

                byte[] byteToSend = Encoding.ASCII.GetBytes(toSend);
                ns.Write(byteToSend, 0, byteToSend.Length);
                StringSent = Encoding.ASCII.GetString(byteToSend);

                Console.WriteLine("string sent to the server is: " + Encoding.ASCII.GetString(byteToSend));

                client.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine();
                Console.WriteLine("CHECK IS SERVER IS RUNNING AND LISTENING");
                Console.WriteLine("CHECK IP ADDRESS");
                Console.WriteLine();
            }

        }

        return 0;
    }
}

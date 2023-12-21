using Dictionary_Server;
using GiftLib;
using NLog;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace ServerNamespace
{
    public static class Server
    {
        private static IPAddress serverIP = IPAddress.Parse("127.0.0.1");
        private static int serverPort = 8888;
        private static Logger log = LogManager.GetCurrentClassLogger();

        public static int ServerPort
        {
            get { return serverPort; }
            set { serverPort = value; }
        }

        public static IPAddress ServerIP
        {
            get { return serverIP; }
            set { serverIP = value; }
        }

        static void Main()
        {
            TcpListener server = new TcpListener(ServerIP, ServerPort);
            try
            {
                server.Start();
                string mess = "Server is online";
                Console.WriteLine(mess);
                log.Info(mess);

                List<string[]> giftsData = DataBaseReader.ReadEverything();
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Thread clientThread = new Thread(HandleClient);
                    clientThread.Start(client);
                    mess = $"Connected with client {client}";
                    log.Info(mess);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex}");
                log.Info($"Something went wrong: {ex}");
            }
            finally
            {
                server?.Stop();
            }

            Console.ReadLine();
        }

        // Server HandleClient method
        static void HandleClient(object? clientObj)
        {
            TcpClient tcpClient = (TcpClient)clientObj!;

            try
            {
                NetworkStream stream = tcpClient.GetStream();

                // Assuming you have a Gift object to send
                GiftLib.Cookie giftToSend = new GiftLib.Cookie(123, "Chocolate", 2.5, 0.1, Dough.Chocolate);

                // Serialize the Gift object to JSON
                string jsonData = JsonSerializer.Serialize(giftToSend);

                // Convert the JSON string to bytes and send to the client
                byte[] buffer = Encoding.UTF8.GetBytes(jsonData);
                stream.Write(buffer, 0, buffer.Length);

                tcpClient.Close();
                Console.WriteLine("Sent response to client");
                Console.WriteLine("The client has disconnected");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error within client: {ex.Message}");
                log.Info($"Error within client: {ex.Message}");
            }
        }
    }
}

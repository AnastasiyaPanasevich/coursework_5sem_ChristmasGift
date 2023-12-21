using Dictionary_Server;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerNamespace
{
    public static class Server
    {
        private static IPAddress serverIP = IPAddress.Parse("127.0.0.1");
        private static int serverPort = 8888;

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
                Console.WriteLine("Server is online");

                List<string[]> giftsData = DataBaseReader.ReadEverything();
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Thread clientThread = new Thread(HandleClient);
                    clientThread.Start(client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex}");
            }
            finally
            {
                server?.Stop();
            }

            Console.ReadLine();
        }

        static void HandleClient(object? clientObj)
        {
            TcpClient tcpClient = (TcpClient)clientObj!; // Помечаем с атрибутом nullability

            try
            {
                NetworkStream stream = tcpClient.GetStream();
                byte[] data = new byte[1024];
                int bytesRead = stream.Read(data, 0, data.Length);
                string receivedData = Encoding.ASCII.GetString(data, 0, bytesRead);

                Console.WriteLine($"Received data from client: {receivedData}");

                string responseMessage = (receivedData == "example") ? "OK" : "Error";

                byte[] responseData = Encoding.ASCII.GetBytes(responseMessage);
                stream.Write(responseData, 0, responseData.Length);

                tcpClient.Close();
                Console.WriteLine($"Sent response to client: {responseMessage}");
                Console.WriteLine("The client has disconnected");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error within client: {ex.Message}");
            }
        }
    }
}

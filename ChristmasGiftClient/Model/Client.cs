using System;
using System.Net.Sockets;
using System.Text;

namespace ChristmasGiftClient.Model
{
    internal class Client
    {
        private static string serverIP = "127.0.0.1";
        private static int serverPort = 8888;

        static void Main()
        {
            TcpClient client = new TcpClient(serverIP, serverPort);

            try
            {
                NetworkStream stream = client.GetStream();
                string message = "example";
                byte[] data = Encoding.ASCII.GetBytes(message);

                stream.Write(data, 0, data.Length);
                Console.WriteLine($"Sent message to server: {message}");

                // Чтение ответа от сервера
                byte[] responseData = new byte[1024];
                int bytesRead = stream.Read(responseData, 0, responseData.Length);
                string responseMessage = Encoding.ASCII.GetString(responseData, 0, bytesRead);

                Console.WriteLine($"Received response from server: {responseMessage}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                client.Close();
            }

            Console.ReadLine();
        }
    }
}

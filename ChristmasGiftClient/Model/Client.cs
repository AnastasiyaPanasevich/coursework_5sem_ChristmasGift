using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasGiftClient.Model
{
    internal class Client
    {
        private static string serverIP = "127.0.0.1";
        private static int serverPort = 8888;

        public static async Task RunClientAsync(string message)
        {
            TcpClient client = new TcpClient(serverIP, serverPort);

            try
            {
                NetworkStream stream = client.GetStream();
                byte[] data = Encoding.ASCII.GetBytes(message);

                stream.Write(data, 0, data.Length);
                Console.WriteLine($"Sent message to server: {message}");

                // Чтение ответа от сервера в отдельном потоке с тайм-аутом
                Task<string> readTask = Task.Run(() => ReadFromStream(stream));

                if (await Task.WhenAny(readTask, Task.Delay(5000)) == readTask)
                {
                    // Ответ получен в течение 5 секунд
                    string responseMessage = await readTask;
                    Console.WriteLine($"Received response from server: {responseMessage}");
                }
                else
                {
                    // Тайм-аут
                    Console.WriteLine("Server did not respond within 5 seconds.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                client.Close();
            }
        }

        private static async Task<string> ReadFromStream(NetworkStream stream)
        {
            byte[] responseData = new byte[1024];
            int bytesRead = await stream.ReadAsync(responseData, 0, responseData.Length);
            return Encoding.ASCII.GetString(responseData, 0, bytesRead);
        }
    }
}

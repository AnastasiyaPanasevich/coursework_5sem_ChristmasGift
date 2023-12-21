using GiftLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ChristmasGiftClient.Model
{
    internal class Client
    {
        private static string serverIP = "127.0.0.1";
        private static int serverPort = 8888;

        public static async Task<Gift> RunClientAsync(List<int> selectedIndexes)
        {
            TcpClient client = new TcpClient(serverIP, serverPort);

            try
            {
                NetworkStream stream = client.GetStream();

                // Serialize the list of indexes to send to the server
                byte[] data = Serialize(selectedIndexes);

                // Send the serialized data to the server
                await stream.WriteAsync(data, 0, data.Length);
                Console.WriteLine($"Sent message to the server: List of Indexes");

                // Reading the response from the server in a separate thread with a timeout
                Task<byte[]> readTask = Task.Run(() => ReadFromStream(stream));

                if (await Task.WhenAny(readTask, Task.Delay(5000)) == readTask)
                {
                    // Response received within 5 seconds
                    byte[] responseData = await readTask;

                    // Deserialize the response into a Gift object
                    Gift receivedGift = Deserialize<Gift>(responseData);

                    Console.WriteLine($"Received response from the server: {receivedGift}");
                    return receivedGift;
                }
                else
                {
                    // Timeout
                    Console.WriteLine("Server did not respond within 5 seconds.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
            finally
            {
                client.Close();
            }
        }

        private static async Task<byte[]> ReadFromStream(NetworkStream stream)
        {
            byte[] responseData = new byte[1024];
            int bytesRead = await stream.ReadAsync(responseData, 0, responseData.Length);
            return responseData.Take(bytesRead).ToArray();
        }

        // Helper method to serialize an object to byte array using XML serialization
        private static byte[] Serialize<T>(T obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        // Helper method to deserialize a byte array to an object using XML deserialization
        private static T Deserialize<T>(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(ms);
            }
        }
    }
}

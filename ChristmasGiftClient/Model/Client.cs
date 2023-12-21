using GiftLib;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChristmasGiftClient.Model
{
    internal class Client
    {
        private static string serverIP = "127.0.0.1";
        private static int serverPort = 8888;

        public static void Main()
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

        public static void GetGifts(List<string[]> gifts)
        {

            foreach (var gift in gifts)
            {
                switch (Convert.ToInt32(gift[0]))
                {
                    case >= 4000:
                        States.Ornaments.Add(new Ornaments(gift));
                        break;
                    case >= 3000:
                        States.Cookies.Add(new GiftLib.Cookie(gift));
                        break;
                    case >= 2000:
                        States.Clothes.Add(new Clothes(gift));
                        break;
                    case >= 1000:
                        States.Candles.Add(new Candles(gift));
                        break;
                    default:
                        break;
                }
            }


        }
    }
}

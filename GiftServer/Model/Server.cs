using Dictionary_Server;
using GiftLib;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;
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

        static void HandleClient(object? clientObj)
        {
            TcpClient tcpClient = (TcpClient)clientObj!;

            try
            {
                NetworkStream stream = tcpClient.GetStream();

                // Read the incoming data from the client
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                byte[] receivedData = new byte[bytesRead];
                Array.Copy(buffer, receivedData, bytesRead);

                // Deserialize the byte array to a List<int>
                List<int> receivedIndexes = DeserializeXml<List<int>>(receivedData);

                // Do something with the received indexes...
                Console.WriteLine($"Received Indexes: {string.Join(", ", receivedIndexes)}");

                // Write the received index to a file
                bool writeSuccess = WriteIndexToFile(receivedIndexes[0]);

                // Send response to the client
                if (writeSuccess)
                {
                    SendResponse(stream, "Success"); // Success
                }
                else
                {
                    SendResponse(stream, "Failure"); // Failure
                }

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

        // Helper method to write an index to a file
        private static bool WriteIndexToFile(int index)
        {
            try
            {
                // Use the index as the file name
                string filePath = $"{index}.txt";

                // Write the index to the file
                File.WriteAllText(filePath, index.ToString());

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing index to file: {ex.Message}");
                return false;
            }
        }

        // Helper method to send a response to the client
        private static void SendResponse(NetworkStream stream, string response)
        {
            // Serialize the response string to a byte array
            byte[] responseData = Encoding.UTF8.GetBytes(response);

            // Send the serialized response to the client
            stream.Write(responseData, 0, responseData.Length);
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

        // Helper method to deserialize byte array to an object
        private static T DeserializeXml<T>(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(ms);
            }
        }
    }
}

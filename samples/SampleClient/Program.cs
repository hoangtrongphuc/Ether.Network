﻿using System;
using Ether.Network.Packets;
using System.Linq;

namespace SampleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new MyClient("127.0.0.1", 4444, 512);
            client.Connect();

            Console.WriteLine("Enter a message and press enter...");
            int i = 0;

            try
            {
                while (true)
                {
                    string input = $"{GenerateRandomString(20)} - {i.ToString()}";

                    if (input == "quit")
                    {
                        break;
                    }

                    if (input != null)
                    {
                        using (var packet = new NetPacket())
                        {
                            packet.Write(input);

                            Console.WriteLine($"Sending input: '{input}' ; packet length: '{packet.Buffer.Length}'");
                            client.Send(packet);
                        }
                    }

                    i++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            
            client.Disconnect();

            Console.WriteLine("Disconnected. Press any key to continue...");
            Console.ReadLine();
        }

        private const string CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="count">Length of the string.</param>
        /// <returns>Generated string</returns>
        public static string GenerateRandomString(int count = 8)
        {
            var random = new Random();

            return new string(
                Enumerable.Repeat(CHARACTERS, count)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
        }
    }
}
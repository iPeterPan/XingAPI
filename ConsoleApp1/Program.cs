﻿using System;
using System.IO;
using System.Runtime.InteropServices;
using WindowsFormsApp1;
using XingAPINet;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (XingClient xing = new XingClient(false))
            {
                xing.Login += xing_Login;
                xing.Start();

                Console.WriteLine("Press any key to exit...");
                Console.WriteLine();
                Console.ReadLine();
            }
        }

        private static void xing_Login(object sender, LoginEventArgs e)
        {
            string currentFolder = Path.GetDirectoryName(typeof(Program).Assembly.Location);

            XingClient xingClient = sender as XingClient;
            Console.WriteLine($"# of account: {xingClient.NumberOfAccount}");

            foreach (string account in xingClient.GetAccounts())
            {
                Console.WriteLine("\t" + account);
            }

            XQt1101 query = new XQt1101();
            {
                XQt1101InBlock inBlock = new XQt1101InBlock();
                inBlock.shcode = "078020";

                if (query.SetFields(inBlock) == false)
                {
                    Console.WriteLine("Failed to verify data: " + inBlock.BlockName);
                    return;
                }

                Console.WriteLine("GetFields: " + inBlock.BlockName);

                if (query.Request() < 0)
                {
                    Console.WriteLine("Failed to send request");
                }

                Console.WriteLine("Request");

                // XQt1101OutBlock outBlock = XQt1101OutBlock.GetFields(query);
            }
        }
    }
}

﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    private const int portNum = 13;

    public static int Main(string[] args)
    {
        bool done = false;
        var listener = new TcpListener(IPAddress.Any, portNum);
        listener.Start();

        while (!done)
        {
            Console.Write("Waiting for connection...");
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Connection accepted.");

            NetworkStream ns = client.GetStream();
            byte[] byteTime = Encoding.ASCII.GetBytes(DateTime.Now.ToString());

            try
            {
                ns.Write(byteTime, 0, byteTime.Length);
                ns.Close();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        listener.Stop();
        return 0;
    }
} 
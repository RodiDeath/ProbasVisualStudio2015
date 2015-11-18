using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ServidorConsola
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 2000);
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(localEndPoint);

            listener.Listen(10);

            Console.WriteLine("Waiting for a connection...");
            // Program is suspended while waiting for an incoming connection.
            Socket client = listener.Accept();

            NetworkStream ns = new NetworkStream(client);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);

            sw.WriteLine("BIENVENIDO");
            Console.WriteLine("Conexion establecida");
            sw.Flush();
            string datos = "";

            while (true)
            {
                datos = sr.ReadLine();

                if (datos == "exit")
                {
                    break;
                }
                sw.WriteLine("##" + datos);
                sw.Flush();
            }

            sw.WriteLine("ADIOS");
            sw.Close();
            sr.Close();
            ns.Close();

            client.Close();

            Console.WriteLine("Conexion con cliente finalizada");
            Console.WriteLine("Pulse cualquier tecla para salir...");
            Console.ReadLine();

            listener.Close();

        }
    }
}

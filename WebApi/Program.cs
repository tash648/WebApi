using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using QuickErrandsWebApi;

namespace Server
{
    class Program
    {
        private static string GetIPAddress()
        {
            return Dns.GetHostAddresses(Dns.GetHostName()).First(a => a.AddressFamily == AddressFamily.InterNetwork).ToString();
        }

        static void Main(string[] args)
        {
            string baseAddress = string.Format("http://localhost:8081");//, args.Any() ? args[0] : GetIPAddress());

            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.WriteLine("Сервер запущен");
                Console.ReadLine();
            }
        }
    }
}

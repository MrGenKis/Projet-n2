﻿using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace P2FixAnAppDotNetCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
            Console.WriteLine("Test : La console fonctionne !");
            Console.ReadLine(); 

        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

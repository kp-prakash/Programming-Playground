﻿using Microsoft.Owin.Hosting;
using System;
using System.Diagnostics;

namespace MyApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string baseUrl = "http://localhost:12345/";

            using (WebApplication.Start<Startup>(new StartOptions() { Url = baseUrl, Server = "MyCustomServer" }))
            {
                // Note: CustomServer has not actually been implemented, no requests will be accepted.

                // Launch the browser
                // Process.Start(baseUrl);

                Console.WriteLine("Started, Press any key to stop.");
                Console.ReadKey();
                Console.WriteLine("Stopped");
            }
        }
    }
}

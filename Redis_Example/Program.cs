
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redis_Example.Implementations;
using Redis_Example.Interfaces;

namespace Redis_Example
{
    class Program
    {
        private static ServiceProvider _services;
        private static bool StopRunning;
        static void Main(string[] args)
        {

            var diContainer = new ServiceCollection();
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            diContainer.AddSingleton<IConfiguration>(builder.Build());

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-provider")
                {
                    var provider = args[i + 1];

                    if (provider == "redis")
                    {
                        diContainer.AddSingleton<ICache, RedisCache>();
                    }

                    else if (provider == "memory")
                        diContainer.AddSingleton<ICache, InMemoryCaching>();
                }
            }
            _services = diContainer.BuildServiceProvider();


            Console.WriteLine("Hello RedisExample!");
            Help();
            do MainLoop();
            while (!StopRunning);

        }

        private static void MainLoop()
        {
            Console.WriteLine("Please select an action");
            try
            {
                char selection = Console.ReadKey().KeyChar.ToString().ToLower()[0];
                Console.WriteLine();
                switch (selection)
                {
                    case 'g':
                        Get();
                        break;
                    case 's':
                        Set();
                        break;
                    case 'q':
                        Quit();
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error handling input");
            }
        }

        private static void Get()
        {
            Console.WriteLine("Input key to look up");
            var key = Console.ReadLine();
            var value = _services.GetService<ICache>().Get(key);
            Console.WriteLine($"Value = {value}");
        }

        private static void Set()
        {
            Console.WriteLine("Input key");
            var key = Console.ReadLine();
            Console.WriteLine("Input value");
            var value = Console.ReadLine();
            _services.GetService<ICache>().Set(key, value);
            Console.WriteLine($"Done");
        }

        private static void Quit()
        {
            StopRunning = true;
            Console.WriteLine("Press any key to close window");
            Console.ReadKey();
        }

        private static void Help()
        {
            Console.WriteLine("Options are as follows:");
            Console.WriteLine("(G)et");
            Console.WriteLine("(S)et");
            Console.WriteLine("(Q)uit");
        }
    }
}

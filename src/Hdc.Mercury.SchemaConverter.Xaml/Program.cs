using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hdc.Mercury.Configs;

namespace Hdc.Mercury.SchemaConverter.Xaml
{
    internal class Program
    {
        private const string DefaultDeviceConfigsFileName = "DeviceConfigs.xml";

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("args.length == 0. you can input fileNames of DeviceDefs.");
                Exit();
            }

            var deviceDefsFileNames = args.First();

            var boot = new Bootstrapper();

            boot.Convert(deviceDefsFileNames, DefaultDeviceConfigsFileName);

            var repo = new XamlDeviceGroupConfigRepository();
            var config = repo.Load();

            Exit();
        }


        private static void Exit()
        {
            Console.WriteLine("\n--------------------");
            Console.WriteLine("convertion completed");
            Console.WriteLine("press any key to exit...\n\n");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
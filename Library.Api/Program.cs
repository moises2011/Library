using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using log4net;
using System.Reflection;
using log4net.Config;

namespace Library
{
    public class Program
    {
       public static void Main(string[] args)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

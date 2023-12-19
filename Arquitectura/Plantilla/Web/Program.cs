using System.IO;
using System.Reflection;
using System.Xml;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Web
{
    public class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));
        public static void Main(string[] args)
        {
            ConfigurarLog4Net();
            CreateHostBuilder(args)                
                .Build()
                .Run();
        }
                
        private static void ConfigurarLog4Net()
        {
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("log4net.config"));

            var repo = log4net.LogManager.CreateRepository(
                Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

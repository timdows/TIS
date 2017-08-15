using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace tis
{
	public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
				.UseUrls("http://localhost:5010")
				.UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}

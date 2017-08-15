using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace TimdowsIdentityServer
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
				.Build();

			host.Run();
		}
	}
}

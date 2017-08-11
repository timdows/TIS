﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace TimdowsIdentityServer
{
	public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
				.UseKestrel()
				.UseUrls("http://localhost:5000")
				.UseStartup<Startup>()
                .Build();
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace TimdowsIdentityServer
{
	public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddMvc();

			var cert = new X509Certificate2(Path.Combine(Directory.GetCurrentDirectory(), "tis.timdows.pfx"), "houseDB321");

			services.AddIdentityServer()
				.AddSigningCredential(cert)
				.AddInMemoryApiResources(Config.GetApiResources())
				.AddInMemoryClients(Config.GetClients())
				.AddTestUsers(Config.GetUsers());
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
			loggerFactory.AddConsole(LogLevel.Debug);
			app.UseDeveloperExceptionPage();

			app.UseIdentityServer();

			if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			//app.Run(async (context) =>
			//{
			//    await context.Response.WriteAsync("Hello World!");
			//});

			app.UseStaticFiles();
			app.UseMvcWithDefaultRoute();
		}
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
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
			var cert = new X509Certificate2(Path.Combine(Directory.GetCurrentDirectory(), "tis.timdows.pfx"), "houseDB321");

			services.AddIdentityServer(options =>
			{
				options.IssuerUri = "https://tis.timdows.com";
			})
				.AddSigningCredential(cert)
				.AddInMemoryApiResources(Config.GetApiResources())
				.AddInMemoryClients(Config.GetClients())
				.AddTestUsers(Config.GetUsers());
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
			loggerFactory.AddConsole(LogLevel.Debug);

			if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			var fordwardedHeaderOptions = new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
			};
			fordwardedHeaderOptions.KnownNetworks.Clear();
			fordwardedHeaderOptions.KnownProxies.Clear();

			app.UseForwardedHeaders(fordwardedHeaderOptions);

			app.UseIdentityServer();
		}
    }
}

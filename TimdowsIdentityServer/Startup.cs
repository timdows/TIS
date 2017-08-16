using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using TimdowsIdentityServer.Settings;

namespace TimdowsIdentityServer
{
	public class Startup
    {
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();

			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.Enrich.FromLogContext()
				.WriteTo.LiterateConsole()
				.ReadFrom.Configuration(Configuration)
				.CreateLogger();
		}

		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
        {
			var cert = new X509Certificate2(Path.Combine(Directory.GetCurrentDirectory(), "tis.timdows.pfx"), "houseDB321");
			var identityServerSettings = Configuration.GetSection("IdentityServer").Get<IdentityServerSettings>();

			services.AddIdentityServer()
				.AddSigningCredential(cert)
				.AddInMemoryApiResources(Config.GetApiResources())
				.AddInMemoryClients(Config.GetClients(identityServerSettings.ROClients))
				.AddTestUsers(Config.GetUsers(identityServerSettings.Users));

			services.Configure<IdentityServerSettings>(Configuration.GetSection("IdentityServer"));
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
			loggerFactory.AddSerilog();

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

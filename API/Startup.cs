using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace API
{
	public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			});

			services.AddJwtBearerAuthentication(o =>
			{
				o.Authority = "http://localhost:5000";
				o.Audience = "houseDB";
				o.RequireHttpsMetadata = false;
			});

			services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
			loggerFactory.AddConsole(LogLevel.Debug);
			//loggerFactory.AddDebug();
			

			//var identityServerAuthenticationOptions = new IdentityServerAuthenticationOptions
			//{
			//	Authority = "http://localhost:5000",
			//	RequireHttpsMetadata = false,

			//	ApiName = "houseDB"
			//};

			////app.UseIdentityServerAuthentication(identityServerAuthenticationOptions);
			//app.UseJwtBearerAuthentication(new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerOptions
			//{
			//	Authority = "http://localhost:5000",
			//	RequireHttpsMetadata = false,
			//	Audience = "houseDB"
			//});

			app.UseAuthentication();

			app.UseMvc();
		}
    }
}

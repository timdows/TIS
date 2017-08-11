using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API
{
	public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddAuthentication(o =>
			{
				o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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
			loggerFactory.AddConsole();
			loggerFactory.AddDebug();

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

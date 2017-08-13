﻿using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

			var test = new Test();
			test.RunAsync().GetAwaiter().GetResult();
		}

		public class Test
		{
			public async Task RunAsync1()
			{
				// discover endpoints from metadata
				var disco = await DiscoveryClient.GetAsync("http://localhost:5000");

				// request token
				var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "testsecret123");
				var tokenResponse = await tokenClient.RequestClientCredentialsAsync("houseDB");

				if (tokenResponse.IsError)
				{
					Console.WriteLine(tokenResponse.Error);
					return;
				}

				Console.WriteLine(tokenResponse.Json);

				// call api
				var client = new HttpClient();
				client.SetBearerToken(tokenResponse.AccessToken);

				var response = await client.GetAsync("http://localhost:5001/api/identity");
				if (!response.IsSuccessStatusCode)
				{
					Console.WriteLine(response.StatusCode);
				}
				else
				{
					var content = await response.Content.ReadAsStringAsync();
					Console.WriteLine(JArray.Parse(content));
				}
			}

			public async Task RunAsync()
			{
				// discover endpoints from metadata
				var disco = await DiscoveryClient.GetAsync("http://localhost:5000");

				// request token
				var tokenClient = new TokenClient(disco.TokenEndpoint, "angularjsclient");
				var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("test@timdows.com", "z2MMpN2CXU10d0hPNVGA!", "openid");

				if (tokenResponse.IsError)
				{
					Console.WriteLine(tokenResponse.Error);
					return;
				}

				Console.WriteLine(tokenResponse.Json);
				Console.WriteLine("\n\n");
			}
		}
    }
}

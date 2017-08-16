using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using TimdowsIdentityServer.Settings;

namespace TimdowsIdentityServer
{
	public class Config
    {
		public static IEnumerable<ApiResource> GetApiResources()
		{
			return new List<ApiResource>
			{
				new ApiResource("houseDB", "HouseDB API")
			};
		}

		public static IEnumerable<Client> GetClients(List<ROClient> roClients)
		{
			var clients = new List<Client>();
			foreach(var roClient in roClients)
			{
				// resource owner password grant client
				clients.Add(new Client
				{
					ClientId = roClient.ClientId,
					AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
					ClientSecrets = { new Secret(roClient.Secret.Sha256()) },
					AllowedScopes = { "houseDB" }
				});
			}

			return clients;
		}

		public static List<TestUser> GetUsers(List<User> users)
		{
			var testUsers = new List<TestUser>();
			foreach (var user in users)
			{
				testUsers.Add(new TestUser
				{
					SubjectId = user.SubjectId,
					Username = user.Name,
					Password = user.Password,
					Claims = new List<Claim>
					{
						//new Claim(JwtClaimTypes.Role, "houseDB.user"),
						new Claim(JwtClaimTypes.Scope, "houseDB")
					}
				});
			}
			
			return testUsers;
		}
	}
}

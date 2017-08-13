using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

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

		public static IEnumerable<Client> GetClients()
		{
			return new List<Client>
			{
				new Client
				{
					ClientId = "client",

					// no interactive user, use the clientid/secret for authentication
					AllowedGrantTypes = GrantTypes.ClientCredentials,

					// secret for authentication
					ClientSecrets =
					{
						new Secret("testsecret123".Sha256())
					},

					// scopes that client has access to
					AllowedScopes = { "houseDB" }
				},

				 // resource owner password grant client
				new Client
				{
					ClientId = "ro.client",
					AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

					ClientSecrets =
					{
						new Secret("testsecret123".Sha256())
					},
					AllowedScopes = { "houseDB" }
				}

			};
		}

		public static List<TestUser> GetUsers()
		{
			var users = new List<TestUser>();
			var user = new TestUser
			{
				SubjectId = "1",
				Username = "test",
				Password = "test",
				Claims = new List<Claim>()
			};
			user.Claims.Add(new Claim(JwtClaimTypes.Role, "houseDB.user"));
			user.Claims.Add(new Claim(JwtClaimTypes.Scope, "houseDB"));

			users.Add(user);

			return users;
		}
	}
}

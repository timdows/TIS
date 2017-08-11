using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

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
			return new List<TestUser>
			{
				new TestUser
				{
					SubjectId = "1231233",
					Username = "alice",
					Password = "password"
				},
				new TestUser
				{
					SubjectId = "2",
					Username = "bob",
					Password = "password"
				}
			};
		}
	}
}

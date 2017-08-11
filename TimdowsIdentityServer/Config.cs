using IdentityServer4.Models;
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
				}
			};
		}
	}
}

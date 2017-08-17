using System.Collections.Generic;

namespace TimdowsIdentityServer.Settings
{
	public class IdentityServerSettings
    {
		public List<ROClient> ROClients { get; set; }
		public List<IdentityUser> Users { get; set; }
	}

	public class ROClient
	{
		public string ClientId { get; set; }
		public string Secret { get; set; }
	}

	public class IdentityUser
	{
		public string SubjectId { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
		public List<IdentityScope> Scopes { get; set; }
	}

	public class IdentityScope
	{
		public string Name { get; set; }
	}
}

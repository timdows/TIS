using System.Collections.Generic;

namespace TimdowsIdentityServer.Settings
{
	public class IdentityServerSettings
    {
		public List<ROClient> ROClients { get; set; }
		public List<User> Users { get; set; }
	}

	public class ROClient
	{
		public string ClientId { get; set; }
		public string Secret { get; set; }
	}

	public class User
	{
		public string SubjectId { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
	}
}

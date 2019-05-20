namespace BRG.Entities
{
	public class ProxyItem
	{
		/// <summary>
		/// 创建 <see cref="ProxyItem" />  的新实例(ProxyItem)
		/// </summary>
		public ProxyItem()
		{
			Port = 80;
		}

		public string Host { get; set; }

		public int Port { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }
	}
}

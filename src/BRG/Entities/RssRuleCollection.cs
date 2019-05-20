using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRG.Entities
{
	using System.Threading.Tasks;
	using FSLib.Network.Http;
	using Newtonsoft.Json;

	public class RssRuleCollection : Dictionary<int, FilterRuleCollection>
	{
		/// <summary>
		/// 检测指定规则的更新
		/// </summary>
		/// <param name="id"></param>
		public void CheckUpdate(int id)
		{
			if (!ContainsKey(id))
				return;

			Task.Factory.StartNew(() =>
			{
				var client = new HttpClient();
				var url = $"https://www.fishlee.net/service/download/{id}";
				var ctx = client.Create<byte[]>(HttpMethod.Get, url, allowAutoRedirect: true);
				ctx.Send();

				if (ctx.IsValid())
				{
					try
					{
						var text = Encoding.UTF8.GetString(ctx.Result.Decompress());
						this[id] = JsonConvert.DeserializeObject<FilterRuleCollection>(text);
					}
					catch (Exception)
					{
						var rss = AppContext.Instance.Options.RssRuleCollection;
						if (rss.ContainsKey(id))
							rss.Remove(id);
					}
				}
			});
		}
	}
}

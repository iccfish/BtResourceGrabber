namespace BRG.Entities
{
	using System;
	using System.Collections.Generic;

	public class HashSiteMap : Dictionary<int, Dictionary<long, string>>
	{
		/// <summary>
		/// 查找指定的站里面的指定ID对应的Hash
		/// </summary>
		/// <param name="siteId"></param>
		/// <param name="torrentId"></param>
		/// <returns></returns>
		public string FindHash(int siteId, long torrentId)
		{
			return this.GetValue(siteId)?.GetValue(torrentId);
		}

		/// <summary>
		/// 更新指定的站点ID对应的Hash缓存
		/// </summary>
		/// <param name="siteId"></param>
		/// <param name="torrentId"></param>
		/// <param name="hash"></param>
		public void UpdateHash(int siteId, long torrentId, string hash)
		{
			var map = this.GetValue(siteId, _ => new Dictionary<long, string>());
			map.AddOrUpdate(torrentId, hash);
		}
	}

	public class HashSiteMapStr : Dictionary<int, Dictionary<string, string>>
	{
		/// <summary>
		/// 查找指定的站里面的指定ID对应的Hash
		/// </summary>
		/// <param name="siteId"></param>
		/// <param name="torrentId"></param>
		/// <returns></returns>
		public string FindHash(int siteId, string torrentId)
		{
			return this.GetValue(siteId)?.GetValue(torrentId);
		}

		/// <summary>
		/// 更新指定的站点ID对应的Hash缓存
		/// </summary>
		/// <param name="siteId"></param>
		/// <param name="torrentId"></param>
		/// <param name="hash"></param>
		public void UpdateHash(int siteId, string torrentId, string hash)
		{
			var map = this.GetValue(siteId, _ => new Dictionary<string, string>());
			map.AddOrUpdate(torrentId, hash);
		}
	}

}

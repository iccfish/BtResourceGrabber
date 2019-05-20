namespace BRG
{
	using System;
	using System.Text.RegularExpressions;
	using System.Web;
	using BRG.Entities;

	public class BrtUtility
	{

		public static string ClearString(string str)
		{
			if (string.IsNullOrEmpty(str))
				return string.Empty;

			return Regex.Replace(System.Web.HttpUtility.HtmlDecode(str), @"(\s|<.*?>)", "");
		}

		public static string RemoveHtmlChars(string str)
		{
			if (string.IsNullOrEmpty(str))
				return string.Empty;

			return Regex.Replace(System.Web.HttpUtility.HtmlDecode(str), @"<.*?>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase).Trim();
		}

		public static string DecodePath(string str)
		{
			if (string.IsNullOrEmpty(str))
				return "";

			str = HttpUtility.UrlDecode(str);
			str = Regex.Replace(str, @"&#0*(\d+);", _ =>
			{
				return ((char)_.Groups[1].Value.ToInt32()).ToString();
			});

			return str;
		}

		public static string CreateEd2kUrl(IResourceInfo resource)
		{
			return $"ed2k://|file|{HttpUtility.UrlPathEncode(resource.Title)}|{resource.DownloadSizeValue}|{resource.Hash.ToUpper()}|/";
		}

		public static string BuildDetailLink(IResourceInfo resource)
		{
			switch (resource.ResourceType)
			{
				case ResourceType.BitTorrent:
					return resource.CreateMagnetLink(true);
				case ResourceType.Ed2K:
					return CreateEd2kUrl(resource);
				case ResourceType.NetDisk:
					if (resource.NetDiskData != null)
						return resource.NetDiskData.Url + (resource.NetDiskData.Pwd.IsNullOrEmpty() ? "" : "\t密码：" + resource.NetDiskData.Pwd);

					return string.Empty;
				default:
					break;
			}

			return null;
		}
	}
}

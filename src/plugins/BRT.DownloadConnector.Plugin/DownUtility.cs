using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRT.DownloadConnector.Plugin
{
	using System.Text.RegularExpressions;

	class DownUtility
	{
		/// <summary>
		/// 获得可执行文件路径
		/// </summary>
		/// <param name="openUrl"></param>
		/// <returns></returns>
		public static string GetFilePath(string openUrl)
		{
			if(openUrl.IsNullOrEmpty())
				return null;

			if (openUrl[0] == '"')
			{
				return Regex.Match(openUrl, @"^""([^""]+?)""", RegexOptions.IgnoreCase).GetGroupValue(1);
			}

			return openUrl.Split(' ')[0];
		}
	}
}

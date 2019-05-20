using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRG
{
	using System.Text.RegularExpressions;

	public static class EngineUtility
	{
		public static bool IsUnicodeKey(string key)
		{
			return key.Any(s => s > 255);
		}


		public static int CompareSizeString(string str1, string str2)
		{
			var v1 = ToSize(str1);
			var v2 = ToSize(str2);

			return v1 < v2 ? -1 : v1 == v2 ? 0 : 1;
		}

		public static long ToSize(string str)
		{
			if (str.IsNullOrEmpty())
				return 0L;

			str = Regex.Replace(str, @"[\s,]", "");
			var m = Regex.Match(str, @"([\d\.]+)(G|T|M|GB|TB|MB|KB|B|b)i?(bytes)?", RegexOptions.IgnoreCase);
			if (!m.Success)
				return 0L;

			var num1 = m.Groups[1].Value.ToDouble();
			var unit = m.Groups[2].Value;

			if (unit[0] == 'T' || unit[0] == 't')
				num1 *= 1000000000000;
			else if (unit[0] == 'G' || unit[0] == 'g')
				num1 *= 1000000000;
			else if (unit[0] == 'M' || unit[0] == 'm')
				num1 *= 1000000;
			else if (unit[0] == 'K' || unit[0] == 'k')
				num1 *= 1000;

			return (long)num1;
		}

		public static long ToInt64(byte[] buffer)
		{
			var ret = 0L;

			for (int i = 0; i < buffer.Length; i++)
			{
				ret += (long)buffer[i] << (8 * i);
			}

			return ret;
		}
	}
}

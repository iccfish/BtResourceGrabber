using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BtResourceGrabber.Service
{
	using System.Text.RegularExpressions;
	using BRG;
	using BRG.Engines;
	using BRG.Engines.Entities;
	using BRG.Entities;
	using BtResourceGrabber.Entities;

	class ResourceFilterProcessor
	{
		string _key, _filteredKey;

		HashSet<string> _excludeKey = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		HashSet<string> _strictKey = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		string _sizeDirective;
		long? _sizeFilter = null;
		bool _greater = false;

		public string ParseDirective(string key)
		{
			if (!AppContext.Instance.Options.EnableSearchDirective)
				return key;

			if (key.IsNullOrEmpty())
				return "";

			if (key.IsIgnoreCaseEqualTo(_key))
				return _filteredKey;
			_key = key;

			_excludeKey.Clear();
			_strictKey.Clear();
			_sizeDirective = null;

			_filteredKey = key.IsNullOrEmpty() ? "" : Regex.Replace(key, @"(?<=^|\s)([+\-><])([^\s]+)", _ =>
			{
				var op = _.Groups[1].Value;
				if (op == "+")
				{
					_strictKey.SafeAdd(_.Groups[2].Value);
					return _.Groups[2].Value;
				}
				if (op == "-")
				{
					_excludeKey.SafeAdd(_.Groups[2].Value);
					return "";
				}
				if (op == ">" || op == "<")
				{
					var size = EngineUtility.ToSize(_.Groups[2].Value);
					if (size > 0L)
					{
						_greater = op == ">";
						_sizeDirective = _.Value;
						_sizeFilter = size;

						return "";
					}
				}

				return _.Groups[2].Value;
			});
			_filteredKey = Regex.Replace(_filteredKey, @"\s\s+", " ");

			return _filteredKey;
		}

		static char[] _keySplit = new[] { ',', ';', '\t', ' ' };

		public void Filter(IResourceSearchInfo result, string key)
		{
			//执行过滤
			var query = result.Where(s =>
									(_excludeKey.Count > 0 && _excludeKey.Any(x => s.Title.Contains(x, StringComparison.OrdinalIgnoreCase)))
									|| (_strictKey.Count > 0 && !_strictKey.Any(x => s.Title.Contains(x, StringComparison.OrdinalIgnoreCase)))
									|| (_sizeFilter.HasValue && (_sizeFilter.Value < s.DownloadSizeCalcauted ^ _greater))
				);
			var removed = query.ToArray();
			removed.ForEach(s => result.Remove(s));

			//计算排序
			var keys = key.Split(_keySplit, StringSplitOptions.RemoveEmptyEntries);
			result.ForEach(s => ((ResourceInfo)s).MatchWeight = keys.Count(x => s.Title.IndexOf(x, StringComparison.OrdinalIgnoreCase) != -1));
		}
	}
}

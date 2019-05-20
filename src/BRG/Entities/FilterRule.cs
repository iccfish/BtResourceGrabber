using System;
using System.Linq;
using System.Text;

namespace BRG.Entities
{
	using System.Text.RegularExpressions;
	using Newtonsoft.Json;

	/// <summary>
	/// 过滤规则
	/// </summary>
	public class FilterRule
	{
		[JsonProperty("s")]
		public FilterSource Source { get; set; }

		[JsonProperty("g")]
		public bool IsRegex { get; set; }

		[JsonProperty("r")]
		public string[] Rules { get; set; }

		[JsonProperty("a")]
		public FilterBehaviour Behaviour { get; set; }

		Regex[] _compiledRegex;

		[JsonIgnore]
		public Regex[] CompiledRegex
		{
			get
			{
				if (_compiledRegex == null)
				{
					try
					{
						_compiledRegex = Rules.Select(s => new Regex(s, RegexOptions.IgnoreCase | RegexOptions.Singleline)).ToArray();
					}
					catch (Exception)
					{
						return null;
					}
				}

				return _compiledRegex;
			}
		}

		public bool IsMatch(IResourceInfo resource)
		{
			if ((Rules?.Length ?? 0) <= 0)
			{
				return false;
			}

			var target = Source == FilterSource.Name ? resource.Title : resource.Hash;
			if (IsRegex)
			{
				return CompiledRegex?.All(s => s.IsMatch(target)) == true;
			}

			return Rules.All(s => target.IndexOf(s, StringComparison.OrdinalIgnoreCase) != -1);
		}
	}
}

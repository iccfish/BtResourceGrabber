namespace BRG.Service
{
	using System;
	using System.Collections.Generic;
	using BRG.Entities;
	using SmartAssembly.Attributes;

	[DoNotObfuscate]
	public class DownloadHistory : Dictionary<string, HistoryItem>
	{
		public DownloadHistory()
			: base(StringComparer.OrdinalIgnoreCase)
		{

		}
	}
}

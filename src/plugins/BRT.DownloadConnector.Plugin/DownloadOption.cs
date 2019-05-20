using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRT.DownloadConnector.Plugin
{
	public class DownloadOption
	{
		public Dictionary<string, DownloadParameter> DownloadParameters { get; } = new Dictionary<string, DownloadParameter>(StringComparer.OrdinalIgnoreCase);
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRG.Engines.BuildIn
{
	using Ivony.Html;

	static class HtmlExtensionMethods
	{
		public static string GetAttributeValue(this IHtmlElement element, string name)
		{
			return element.Attribute(name)?.AttributeValue;
		}
	}
}

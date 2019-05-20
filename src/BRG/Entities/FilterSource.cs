using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BRG.Entities
{
	/// <summary>
	/// 过滤来源
	/// </summary>
	public enum FilterSource
	{
		[Description("资源名")]
		Name,
		[Description("资源特征码")]
		Hash
	}
}

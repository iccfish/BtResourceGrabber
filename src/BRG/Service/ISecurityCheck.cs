using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRG.Service
{
	using BRG.Entities;
	using BRG.Security;

	public interface ISecurityCheck
	{
		/// <summary>
		/// 校验器
		/// </summary>
		List<ISecurityChecker> Checkers { get; }
		void Check(IResourceSearchInfo infos);
	}

}

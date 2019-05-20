namespace BRG.Security
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.Diagnostics;
	using System.Linq;
	using BRG.Entities;
	using BRG.Service;

	[Export(typeof(ISecurityCheck))]
	public class SecurityCheck : ISecurityCheck
	{
		#region 单例模式

		static SecurityCheck _instance;
		static readonly object _lockObject = new object();

		public static SecurityCheck Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = new SecurityCheck();
						}
					}
				}

				return _instance;
			}
		}

		#endregion

		/// <summary>
		/// 校验器
		/// </summary>
		[ImportMany]
		public List<ISecurityChecker> Checkers { get; private set; }

		public void Check(IResourceSearchInfo infos)
		{
			var list = infos.Where(s => s.VerifyState == VerifyState.Unknown).ToList();
			var rules = AppContext.Instance.Options.RssRuleCollection.Values.Union(new[] { AppContext.Instance.Options.RuleCollection }).ExceptNull().ToArray();

			foreach (var info in list)
			{
				Debug.WriteLine($"判断资源：{info.Title} 安全性（自动规则）");

				foreach (var checker in Checkers)
				{
					var state = checker.Check(info);
					if (state != VerifyState.Unknown)
					{
						info.ChangeVerifyState(state, 0);
						break;
					}
				}

				Debug.WriteLine($"判断资源：{info.Title} 安全性（订阅规则）");

				foreach (var rc in rules.ExceptNull())
				{
					//过滤校验
					if (rc.Count <= 0) continue;

					foreach (var rule in rc.ExceptNull())
					{
						if (!rule.IsMatch(info))
							continue;

						//标记or移除？
						if (rule.Behaviour == FilterBehaviour.Hide)
						{
							infos.Remove(info);

							break;
						}
						if (rule.Behaviour == FilterBehaviour.Mark && (info.VerifyState == VerifyState.Unknown || info.VerifyState == VerifyState.None))
						{
							//标记
							info.ChangeVerifyState(VerifyState.Illegal, 0);
						}
					}
				}
			}
		}
	}
}

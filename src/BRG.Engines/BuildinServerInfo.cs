namespace BRG.Engines
{
	using System;
	using System.Drawing;
	using BRG.Entities;

	public class BuildinServerInfo : IServiceInfo
	{
		Version _version;

		/// <summary>
		/// 创建 <see cref="BuildinServerInfo" />  的新实例(BuildinServerInfo)
		/// </summary>
		/// <param name="name"></param>
		/// <param name="icon"></param>
		/// <param name="descrption"></param>
		public BuildinServerInfo(string name, Image icon, string descrption)
		{
			Name = name;
			Icon = icon;
			Descrption = descrption;
			_version = new Version("1.0.0.0");
		}

		#region Implementation of IServiceInfo

		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// 作者
		/// </summary>
		public string Author { get { return "木鱼"; } }

		/// <summary>
		/// 联系方式
		/// </summary>
		public string Contract
		{
			get { return "http://www.fishlee.net/about/"; }
		}

		/// <summary>
		/// 图标
		/// </summary>
		public Image Icon { get; private set; }

		/// <summary>
		/// 描述
		/// </summary>
		public string Descrption { get; private set; }

		/// <summary>
		/// 版本
		/// </summary>
		public Version Version
		{
			get { return _version; }
			set { _version = value; }
		}

		#endregion
	}
}

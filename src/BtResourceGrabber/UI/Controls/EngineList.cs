using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BtResourceGrabber.Service;

namespace BtResourceGrabber.UI.Controls
{
	using System.ComponentModel;
	using BRG;
	using BRG.Service;

	partial class EngineList : TabControl
	{
		string _searchKey;

		public EngineList()
		{
			InitializeComponent();

			if (Program.IsRunning)
			{
				Init();
			}
		}

		/// <summary>
		/// 选择发生了变化
		/// </summary>
		public event EventHandler<ResourceSelectionEventArgs> ResourceSelectionChanged;

		/// <summary>
		/// 引发 <see cref="ResourceSelectionChanged" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnResourceSelectionChanged(ResourceSelectionEventArgs ea)
		{
			var handler = ResourceSelectionChanged;
			if (handler != null)
				handler(this, ea);
		}


		/// <summary>
		/// 获得或设置搜索关键字
		/// </summary>
		public string SearchKey
		{
			get { return _searchKey; }
			set
			{
				if (string.IsNullOrEmpty(value))
					return;

				if (string.Compare(value, SearchKey, StringComparison.OrdinalIgnoreCase) == 0)
				{
					SelectedEngineUI.ContinueLoad(SearchKey);
				}
				else
				{
					_searchKey = value;
					SelectedEngineUI.ForeceReload(SearchKey);
				}
			}
		}

		public new MultiEngineTab SelectedTab
		{
			get { return base.SelectedTab as MultiEngineTab; }
		}

		public new int SelectedIndex
		{
			get { return base.SelectedIndex; }
		}

		public MultiEngineTabContent SelectedEngineUI
		{
			get { return SelectedTab.SelectValue(s => s.Controls[0] as MultiEngineTabContent); }
		}

		void Init()
		{
			ilProvider.Images.Add("", Properties.Resources.globe_16);
			ServiceManager.Instance.ResourceProviders.ForEach(s =>
			{
				s.DisabledChanged += SearchProvider_DisabledChanged;
				if (s.Info.Icon != null)
				{
					ilProvider.Images.Add(s.Info.Name, s.Info.Icon);
				}
			});

			//综合搜索
			var multiSearchEngines = ServiceManager.Instance.ResourceProviders.Where(s => s.SupportResourceInitialMark && !AppContext.Instance.Options.EngineStandalone.Contains(s.Info.Name)).ToArray();
			if (multiSearchEngines.Length > 0)
				new MultiEngineTab(this, multiSearchEngines);

			ServiceManager.Instance.ResourceProviders.Where(s => (!s.SupportResourceInitialMark || AppContext.Instance.Options.EngineStandalone.Contains(s.Info.Name)) && !s.Disabled).ForEach(s =>
			   {
				   new MultiEngineTab(this, s);
			   });
			ServiceManager.Instance.ActiveSearchServiceChanged += Instance_ActiveSearchServiceChanged;
			Instance_ActiveSearchServiceChanged(null, null);

			SelectedIndexChanged += EngineList_SelectedIndexChanged;

			Multiline = AppContext.Instance.Options.MultilineEngineTab;
			AppContext.Instance.Options.PropertyChanged += Options_PropertyChanged;
		}

		private void SearchProvider_DisabledChanged(object sender, EventArgs e)
		{
			var provider = sender as IResourceProvider;

			if (provider.Disabled)
			{
				//禁用了。
				this.TabPages.Cast<MultiEngineTab>().Where(s => s.Providers.Length == 1 && s.Providers[0] == provider).ToArray().ForEach(s => TabPages.Remove(s));
			}
			else
			{
				//已启用
				if (!TabPages.Cast<MultiEngineTab>().Any(s => s.Providers.Contains(provider)))
				{
					new MultiEngineTab(this, provider);
				}
			}
		}

		private void Options_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.IsPropertyOf(AppContext.Instance.Options, s => s.MultilineEngineTab))
			{
				Multiline = AppContext.Instance.Options.MultilineEngineTab;
			}
		}

		void EngineList_SelectedIndexChanged(object sender, EventArgs e)
		{
			SelectedEngineUI.ReloadIfKeyChanged(SearchKey);
			ServiceManager.Instance.ActiveSearchService = SelectedEngineUI?.RawEngines[0];
		}


		void Instance_ActiveSearchServiceChanged(object sender, EventArgs e)
		{
			if (ServiceManager.Instance.ActiveSearchService != null)
			{
				var tab=TabPages.Cast<MultiEngineTab>().FirstOrDefault(s => s.EngineUI.RawEngines.Contains(ServiceManager.Instance.ActiveSearchService));
				if (tab != null)
					base.SelectedTab = tab;
			}
			else base.SelectedIndex = 0;
		}




		#region 隐藏方法

		bool ShouldSerializeImageList()
		{
			return false;
		}
		bool ShouldSerializeSearchKey()
		{
			return false;
		}

		bool ShouldSerializeSelectedIndex()
		{
			return false;
		}
		bool ShouldSerializeSelectedTab()
		{
			return false;
		}


		#endregion
	}
}

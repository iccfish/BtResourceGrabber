using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BtResourceGrabber.UI.Controls.ResourceListView
{
	using System.Drawing;
	using BRG;
	using BRG.Engines.Entities;
	using BRG.Entities;
	using BtResourceGrabber.Entities;
	using ComponentOwl.BetterListView;

	class ResourceListViewItem : BetterListViewItem
	{
		public IResourceInfo Resource { get; private set; }


		public new ResourceListViewItem ParentItem
		{
			get { return base.ParentItem as ResourceListViewItem; }
		}

		/// <summary>
		/// 获得当前资源的分类项
		/// </summary>
		public ResourceListViewItem MasterItem
		{
			get { return ParentItem ?? this; }
		}

		public List<ResourceIdentity> ResourceIdentities
		{
			get
			{
				if (Resource.HasSubResources)
				{
					return ChildItems.OfType<ResourceListViewItem>().SelectMany(s => s.ResourceIdentities).ToList();
				}

				return new List<ResourceIdentity>()
				{
					new ResourceIdentity(Resource, ChildItems.Count == 0 ? null : ChildItems.OfType<ResourceListViewItem>().Select(s => s.Resource).ToArray())
				};
			}
		}

		/// <summary>
		/// 获得顶级Item
		/// </summary>
		public ResourceListViewItem TopItem
		{
			get
			{
				var item = this;
				while (item.ParentItem != null && !item.ParentItem.Resource.HasSubResources)
				{
					item = item.ParentItem;
				}
				return item;
			}
		}


		public bool MultiEngine { get; private set; }

		bool _subItemCreated;

		public ResourceListViewItem(IResourceInfo resource, bool multiEngine)
		{
			Resource = resource;
			MultiEngine = multiEngine;

			//初始化结构
			Text = resource.Title.GetSubString(100);
			SubItems.AddRange(new[]
							{
								"",
								"",
								resource.HasSubResources ? "(多个资源)" : resource.DownloadSizeValue == null ? (resource.DownloadSizeCalcauted == 0L ? resource.DownloadSize.DefaultForEmpty("<未知>") : resource.DownloadSizeCalcauted.ToSizeDescription()) : resource.DownloadSizeValue.Value.ToSizeDescription(),
								resource.FileCount == null ? "----" : resource.FileCount.Value.ToString("N0"),
								"",
								resource.UpdateTime.HasValue ? resource.UpdateTime.Value.ToString("yyyy-MM-dd") : resource.UpdateTimeDesc
							});
			Resource_PreviewTypeChanged(this, null);

			UseItemStyleForSubItems = true;
			ImageKey = resource.HasSubResources ? "multiresource" : multiEngine ? "e_" + resource.Provider.Info.Name : GetImageKey(resource);

			//初始化标记
			RefreshItemMask();
			if (!resource.IsHashLoaded)
				Resource.DetailLoaded += Resource_DetailLoaded;
			else
			{
				RefreshDownloadStatus();
				RefreshIllegalStatus();
			}
			resource.PreviewTypeChanged += Resource_PreviewTypeChanged;

			CheckRowStyle();

			IsExpanded = false;
			if (resource.HasSubResources)
			{
				//多个资源项
				if (resource.SubResources == null)
				{
					ChildItems.Add(new PaddingLoadingItem());
					resource.SubResourceLoaded += (s, e) => AppContext.UiInvoke(LoadSubResources);
				}
				else
				{
					LoadSubResources();
					IsExpanded = true;
				}
			}
		}

		static string GetImageKey(IResourceInfo resource)
		{
			if (resource.ResourceType == ResourceType.BitTorrent)
				return "torrent";
			if (resource.ResourceType == ResourceType.Ed2K)
				return "ed2k";

			if (resource.ResourceType == ResourceType.NetDisk)
			{
				switch (resource.NetDiskData.NetDiskType)
				{
					case NetDiskType.Unknown:
						break;
					case NetDiskType.BaiduDesk:
						return "wp_baidu";
					case NetDiskType.XlShare:
						return "wp_xl";
					case NetDiskType.QqXf:
						return "wp_xf";
					case NetDiskType.QihuShare:
						return "wp_qihu";
					case NetDiskType.Weiyun:
						break;
					default:
						break;
				}
			}

			return "";
		}

		void LoadSubResources()
		{
			Array.ForEach(ChildItems.OfType<PaddingLoadingItem>().ToArray(), s => s.Remove());

			//appnd sub items
			ListView?.BeginUpdate();

			foreach (var item in ChildItems.OfType<PaddingLoadingItem>().ToArray())
			{
				item.Remove();
			}
			ChildItems.AddRange(Resource.SubResources.Select(s => new ResourceListViewItem(s, false)));

			ListView?.EndUpdate();
		}

		private void Resource_PreviewTypeChanged(object sender, EventArgs e)
		{
			AppContext.UiInvoke(() =>
			{
				if (Resource.SupportPreivewType != PreviewType.None)
				{
					if (Resource.SupportPreivewType.HasFlag(PreviewType.Image))
						SubItems[1].Image = Properties.Resources.picture;
					else
						SubItems[1].Image = Properties.Resources.favorite;
				}
				else
				{
					SubItems[1].Image = null;
				}
			});
		}

		private void Resource_DetailLoaded(object sender, EventArgs e)
		{
			AppContext.UiInvoke(() =>
			{
				RefreshItemMask();
				RefreshDownloadStatus();
				RefreshIllegalStatus();
			});
		}

		/// <summary>
		/// 标记为已下载
		/// </summary>
		public void RefreshDownloadStatus(bool initialCall = true)
		{
			AppContext.UiInvoke(() =>
			{
				ListView?.BeginUpdate();
				SubItems[2].Image = null;

				if (Resource.Downloaded)
					SubItems[2].Image = Properties.Resources.down_16;
				else if (Resource.VerifyState != VerifyState.Unknown && initialCall)
				{
					RefreshIllegalStatus();
				}

				ChildItems.OfType<ResourceListViewItem>().ForEach(s =>
				{
					s.Resource.ChangeDownloadedStatus(Resource.Downloaded);
					s.RefreshDownloadStatus(false);
				});
				ListView?.EndUpdate();
			});
		}
		/// <summary>
		/// 标记安全状态
		/// </summary>
		public void RefreshIllegalStatus()
		{
			if (/*!AppContext.Instance.Options.EnableCloudSaftyCheckOverride || */Resource.Downloaded)
				return;

			AppContext.UiInvoke(() =>
			{
				ListView?.BeginUpdate();

				SubItems[2].Image = null;

				switch (Resource.VerifyState)
				{
					case VerifyState.Unknown:
						break;
					case VerifyState.None:
						break;
					case VerifyState.Verified:
						SubItems[2].Image = Properties.Resources.tick_shield;
						break;
					case VerifyState.Reported:
						SubItems[2].Image = Properties.Resources.exclamation_shield;
						break;
					case VerifyState.Illegal:
					case VerifyState.Fake:
						SubItems[2].Image = Properties.Resources.cross_shield;
						break;
					case VerifyState.AutoFake:
					case VerifyState.AutoIllegal:
						SubItems[2].Image = Properties.Resources.minus_shield;
						break;
					default:
						break;
				}

				ChildItems.OfType<ResourceListViewItem>().ForEach(s =>
				{
					s.Resource.ChangeVerifyState(Resource.VerifyState, Resource.ReportNum);
					s.RefreshIllegalStatus();
				});
				ListView?.EndUpdate();
			});
		}

		/// <summary>
		/// 标记为未下载
		/// </summary>
		public void SetUndownload()
		{
			SubItems[2].Image = null;

			if (ChildItems != null)
				ChildItems.OfType<ResourceListViewItem>().ForEach(s => s.SetUndownload());
		}

		void CheckRowStyle()
		{
			if (Resource?.ResourceType == ResourceType.MultiResource)
			{
				ForeColor = Color.BlueViolet;
				IsBold = true;
				return;
			}

			var hasPreferDownloadr = Resource?.PreferDownloadProvider != null || ChildItems.Cast<ResourceListViewItem>().Any(s => s.Resource.PreferDownloadProvider != null);
			if (hasPreferDownloadr)
			{
				ForeColor = Color.RoyalBlue;
				IsBold = true;
			}
		}


		/// <summary>
		/// 刷新指定项的标记
		/// </summary>
		public void RefreshItemMask()
		{
			string maskName;
			var mask = AppContext.Instance.GetResourceMark(Resource, out maskName);

			ApplyMark(maskName, mask);
		}

		/// <summary>
		/// 应用样式
		/// </summary>
		/// <param name="maskName"></param>
		/// <param name="e"></param>
		public void ApplyMark(string maskName, HashMark e)
		{
			if (e != null)
			{
				ForeColor = e.Color;
				BackColor = e.BackColor;
			}
			else
			{
				ForeColor = SystemColors.WindowText;
				BackColor = SystemColors.Window;

				CheckRowStyle();
			}
			SubItems[5].Text = maskName ?? "";

			if (ChildItems != null)
			{
				ChildItems.Cast<ResourceListViewItem>().ForEach(s => s.ApplyMark(maskName, e));
			}
		}

		/// <summary>
		/// 添加子项
		/// </summary>
		/// <param name="resource"></param>
		public void AppendSubResult(IResourceInfo resource)
		{
			if (ChildItems.Cast<ResourceListViewItem>().Any(s => s.Resource.Provider == resource) || (Resource != null && Resource.Provider == resource.Provider))
				return;

			if (!_subItemCreated)
			{
				_subItemCreated = true;

				var res = Resource;
				ChildItems.Add(new ResourceListViewItem(res, true));

				//重置图标
				ImageKey = "torrent_multi";
				IsBold = true;
			}

			//设置资源大小
			if (Resource.DownloadSizeCalcauted == 0L)
			{
				Resource.DownloadSize = resource.DownloadSize;
				Resource.DownloadSizeValue = resource.DownloadSizeValue;
				SubItems[3].Text = resource.DownloadSizeValue == null ? (resource.DownloadSizeCalcauted == 0L ? resource.DownloadSize.DefaultForEmpty("<未知>") : resource.DownloadSizeCalcauted.ToSizeDescription()) : resource.DownloadSizeValue.Value.ToSizeDescription();
			}
			if (Resource.SupportPreivewType == PreviewType.None)
			{
				Resource.PreviewInfo = Resource.PreviewInfo ?? resource.PreviewInfo;
				Resource.SupportPreivewType = resource.SupportPreivewType;
				Resource_PreviewTypeChanged(this, null);
			}

			//状态
			if (Resource.VerifyState == VerifyState.Unknown || Resource.VerifyState == VerifyState.None)
			{
				if (resource.VerifyState != VerifyState.Unknown && resource.VerifyState != VerifyState.None)
				{
					Resource.ChangeVerifyState(resource.VerifyState, resource.ReportNum);
				}
			}
			else
			{
				resource.ChangeVerifyState(Resource.VerifyState, Resource.ReportNum);
			}
			if (resource.Downloaded || Resource.Downloaded)
			{
				resource.ChangeDownloadedStatus(true);
				Resource.ChangeDownloadedStatus(true);
			}

			ChildItems.Add(new ResourceListViewItem(resource, true));
			CheckRowStyle();
		}
	}
}

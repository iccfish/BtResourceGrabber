using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BtResourceGrabber.Entities;
using BtResourceGrabber.Service;

namespace BtResourceGrabber.UI.Controls
{
	using BRG.Engines.Entities;
	using ComponentOwl.BetterListView;

	public class ResourceSelectionEventArgs : EventArgs
	{
		/// <summary>
		/// 已选择的数据
		/// </summary>
		public List<ResourceIdentity> SelectedResources { get; private set; }


		/// <summary>
		/// 创建 <see cref="ResourceSelectionEventArgs"/>  的新实例(ResourceSelectEventArgs)
		/// </summary>
		/// <param name="selectedResources"></param>
		/// <param name="provider"></param>
		public ResourceSelectionEventArgs(List<ResourceIdentity> selectedResources)
		{
			SelectedResources = selectedResources;
		}
	}
}

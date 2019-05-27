using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BtResourceGrabber.UI.Controls.ResourceListView
{
	partial class ListView
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.ilLv = new System.Windows.Forms.ImageList(this.components);
			this.clName = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
			this.clSize = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
			this.clFileCount = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
			this.clMark = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
			this.clUpdate = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
			this.clDownloaded = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
			this.clPreview = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// ilLv
			// 
			this.ilLv.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.ilLv.ImageSize = new System.Drawing.Size(16, 16);
			this.ilLv.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// clName
			// 
			this.clName.Name = "clName";
			this.clName.Text = "资源名";
			this.clName.Width = 159;
			// 
			// clSize
			// 
			this.clSize.MinimumWidth = 85;
			this.clSize.Name = "clSize";
			this.clSize.Text = "大小";
			this.clSize.Width = 85;
			// 
			// clFileCount
			// 
			this.clFileCount.MinimumWidth = 52;
			this.clFileCount.Name = "clFileCount";
			this.clFileCount.Text = "文件数";
			this.clFileCount.Width = 52;
			// 
			// clMark
			// 
			this.clMark.MinimumWidth = 81;
			this.clMark.Name = "clMark";
			this.clMark.Text = "标记";
			this.clMark.Width = 81;
			// 
			// clUpdate
			// 
			this.clUpdate.MinimumWidth = 81;
			this.clUpdate.Name = "clUpdate";
			this.clUpdate.Text = "更新日期";
			this.clUpdate.Width = 81;
			// 
			// clDownloaded
			// 
			this.clDownloaded.MinimumWidth = 30;
			this.clDownloaded.Name = "clDownloaded";
			this.clDownloaded.Width = 30;
			// 
			// clPreview
			// 
			this.clPreview.MinimumWidth = 30;
			this.clPreview.Name = "clPreview";
			this.clPreview.Width = 30;
			// 
			// ListView
			// 
			this.AutoSizeItemsInDetailsView = true;
			this.Columns.Add(this.clName);
			this.Columns.Add(this.clPreview);
			this.Columns.Add(this.clDownloaded);
			this.Columns.Add(this.clSize);
			this.Columns.Add(this.clFileCount);
			this.Columns.Add(this.clMark);
			this.Columns.Add(this.clUpdate);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.GridLines = ComponentOwl.BetterListView.BetterListViewGridLines.None;
			this.HideSelectionMode = ComponentOwl.BetterListView.BetterListViewHideSelectionMode.Disable;
			this.ImageList = this.ilLv;
			this.ImageListColumns = this.ilLv;
			this.ImageListGroups = this.ilLv;
			this.SortedColumnsRowsHighlight = ComponentOwl.BetterListView.BetterListViewSortedColumnsRowsHighlight.ShowAlways;
			this.SortOnCollectionChange = false;
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private ComponentOwl.BetterListView.BetterListViewColumnHeader clName;
		private ComponentOwl.BetterListView.BetterListViewColumnHeader clSize;
		private ComponentOwl.BetterListView.BetterListViewColumnHeader clFileCount;
		private ComponentOwl.BetterListView.BetterListViewColumnHeader clMark;
		private ComponentOwl.BetterListView.BetterListViewColumnHeader clUpdate;
		private System.Windows.Forms.ImageList ilLv;
		private ComponentOwl.BetterListView.BetterListViewColumnHeader clDownloaded;
		private ComponentOwl.BetterListView.BetterListViewColumnHeader clPreview;
	}
}

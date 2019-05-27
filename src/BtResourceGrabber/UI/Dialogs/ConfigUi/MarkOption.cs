using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BtResourceGrabber.UI.Dialogs.ConfigUi
{
	using BRG;
	using BRG.Entities;

	using Controls;

	partial class MarkOption : ConfigControl
	{
		List<string> _maskSrc;

		public MarkOption()
		{
			InitializeComponent();

			Text = "标记选项";
			Image = Properties.Resources.label_16;

			var opt = AppContext.Instance.Options;

			chkAutoMark.AddDataBinding(opt, s => s.Checked, s => s.EnableAutoMark);
			gpAutoMark.AddDataBinding(chkAutoMark, s => s.Enabled, s => s.Checked);
			cbAutoMarkType.DataSource = _maskSrc = opt.HashMarks.Keys.ToList();
			cbAutoMarkType.DataBindings.Add("SelectedItem", opt, "AutoMarkDownloadedTorrent");

			InitMaskEdit();

		}

		void InitMaskEdit()
		{
			var opt = AppContext.Instance.Options;
			lbMask.Items.AddRange(opt.HashMarks.Select(s => (object)new MaskBoxItem(s.Key, s.Value)).ToArray());

			btnMaskAdd.Click += (s, e) =>
			{
				using (var dlg = new SelectMark())
				{
					if (dlg.ShowDialog() != DialogResult.OK)
						return;

					opt.HashMarks.Add(dlg.MaskName, dlg.HashMark);
					lbMask.Items.Add(new MaskBoxItem(dlg.MaskName, dlg.HashMark));
					lbMask.SelectedIndex = lbMask.Items.Count - 1;
					_maskSrc.Add(dlg.MaskName);
				}
				cbAutoMarkType.ResetBindings();
			};
			btnMaskEdit.Click += (s, e) =>
			{
				if (lbMask.SelectedItem == null)
					return;

				var item = lbMask.SelectedItem as MaskBoxItem;
				var prevMaskName = item.MaskName;
				using (var dlg = new SelectMark() { MaskName = item.DisplayText, HashMark = item.Mask })
				{
					if (dlg.ShowDialog() != DialogResult.OK)
						return;

					item.Mask = dlg.HashMark;
					item.MaskName = dlg.MaskName;
					lbMask.Invalidate();

					if (item.MaskName == prevMaskName)
					{
						opt.HashMarks[item.MaskName] = item.Mask;
					}
					else
					{
						opt.HashMarks.Remove(prevMaskName);
						opt.HashMarks.Add(item.MaskName, item.Mask);

						//更新
						foreach (var key in AppContext.Instance.HashMarkCollection.Keys)
						{
							if (AppContext.Instance.HashMarkCollection[key] == prevMaskName)
								AppContext.Instance.HashMarkCollection[key] = item.MaskName;
						}
					}
				}
			};
			btnMaskDelete.Click += (s, e) =>
			{
				if (lbMask.SelectedItem == null)
					return;

				var item = lbMask.SelectedItem as MaskBoxItem;
				if (!Question("确定要删除标记 【" + item.MaskName + "】吗？"))
					return;

				opt.HashMarks.Remove(item.MaskName);
				lbMask.Items.Remove(item);

				_maskSrc.Remove(item.MaskName);
				cbAutoMarkType.ResetBindings();
			};
		}

		class MaskBoxItem : ListBoxExItem
		{
			HashMark _mask;

			public HashMark Mask
			{
				get
				{
					return _mask;
				}
				set
				{
					_mask = value;
					ForeColor = value.Color;
				}
			}

			public string MaskName
			{
				get { return DisplayText; }
				set { DisplayText = value; }
			}

			/// <summary>
			/// 创建 <see cref="Option.MaskBoxItem" />  的新实例(MaskBoxItem)
			/// </summary>
			/// <param name="mask"></param>
			public MaskBoxItem(string name, HashMark mask)
			{
				_mask = mask;

				ForeColor = mask.Color;
				DisplayText = name;
			}
		}


	}
}

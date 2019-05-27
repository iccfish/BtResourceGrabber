using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BtResourceGrabber.UI.Dialogs.ConfigUi
{
	using System.Text.RegularExpressions;
	using BRG.Entities;

	public partial class RuleEditor : Form
	{
		public RuleEditor()
		{
			InitializeComponent();


			txtTest.TextChanged += TxtTest_TextChanged;
			txtRule.TextChanged += TxtRule_TextChanged;
			btnOk.Enabled = false;
			cbBehaviour.SelectedIndex = 0;

			//过滤来源
			var sources = typeof(FilterSource).GetEnumDescription();
			cbSource.DataSource = sources;
			cbSource.ValueMember = "Value";
			cbSource.DisplayMember = "DescriptionText";
			cbSource.SelectedIndex = 0;
		}

		private void TxtRule_TextChanged(object sender, EventArgs e)
		{
			if (txtRule.TextLength == 0)
			{
				btnOk.Enabled = false;
			}

			txtRule.ForeColor = Color.Green;
			btnOk.Enabled = true;
			if (chkUseReg.Checked)
			{
				try
				{
					new Regex(txtRule.Text);
				}
				catch (Exception)
				{
					txtRule.ForeColor = Color.Red;
					btnOk.Enabled = false;
				}
			}
		}

		private void TxtTest_TextChanged(object sender, EventArgs e)
		{
			if (txtTest.TextLength == 0)
			{
				lblTestResult.Hide();
				return;
			}
			lblTestResult.Show();

			var rules = txtRule.Lines.Where(s => s.Length > 0).ToArray();
			var regex = new List<Regex>(rules.Length);

			if (chkUseReg.Checked)
			{
				try
				{
					rules.ForEach(s => regex.Add(new Regex(s, RegexOptions.Singleline | RegexOptions.IgnoreCase)));
				}
				catch (Exception)
				{
					lblTestResult.ForeColor = Color.Red;
					lblTestResult.Text = "表达式有误";

					return;
				}
			}

			bool isMatch;
			if (regex.Count > 0)
			{
				isMatch = regex.All(s => s.IsMatch(txtTest.Text));
			}
			else
			{
				isMatch = rules.All(s => txtTest.Text.IndexOf(s, StringComparison.OrdinalIgnoreCase) != -1);
			}

			lblTestResult.ForeColor = isMatch ? Color.Green : Color.Red;
			lblTestResult.Text = isMatch ? "匹配成功" : "不匹配";
		}

		public FilterRule Rule
		{
			get
			{
				return new FilterRule()
				{
					Behaviour = (FilterBehaviour)cbBehaviour.SelectedIndex,
					IsRegex = chkUseReg.Checked,
					Rules = txtRule.Lines,
					Source = (FilterSource)cbSource.SelectedValue
				};
			}
			set
			{
				cbBehaviour.SelectedIndex = (int)value.Behaviour;
				txtRule.Lines = value.Rules;
				chkUseReg.Checked = value.IsRegex;
			}
		}
	}
}

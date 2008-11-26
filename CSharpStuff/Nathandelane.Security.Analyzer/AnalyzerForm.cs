using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nathandelane.Security.Analyzer
{
	public partial class AnalyzerForm : Form
	{
		public AnalyzerForm()
		{
			InitializeComponent();
		}

		private void ClearForm(object sender, EventArgs e)
		{
			cryptPlainTextBox.Text = String.Empty;
			resultsListBox.Items.Clear();
		}

		private void AnalyzeText(object sender, EventArgs e)
		{
			analyzeButton.Enabled = false;
			resultsListBox.Items.Clear();
			Dictionary<string, long> analysis = new Dictionary<string, long>();

			if (characterRadioButton.Checked)
			{
				char[] characters = cryptPlainTextBox.Text.ToCharArray();

				foreach (char c in characters)
				{
					if (!analysis.ContainsKey(String.Format("{0} ({1})", (int)c, c)))
					{
						analysis.Add(String.Format("{0} ({1})", (int)c, c), 1L);
					}
					else
					{
						analysis[String.Format("{0} ({1})", (int)c, c)]++;
					}
				}
			}
			else if (wordRadioButton.Checked)
			{
				string[] delimiters = { ((string)delimiterComboBox.SelectedItem).Contains("(032)") ? " " : ((string)delimiterComboBox.SelectedItem).Split( new char[] { ' ' })[1] };
				string[] strings = cryptPlainTextBox.Text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

				foreach (string s in strings)
				{
					if (!analysis.ContainsKey(String.Format("{0}", s)))
					{
						analysis.Add(String.Format("{0}", s), 1L);
					}
					else
					{
						analysis[String.Format("{0}", s)]++;
					}
				}
			}

			foreach (string s in analysis.Keys)
			{
				string item = String.Format("{0}:{1}", PadNumber(analysis[s], 4), s);
				resultsListBox.Items.Add(item);
			}

			analyzeButton.Enabled = true;
		}

		private void PopulateDelimiterComboBox(object sender, EventArgs e)
		{
			char[] characters = cryptPlainTextBox.Text.ToCharArray();

			foreach (char c in characters)
			{
				string item = String.Format("({0}) {1}", PadNumber((int)c, 3), c);

				if (!delimiterComboBox.Items.Contains(item))
				{
					delimiterComboBox.Items.Add(item);
				}
			}
		}

		private void EnableOrDisableComboBox(object sender, EventArgs e)
		{
			if (wordRadioButton.Checked)
			{
				delimiterComboBox.Enabled = true;
			}
			else
			{
				delimiterComboBox.Enabled = false;
			}
		}

		private string PadNumber(long number, int width)
		{
			int remainingWidth = width - (number.ToString()).Length;
			string result = String.Format("{0}{1}", new String('0', remainingWidth), number);

			return result;
		}
	}
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nathandelane.Net.HGrep
{
	public partial class AboutForm : Form
	{
		#region Fields

		private string _licenseText;

		#endregion

		#region Constructors

		public AboutForm(string licenseText)
		{
			_licenseText = licenseText;

			InitializeComponent();

			_licenseTextBox.Text = _licenseText;
		}

		#endregion
	}
}

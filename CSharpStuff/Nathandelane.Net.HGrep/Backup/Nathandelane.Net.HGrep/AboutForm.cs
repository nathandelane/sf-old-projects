/*  Copyright (C) 2009, Nathandelane.
	License:
	Copyright 1992, 1997-1999, 2000 Free Software Foundation, Inc.

	This program is free software; you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation; either version 3, or (at your option)
	any later version.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with this program; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA
	02111-1307, USA.
*/

using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;

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

		private void OpenBrowser(object sender, LinkLabelLinkClickedEventArgs e)
		{
			e.Link.Visited = true;

			RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(@"htmlfile\shell\open\command", false);
			string defaultBrowserPath = (registryKey.GetValue(null, null) as string).Split(new char[] { '"' })[1];

			Process.Start(defaultBrowserPath, "http://code.google.com/p/hgrep");
		}
	}
}

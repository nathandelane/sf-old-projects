using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Nathandelane.Net.PortScanner
{
	public partial class DefaultForm : Form
	{
		private Settings _settings;

		public DefaultForm()
		{
			_settings = new Settings();
			InitializeComponent();
		}

		private void ClearOutputTextBox(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_outputTextBox.Clear();
		}

		private void ExecuteCommand(object sender, EventArgs e)
		{
			_outputTextBox.Cursor = Cursors.WaitCursor;
			string[] tokens = _commandTextBox.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

			if (tokens.Length > 0)
			{
				try
				{
					switch (tokens[0])
					{
						case "scan":
							if (tokens.Length == 3)
							{
								string url = tokens[1];

								try
								{
									int port = int.Parse(tokens[2]);
									Scan(url, port, true);
								}
								catch (Exception e1)
								{
									Regex regex1 = new Regex("^[\\d]*(-){1}[\\d]*");
									Regex regex2 = new Regex("^^([\\d]*(1){1})*([\\d]*){1}");

									if (regex1.Matches(tokens[2]).Count > 0)
									{
										string portRange = tokens[2];
										Scan(url, portRange);
									}
									else if (regex2.Matches(tokens[2]).Count > 0)
									{
										string[] ports = tokens[2].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
										int[] intPortNums = new int[ports.Length];
										int counter = 0;
										foreach (string portNum in ports)
										{
											try
											{
												intPortNums[counter] = int.Parse(portNum);
												counter++;
											}
											catch (Exception e3)
											{
												_outputTextBox.AppendText(String.Format("Scan command not understood at [2]: {0}{1}", tokens[2], Environment.NewLine));
											}
										}
										Scan(url, intPortNums);
									}
									else
									{
										_outputTextBox.AppendText(String.Format("Scan command not understood at [2]: {0}{1}", tokens[2], Environment.NewLine));
									}
								}
							}
							else
							{
								_outputTextBox.AppendText(String.Format("Command was malformed ({0}), should be: scan <url> <port|port0,port1,...|portMin-portMax>{1}", String.Join(" ", tokens), Environment.NewLine));
							}
							break;
						default:
							_outputTextBox.AppendText(String.Format("Unrecognized command: {0}{1}", tokens[0], Environment.NewLine));
							break;
					}
				}
				catch (Exception e2)
				{
					_outputTextBox.AppendText(String.Format("Exception caught! {0}{1}", e2.Message, Environment.NewLine));
				}
				finally
				{
					_commandTextBox.Clear();
				}
			}
			else
			{
				_outputTextBox.AppendText(String.Format("Null command not allowed.{0}", Environment.NewLine));
			}

			_outputTextBox.AppendText(Environment.NewLine);
			_outputTextBox.Cursor = Cursors.Default;
		}

		private void Scan(string url, int port, bool showMessage)
		{
			if (showMessage)
			{
				_outputTextBox.AppendText(String.Format("Scanning {0} on port {1}{2}", url, port, Environment.NewLine));
			}

			using (TcpClient socket = new TcpClient())
			{

				try
				{
					socket.Connect(url, port);
					_outputTextBox.AppendText(String.Format("  Port number {0} is open{1}", port, Environment.NewLine));
				}
				catch (Exception e)
				{
					if (e is TimeoutException)
					{
						_outputTextBox.AppendText(String.Format("  Timeout was caught{0}", Environment.NewLine));
					}
					else
					{
						_outputTextBox.AppendText(String.Format("  Port number {0} is closed{1}", port, Environment.NewLine));
					}
				}
			}
		}

		private void Scan(string url, int[] ports)
		{
			string messagePart = String.Format("Scanning {0}", url);
			string message = String.Format("{0} on ports", messagePart);
			foreach(int num in ports)
			{
				message = String.Format("{0} {1}", message, num);
			}
			_outputTextBox.AppendText(String.Format("{0}{1}", message, Environment.NewLine));
			foreach (int port in ports)
			{
				Scan(url, port, false);
				this.Update();
			}
		}

		private void Scan(string url, string portRange)
		{
			int min = int.Parse(portRange.Split('-')[0]);
			int max = int.Parse(portRange.Split('-')[1]);
			_outputTextBox.AppendText(String.Format("Scanning {0} on ports {1}{2}", url, portRange, Environment.NewLine));
			for (int i = min; i <= max; i++)
			{
				Scan(url, i, false);
				this.Update();
			}
		}

		private void OnBlur(object sender, EventArgs e)
		{
			
		}

		private void ScanKeys(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13)
			{
				ExecuteCommand(sender, new EventArgs());
			}
		}
	}
}
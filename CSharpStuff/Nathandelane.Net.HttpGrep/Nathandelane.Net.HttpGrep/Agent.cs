using HtmlAgilityPack;
using ICSharpCode.SharpZipLib.GZip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Nathandelane.Net.HttpGrep
{
	public class Agent
	{
		private Context _context;
		private HtmlDocument _document;
		private HttpWebRequest _request;
		private HttpWebResponse _response;

		public Agent()
		{
			_context = Context.GetContext();
			_document = new HtmlDocument();
		}

		public void Run()
		{
			Uri requestUri = null;
			WebProxy simpleWebProxy = null;

			if (Uri.TryCreate(_context[Context.Url], UriKind.Absolute, out requestUri))
			{
				_request = (HttpWebRequest)WebRequest.Create(requestUri);

				if (_context.ArgumentIsDefined(Context.Method))
				{
					_request.Method = _context[Context.Method].ToString();
				}

				_request.Accept = "application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";
				_request.UserAgent = "HttpGrep";

				if (_context.ArgumentIsDefined(Context.IgnoreBadCerts))
				{
					ServicePointManager.ServerCertificateValidationCallback +=
						delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
						{
							return true;
						};
				}

				if (_context[Context.Post] != null)
				{
					if (!_context.ArgumentIsDefined(Context.PostContentType))
					{
						_request.ContentType = "application/x-www-form-urlencoded";
					}
					else
					{
						_request.ContentType = _context[Context.PostContentType];
					}

					_request.Method = "post";

					using (StreamWriter postBodyWriter = new StreamWriter(_request.GetRequestStream()))
					{
						postBodyWriter.Write(_context[Context.Post]);
					}
				}

				if (_context[Context.Proxy] != null)
				{
					string[] proxyParts = _context[Context.Proxy].Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
					string address = proxyParts[0];
					int port = 80;

					if (proxyParts.Length == 2)
					{
						port = Convert.ToInt32(proxyParts[1]);
					}

					simpleWebProxy = new WebProxy(address, port);

					_request.Proxy = simpleWebProxy;
				}

				try
				{
					_response = (HttpWebResponse)_request.GetResponse();
					string data;

					if (_response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
					{
						using (StreamReader reader = new StreamReader(new GZipInputStream(_response.GetResponseStream())))
						{
							data = reader.ReadToEnd();
						}
					}
					else
					{
						using (StreamReader reader = new StreamReader(_response.GetResponseStream()))
						{
							data = reader.ReadToEnd();
						}
					}

					data = FormatData(data);

					DisplayResults(data);

					_response.Close();
				}
				catch (Exception exception)
				{
					Console.WriteLine("Exception was caught: {0}", exception.Message);
				}
			}
		}

		/// <summary>
		/// Displays the results of the query based on the Context.
		/// </summary>
		/// <param name="data"></param>
		private void DisplayResults(string data)
		{
			if (_context.ArgumentIsDefined(Context.Data))
			{
				if (!_context.ArgumentIsDefined(Context.NoHeaders))
				{
					Console.WriteLine("Data:");
				}

				Console.WriteLine(data);
			}

			if (_context.ArgumentIsDefined(Context.Find))
			{
				try
				{
					if (!_context.ArgumentIsDefined(Context.NoHeaders))
					{
						Console.WriteLine("Find:");
					}

					HtmlNodeCollection selectedNodes = _document.DocumentNode.SelectNodes(_context[Context.Find]);

					if (selectedNodes != null && selectedNodes.Count > 0)
					{
						if (_context.ArgumentIsDefined(Context.PsHashes))
						{
							int outerCounter = 0;

							foreach (HtmlNode nextNode in selectedNodes)
							{
								if (outerCounter > 0)
								{
									Console.WriteLine(",");
								}

								StringBuilder nodeValue = new StringBuilder();
								nodeValue.Append(String.Format("@{{\"tagname\"=\"{0}\"", nextNode.Name));

								HtmlAttributeCollection attributes = nextNode.Attributes;
								int counter = 0;

								foreach (HtmlAttribute nextAttribute in attributes)
								{
									if (counter >= 0)
									{
										nodeValue.Append(";");
									}

									string nextAttributeEntry = String.Format("\"{0}\"=\"{1}\"", nextAttribute.Name, nextAttribute.Value);

									nodeValue.Append(nextAttributeEntry);

									counter++;
								}

								nodeValue.Append("}");

								Console.Write("{0}", nodeValue);

								outerCounter++;
							}

							Console.WriteLine("");
						}
						else
						{
							foreach (HtmlNode nextNode in selectedNodes)
							{
								StringBuilder nodeValue = new StringBuilder();
								nodeValue.Append(nextNode.Name);
								nodeValue.Append(" [");

								HtmlAttributeCollection attributes = nextNode.Attributes;
								foreach (HtmlAttribute nextAttribute in attributes)
								{
									nodeValue.Append(String.Concat("[", nextAttribute.Name, "='", nextAttribute.Value, "']"));
								}

								nodeValue.Append("]");

								Console.WriteLine("{0}", nodeValue);
							}
						}
					}
				}
				catch (Exception e)
				{
					Console.WriteLine("Exception caught {0}. {1}", e.GetType(), e.Message);
				}
			}

			if (_context.ArgumentIsDefined(Context.Request))
			{
				string[] headers = _request.Headers.AllKeys;

				if (!_context.ArgumentIsDefined(Context.NoHeaders))
				{

					Console.WriteLine("Request Headers:");
				}

				foreach (string nextHeader in headers)
				{
					Console.WriteLine("{0,-40}{1}", nextHeader, _request.Headers[nextHeader]);
				}
			}

			if (_context.ArgumentIsDefined(Context.Response) || _context.ArgCount == 1)
			{
				string[] headers = _response.Headers.AllKeys;

				if (!_context.ArgumentIsDefined(Context.NoHeaders))
				{
					Console.WriteLine("Response Headers:");
				}

				Console.WriteLine("{0,-40}{1}", "Response URL", _response.ResponseUri);

				foreach (string nextHeader in headers)
				{
					Console.WriteLine("{0,-40}{1}", nextHeader, _response.Headers[nextHeader]);
				}
			}
		}

		/// <summary>
		/// Formats the resultant data as a string of XML.
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		private string FormatData(string data)
		{
			string internalValue = data;

			_document = new HtmlDocument();
			_document.LoadHtml(data);

			using (StringWriter writer = new StringWriter())
			{
				_document.OptionOutputAsXml = true;
				_document.Save(writer);

				internalValue = writer.GetStringBuilder().ToString();
			}

			return internalValue;
		}
	}
}

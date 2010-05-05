using HtmlAgilityPack;
using ICSharpCode.SharpZipLib.GZip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;

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

			if (Uri.TryCreate(_context[Context.Url], UriKind.Absolute, out requestUri))
			{
				_request = (HttpWebRequest)WebRequest.Create(requestUri);
				_request.Accept = "application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";
				_request.UserAgent = "HttpGrep";

				if (_context[Context.Post] != null)
				{
					_request.ContentType = "application/x-www-form-urlencoded";
					_request.Method = "post";

					using (StreamWriter postBodyWriter = new StreamWriter(_request.GetRequestStream()))
					{
						postBodyWriter.Write(_context[Context.Post]);
					}
				}

				if (_context[Context.Proxy] != null)
				{
					Uri proxyAddress = null;

					if (Uri.TryCreate(_context[Context.Proxy], UriKind.Absolute, out proxyAddress))
					{
						WebProxy simpleWebProxy = new WebProxy(proxyAddress);
					}
				}

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
		}

		/// <summary>
		/// Displays the results of the query based on the Context.
		/// </summary>
		/// <param name="data"></param>
		private void DisplayResults(string data)
		{
			if (_context.ArgumentIsDefined(Context.Data))
			{
				Console.WriteLine("Data:");
				Console.WriteLine(data);
			}

			if (_context.ArgumentIsDefined(Context.Find))
			{
				try
				{
					Console.WriteLine("Find:");

					HtmlNodeCollection selectedNodes = _document.DocumentNode.SelectNodes(_context[Context.Find]);

					if(selectedNodes != null && selectedNodes.Count > 0)
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
				catch (Exception e)
				{
					Console.WriteLine("Excaption caught {0}. {1}", e.GetType(), e.Message);
				}
			}

			if (_context.ArgumentIsDefined(Context.Request))
			{
				string[] headers = _request.Headers.AllKeys;

				Console.WriteLine("Request Header:");

				foreach (string nextHeader in headers)
				{
					Console.WriteLine("{0,-40}{1}", nextHeader, _request.Headers[nextHeader]);
				}
			}

			if (_context.ArgumentIsDefined(Context.Response) || _context.ArgCount == 1)
			{
				string[] headers = _response.Headers.AllKeys;

				Console.WriteLine("Response Headers:");

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

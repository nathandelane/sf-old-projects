using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace Vehix.QA.Spider
{
    public class Spider
    {
        private static readonly int _maxSpiders = 500;
        private string _url;
        private bool _isDone;

        public Spider(string url)
        {
            _url = url;
            _isDone = false;
        }

        public void Run(Object threadContext)
        {
            try
            {
                Console.Write("Checking {0}...", Url);
                HttpWebRequest request = HttpWebRequest.Create(Url) as HttpWebRequest;
                request.AllowAutoRedirect = true;
                request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.None;
                request.ImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Console.WriteLine("Passed...Extracting links.");
                ExtractAllHtmlLinks(response);
            }
            catch (Exception e)
            {
            }

            _isDone = true;
            SpiderPool.RemoveSpider(this);
        }

        public string Url
        {
            get { return _url; }
        }

        public bool IsDone
        {
            get { return _isDone; }
        }

        public static int Max
        {
            get { return Spider._maxSpiders; }
        }

        private void ExtractAllHtmlLinks(HttpWebResponse response)
        {
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string data = reader.ReadToEnd();
            response.Close();
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(data);
            HtmlNodeCollection links = document.DocumentNode.SelectNodes("//a[@href]");

            for (int i = 0; i < links.Count; i++)
            {
                HtmlNode node = links[i];

                try
                {
                    string href = HtmlLink.NormalizeUrl(_url, node.Attributes["href"].Value);

                    if (!href.Contains("/(X(1)") && !href.Contains("mailto") && !href.Contains("javascript") && !href.Contains("#"))
                    {
                        string id = "";

                        try
                        {
                            id = node.Attributes["id"].Value;
                        }
                        catch (Exception e)
                        {
                            id = "";
                        }

                        HtmlLink link = new HtmlLink(node.InnerText, href, id, _url);
                        NavigationQueue.Push(link);
                        Spider spider = new Spider(link.Href);
                        SpiderPool.AddSpider(spider);
                    }
                }
                catch (Exception e)
                {
                }
            }
        }
    }
}

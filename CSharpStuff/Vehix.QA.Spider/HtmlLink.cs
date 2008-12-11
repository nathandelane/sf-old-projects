using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vehix.QA.Spider
{
    public class HtmlLink
    {
        private string _innerText;
        private string _href;
        private string _id;
        private string _context;

        public HtmlLink()
        {
            _innerText = "";
            _href = "";
            _id = "";
            _context = "";
        }

        public HtmlLink(string innerText, string href, string id, string context)
        {
            _innerText = innerText;
            _href = href;
            _id = id;
            _context = context;
        }

        public string InnerText
        {
            get { return _innerText; }
            set { _innerText = value; }
        }

        public string Href
        {
            get { return _href; }
            set { _href = value; }
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Context
        {
            get { return _context; }
            set { _context = value; }
        }

        public bool Equals(HtmlLink link)
        {
            bool retVal = false;

            if (link.Context.Equals(_context) && link.Href.Equals(_href) && link.Id.Equals(_id) && link.InnerText.Equals(_innerText))
            {
                retVal = true;
            }

            return retVal;
        }

        public static string NormalizeUrl(string localAuthority, string url)
        {
            string retVal = null;

            if (!url.Contains(localAuthority) && !url.StartsWith("http://"))
            {
                if (!url.StartsWith("http://"))
                {
                    if (!url.StartsWith("/"))
                    {
                        retVal = String.Format("http://{0}/{1}", localAuthority, url);
                    }
                    else
                    {
                        int lastIndexOfSlash = localAuthority.LastIndexOf("/");
                        string domainSubstring = localAuthority.Substring(0, lastIndexOfSlash);
                        retVal = String.Format("{0}{1}", localAuthority, url);
                    }
                }
                else
                {
                    retVal = null;
                }
            }

            return retVal;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vehix.QA.Spider.DealerWebsiteChecker
{
    public class DealerWebSite
    {
        private string _webSite;
        private string _franchiseNumber;
        private string _dealerName;
        private int _responseCode;

        public DealerWebSite()
        {
        }

        public DealerWebSite(string dealerWebSite, string franchiseNumber, string dealerName)
        {
            _dealerName = dealerName;
            _franchiseNumber = franchiseNumber;
            _webSite = dealerWebSite;
            _responseCode = 0;
        }

        public string WebSite
        {
            get { return _webSite; }
            set { _webSite = value; }
        }

        public string FranchiseNumber
        {
            get { return _franchiseNumber; }
            set { _franchiseNumber = value; }
        }

        public string DealerName
        {
            get { return _dealerName; }
            set { _dealerName = value; }
        }

        public int ResponseCode
        {
            get { return _responseCode; }
            set { _responseCode = value; }
        }
    }
}

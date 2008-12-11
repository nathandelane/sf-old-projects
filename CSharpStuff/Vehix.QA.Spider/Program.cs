using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;

namespace Vehix.QA.Spider
{
    public class Program
    {
        private static SpiderSettings _settings;
        private bool _errorFlagged;

        private Program()
        {
            _settings = new SpiderSettings();
            _errorFlagged = false;

            List<DealerWebSite> dealerWebSites = GetAllDealerSiteUrls();
            List<string> visitedWebsites = new List<string>();
            string localPath = Environment.CurrentDirectory;

            try
            {
                string logFileName = String.Format("SpiderLogFile_{0}.{1}.{2}_{3}.{4}.log", DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Hour, DateTime.Today.Minute);
                StreamWriter writer = new StreamWriter(String.Format("{0}\\{1}", localPath, logFileName));

                for (int i = 0; i < dealerWebSites.Count; i++)
                {
                    DealerWebSite d = dealerWebSites[i];

                    if (!d.WebSite.StartsWith("http://"))
                    {
                        d.WebSite = String.Format("http://{0}", d.WebSite);
                    }

                    if (!visitedWebsites.Contains(d.WebSite))
                    {
                        visitedWebsites.Add(d.WebSite);
                        Console.Write("{0}. Testing web site {1} ({2})...", i, d.WebSite, d.DealerName);

                        try
                        {
                            d.ResponseCode = MakeRequest(d.WebSite);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("\nInner exception: {0}.", e.Message);
                            writer.WriteLine("Dealer Name: {0}\nFranchise Number: {1}\nWebSite: {2}\nHttp Response Code/Exception: {3}/{4}\n\n", d.DealerName, d.FranchiseNumber, d.WebSite, d.ResponseCode, e.Message);
                            writer.Flush();
                            _errorFlagged = true;
                        }

                        Console.WriteLine("Response: {0}.", d.ResponseCode);
                    }
                    else
                    {
                        int index = visitedWebsites.IndexOf(d.WebSite);

                        Console.WriteLine("{0}. Not testing {1} for {2}, because it was already tested for {3}.", i, d.WebSite, d.DealerName, dealerWebSites[index].DealerName);
                    }
                }

                writer.Close();

                if (_errorFlagged)
                {
                    Console.WriteLine("\n\nPlease email {0} to {1}.", logFileName, _settings["ErrorContact"]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Outer exception: {0}.", e.Message);
            }
            finally
            {
                
            }
        }

        private static void Main(string[] args)
        {
            new Program();
        }

        private List<DealerWebSite> GetAllDealerSiteUrls()
        {
            string sqlCommand = @"Select Distinct ContactInfo.Website, SellerInfo.FranchiseNumber, Party.Name From  Branding.SellerInfo, Contacts.ContactInfo, People.Party Where Rtrim(SellerInfo.SellerInfoType) = 'Dealer' and Status = 'Active' and ContactInfo.ID = SellerInfo.ContactInfo and Party.ID=SellerInfo.Seller and ContactInfo.Website is not null and ContactInfo.Website <> 'http://'";
            string connectionString = String.Format("Data Source={0}\\{1};Initial Catalog=VehixFoundation;Integrated Security=SSPI", _settings["DatabaseServer"], _settings["DatabaseInstance"]);
            List<DealerWebSite> dealerWebSites = new List<DealerWebSite>();

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(sqlCommand, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DealerWebSite dealer = new DealerWebSite(reader.GetString(0), reader.GetString(1), reader.GetString(2));
                    dealerWebSites.Add(dealer);
                }
            }

            return dealerWebSites;
        }

        private int MakeRequest(string url)
        {
            int result = 0;

            WebRequest webRequest = WebRequest.Create(url);
            webRequest.ImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            result = ParseStatusCode(webResponse.StatusCode);

            return result;
        }

        private int ParseStatusCode(HttpStatusCode statusCode)
        {
            int result = -1;

            switch (statusCode)
            {
                case HttpStatusCode.Continue:
                    result = 100;
                    break;
                case HttpStatusCode.OK:
                    result = 200;
                    break;
                case HttpStatusCode.Created:
                    result = 201;
                    break;
                case HttpStatusCode.Accepted:
                    result = 202;
                    break;
                case HttpStatusCode.Ambiguous:
                    result = 300;
                    break;
                case HttpStatusCode.Found:
                    result = 302;
                    break;
                case HttpStatusCode.NotFound:
                    result = 404;
                    break;
                case HttpStatusCode.BadGateway:
                    result = 502;
                    break;
                case HttpStatusCode.BadRequest:
                    result = 400;
                    break;
                case HttpStatusCode.ExpectationFailed:
                    result = 417;
                    break;
                case HttpStatusCode.Forbidden:
                    result = 403;
                    break;
                case HttpStatusCode.Conflict:
                    result = 407;
                    break;
                case HttpStatusCode.GatewayTimeout:
                    result = 504;
                    break;
                case HttpStatusCode.Gone:
                    result = 410;
                    break;
                case HttpStatusCode.HttpVersionNotSupported:
                    result = 505;
                    break;
                case HttpStatusCode.InternalServerError:
                    result = 500;
                    break;
                case HttpStatusCode.LengthRequired:
                    result = 411;
                    break;
                case HttpStatusCode.MethodNotAllowed:
                    result = 405;
                    break;
                case HttpStatusCode.Moved:
                    result = 301;
                    break;
                case HttpStatusCode.NoContent:
                    result = 204;
                    break;
                case HttpStatusCode.NonAuthoritativeInformation:
                    result = 203;
                    break;
                case HttpStatusCode.NotAcceptable:
                    result = 406;
                    break;
                case HttpStatusCode.NotImplemented:
                    result = 501;
                    break;
                case HttpStatusCode.NotModified:
                    result = 304;
                    break;
                case HttpStatusCode.PartialContent:
                    result = 206;
                    break;
                case HttpStatusCode.PaymentRequired:
                    result = 402;
                    break;
                case HttpStatusCode.PreconditionFailed:
                    result = 412;
                    break;
                case HttpStatusCode.ProxyAuthenticationRequired:
                    result = 407;
                    break;
                case HttpStatusCode.RedirectKeepVerb:
                    result = 307;
                    break;
                case HttpStatusCode.RedirectMethod:
                    result = 303;
                    break;
                case HttpStatusCode.RequestedRangeNotSatisfiable:
                    result = 416;
                    break;
                case HttpStatusCode.RequestEntityTooLarge:
                    result = 413;
                    break;
            }

            return result;
        }
    }
}

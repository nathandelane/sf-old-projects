using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Nathandelane.Net.HttpAnalyzer
{
	internal class AcceptAllCertsPolicy : ICertificatePolicy
	{
		#region Constructors

		public AcceptAllCertsPolicy()
		{
		}

		#endregion

		#region Methods

		#region ICertificatePolicy Members

		public bool CheckValidationResult(ServicePoint srvPoint, X509Certificate certificate, WebRequest request, int certificateProblem)
		{
			return true;
		}

		#endregion

		#endregion
	}
}

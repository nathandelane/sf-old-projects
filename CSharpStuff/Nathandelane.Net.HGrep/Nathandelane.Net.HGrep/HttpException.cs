using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Nathandelane.Net.HGrep
{
	internal class HttpException : Exception
	{
		#region Constructors

		public HttpException()
			: base()
		{
		}

		public HttpException(string message)
			: base(message)
		{
		}

		public HttpException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		public HttpException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		#endregion
	}
}

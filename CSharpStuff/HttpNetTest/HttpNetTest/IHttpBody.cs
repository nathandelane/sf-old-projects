using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HttpNetTest
{
	public interface IHttpBody : ICloneable
	{
		/// <summary>
		/// Gets a value that represents the content-type of the body.
		/// </summary>
		string ContentType { get; }

		/// <summary>
		/// Creates a stream that contains the HTTP body.
		/// </summary>
		/// <param name="request"></param>
		/// <param name="bodyStream"></param>
		void WriteHttpBody(WebTestRequest request, Stream bodyStream);
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.Net.Spider
{
	/// <summary>
	/// This class represents the MIME types that are available in HTTP protocol.
	/// </summary>
	public sealed class ContentType
	{
		#region Fields

		private static readonly IDictionary<string, MimeType> _types = new Dictionary<string, MimeType>()
		{
			{ "application/envoy", MimeType.Application },
			{ "application/fractals", MimeType.Application },
			{ "application/futuresplash", MimeType.Application },
			{ "application/hta", MimeType.Application },
			{ "application/internet-poperty-stream", MimeType.Application },
			{ "application/mac-binhex40", MimeType.Application },
			{ "application/msword", MimeType.Application },
			{ "application/octet-stream", MimeType.Application },
			{ "application/oda", MimeType.Application },
			{ "application/olescript", MimeType.Application },
			{ "application/pdf", MimeType.Application },
			{ "application/pics-rules", MimeType.Application },
			{ "application/pkcs10", MimeType.Application },
			{ "application/pkix-crl", MimeType.Application },
			{ "application/rtf", MimeType.Application },
			{ "application/set-payment-initiation", MimeType.Application },
			{ "application/set-registration-initiation", MimeType.Application },
			{ "application/vnd.ms-excel", MimeType.Application },
			{ "application/vnd.ms-outlook", MimeType.Application },
			{ "application/vnd.ms-pkicertstore", MimeType.Application },
			{ "application/vnd.ms-pkiseccat", MimeType.Application },
			{ "application/vnd.pkistl", MimeType.Application },
			{ "application/vnd.ms-powerpoint", MimeType.Application },
			{ "application/vnd.ms-project", MimeType.Application },
			{ "application/vnd.ms-works", MimeType.Application },
			{ "application/winhlp", MimeType.Application },
			{ "application/x-bcpio", MimeType.Application },
			{ "application/x-cdf", MimeType.Application },
			{ "application/x-compress", MimeType.Application },
			{ "application/x-compressed", MimeType.Application },
			{ "application/x-cpio", MimeType.Application },
			{ "application/x-csh", MimeType.Application },
			{ "application/x-director", MimeType.Application },
			{ "application/x-dvi", MimeType.Application },
			{ "application/x-gtar", MimeType.Application },
			{ "application/x-gzip", MimeType.Application },
			{ "application/x-hdf", MimeType.Application },
			{ "application/x-internet-signup", MimeType.Application },
			{ "application/x-iphone", MimeType.Application },
			{ "application/x-javascript", MimeType.Application },
			{ "application/x-latex", MimeType.Application },
			{ "application/x-msaccess", MimeType.Application },
			{ "application/x-mscardfile", MimeType.Application },
			{ "application/x-msclip", MimeType.Application },
			{ "application/x-msdownload", MimeType.Application },
			{ "application/x-msmediaview", MimeType.Application },
			{ "application/x-msmetafile", MimeType.Application },
			{ "application/x-msmoney", MimeType.Application },
			{ "application/x-mspublisher", MimeType.Application },
			{ "application/x-msschedule", MimeType.Application },
			{ "application/x-msterminal", MimeType.Application },
			{ "application/x-mswrite", MimeType.Application },
			{ "application/x-netcdf", MimeType.Application },
			{ "application/x-perfmon", MimeType.Application },
			{ "application/x-pkcs12", MimeType.Application },
			{ "application/x-pkcs7-certificates", MimeType.Application },
			{ "application/x-pkcs7-certreqresp", MimeType.Application },
			{ "application/x-pkcs7-mime", MimeType.Application },
			{ "application/x-pkcs7-signature", MimeType.Application },
			{ "application/x-sh", MimeType.Application },
			{ "application/x-shar", MimeType.Application },
			{ "application/x-shockwave-flash", MimeType.Application },
			{ "application/x-stuffit", MimeType.Application },
			{ "application/x-sv4cpio", MimeType.Application },
			{ "application/x-sv4crc", MimeType.Application },
			{ "application/x-tar", MimeType.Application },
			{ "application/x-tcl", MimeType.Application },
			{ "application/x-tex", MimeType.Application },
			{ "application/x-texinfo", MimeType.Application },
			{ "application/x-troff", MimeType.Application },
			{ "application/x-troff-man", MimeType.Application },
			{ "application/x-troff-me", MimeType.Application },
			{ "application/x-troff-ms", MimeType.Application },
			{ "application/x-ustar", MimeType.Application },
			{ "application/x-wais-source", MimeType.Application },
			{ "application/x-x509-ca-cert", MimeType.Application },
			{ "application/ynd.ms-pkipko", MimeType.Application },
			{ "application/zip", MimeType.Application },
			{ "audio/basic", MimeType.Audio },
			{ "audio/mid", MimeType.Audio },
			{ "audio/mpeg", MimeType.Audio },
			{ "audio/x-aiff", MimeType.Audio },
			{ "audio/x-mpegurl", MimeType.Audio },
			{ "audio/x-pn-realaudio", MimeType.Audio },
			{ "audio/x-wav", MimeType.Audio },
			{ "image/bmp", MimeType.Image },
			{ "image/cis-cod", MimeType.Image },
			{ "image/gif", MimeType.Image },
			{ "image/ief", MimeType.Image },
			{ "image/jpeg", MimeType.Image },
			{ "image/pipeg", MimeType.Image },
			{ "image/svg+xml", MimeType.Image },
			{ "image/tiff", MimeType.Image },
			{ "image/x-cmu-raster", MimeType.Image },
			{ "image/x-cmx", MimeType.Image },
			{ "image/x-icon", MimeType.Image },
			{ "image/x-portable-anymap", MimeType.Image },
			{ "image/x-portable-bitmap", MimeType.Image },
			{ "image/x-portable-graymap", MimeType.Image },
			{ "image/x-portable-pixmap", MimeType.Image },
			{ "image/x-rgb", MimeType.Image },
			{ "image/x-xbitmap", MimeType.Image },
			{ "image/x-xpixmap", MimeType.Image },
			{ "image/x-xwindowdump", MimeType.Image },
			{ "message/rfc822", MimeType.Message },
			{ "text/css", MimeType.Text },
			{ "text/h323", MimeType.Text },
			{ "text/html", MimeType.Text },
			{ "text/iuls", MimeType.Text },
			{ "text/plain", MimeType.Text },
			{ "text/richtext", MimeType.Text },
			{ "text/scriptlet", MimeType.Text },
			{ "text/tab-separated-values", MimeType.Text },
			{ "text/webviewhtml", MimeType.Text },
			{ "text/x-component", MimeType.Text },
			{ "text/x-setext", MimeType.Text },
			{ "text/vcard", MimeType.Text },
			{ "video/mpeg", MimeType.Video },
			{ "video/quicktime", MimeType.Video },
			{ "video/x-la-asf", MimeType.Video },
			{ "video/x-ms-asf", MimeType.Video },
			{ "video/x-msvideo", MimeType.Video },
			{ "video/x-sgi-movie", MimeType.Video },
			{ "x-world/x-vrml", MimeType.XWorld }
		};

		#endregion

		#region Methods

		/// <summary>
		/// Gets a MIME Type for a given content type. Defaults to Text for an undefined content type.
		/// </summary>
		/// <param name="contentType"></param>
		/// <returns></returns>
		public static MimeType GetMimeType(string contentType)
		{
			MimeType resultMimeType = MimeType.Text;

			if (_types.ContainsKey(contentType))
			{
				resultMimeType = _types[contentType];
			}

			return resultMimeType;
		}

		#endregion
	}
}

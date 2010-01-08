using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Nathandelane.Net.Spider
{
	public class SpiderUrl
	{
		#region Fields

		private Uri _target;
		private string _referrer;
		private bool _isImage;

		#endregion

		#region Properties

		public Uri Target
		{
			get { return _target; }
		}

		public string Referrer
		{
			get { return _referrer; }
		}

		public bool IsImage
		{
			get { return _isImage; }
		}

		public bool IsDocument
		{
			get
			{
				bool result = false;

				if (_target.ToString().ToLower().Contains(".doc") || _target.ToString().ToLower().Contains(".pdf") || _target.ToString().ToLower().Contains(".xls"))
				{
					result = true;
				}

				return result;
			}
		}

		public bool IsJavascript
		{
			get
			{
				return _target.ToString().ToLower().Contains("javascript");
			}
		}

		public bool IsMailto
		{
			get
			{
				return _target.ToString().ToLower().Contains("mailto");
			}
		}

		#endregion

		#region Constructors

		public SpiderUrl(Uri target, string referrer)
		{
			_target = target;
			_referrer = referrer;

			if (target.ToString().EndsWith("gif") || target.ToString().EndsWith("jpg") || target.ToString().EndsWith("jpeg") || target.ToString().EndsWith("bmp") || target.ToString().EndsWith("png"))
			{
				_isImage = true;
			}
			else
			{
				_isImage = false;
			}

			SanitizeTarget();
		}

		#endregion

		#region Private Members

		private void SanitizeTarget()
		{
			string strTarget = _target.ToString();

			strTarget = strTarget.Replace("&amp;", "&");

			if (!strTarget.StartsWith("http://") && !strTarget.StartsWith("https://"))
			{
				if (strTarget.StartsWith("/"))
				{
					strTarget = String.Concat(ConfigurationManager.AppSettings["startingUrl"], _target);
				}
				else
				{
					string relativeLocation = _referrer.Substring(0, (_referrer.LastIndexOf('/') + 1));

					strTarget = String.Concat(relativeLocation, _target);
				}
			}

			if (!Uri.TryCreate(strTarget, UriKind.Absolute, out _target))
			{
				throw new Exception(String.Format("Malformed Uri: {0}", strTarget));
			}
		}

		#endregion

		#region Override Methods

		public override string ToString()
		{
			return String.Format("Target={0};Referrer={1};IsImage={2}", Target, Referrer, IsImage);
		}

		#endregion
	}
}

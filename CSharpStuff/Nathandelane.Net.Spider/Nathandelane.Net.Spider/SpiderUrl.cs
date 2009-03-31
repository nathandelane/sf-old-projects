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

		private string _target;
		private string _referrer;
		private bool _isImage;

		#endregion

		#region Properties

		public string Target
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

				if (_target.ToLower().Contains(".doc") || _target.ToLower().Contains(".pdf") || _target.ToLower().Contains(".xls"))
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
				return _target.ToLower().Contains("javascript");
			}
		}

		public bool IsMailto
		{
			get
			{
				return _target.ToLower().Contains("mailto");
			}
		}

		#endregion

		#region Constructors

		public SpiderUrl(string target, string referrer)
		{
			_target = target;
			_referrer = referrer;

			if (target.EndsWith("gif") || target.EndsWith("jpg") || target.EndsWith("jpeg") || target.EndsWith("bmp") || target.EndsWith("png"))
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
			_target = _target.Replace("&amp;", "&");

			if (!_target.StartsWith("http://") && !_target.StartsWith("https://"))
			{
				if (_target.StartsWith("/"))
				{
					_target = String.Concat(ConfigurationManager.AppSettings["startingUrl"], _target);
				}
				else if (_target.ToLower().StartsWith("../../../"))
				{
					string relativeLocation = _referrer.Substring(0, (_referrer.LastIndexOf('/')));

					relativeLocation = relativeLocation.Substring(0, (relativeLocation.LastIndexOf('/')));
					_target = _target.Substring("../../../".Length);
					_target = String.Concat(relativeLocation, "/", _target);
				}
				else if (_target.ToLower().StartsWith("../../"))
				{
					string relativeLocation = _referrer.Substring(0, (_referrer.LastIndexOf('/')));

					relativeLocation = relativeLocation.Substring(0, (relativeLocation.LastIndexOf('/')));
					_target = _target.Substring("../../".Length);
					_target = String.Concat(relativeLocation, "/", _target);
				}
				else if (_target.StartsWith("../"))
				{
					string relativeLocation = _referrer.Substring(0, (_referrer.LastIndexOf('/')));

					relativeLocation = relativeLocation.Substring(0, (relativeLocation.LastIndexOf('/')));
					_target = _target.Substring("../".Length);
					_target = String.Concat(relativeLocation, "/", _target);
				}
				else
				{
					string relativeLocation = _referrer.Substring(0, (_referrer.LastIndexOf('/') + 1));

					_target = String.Concat(relativeLocation, _target);
				}
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

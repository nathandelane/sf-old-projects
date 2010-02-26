using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.Net.Spider
{
	public class SpiderUrl
	{
		#region Fields

		private Uri _target;
		private string _referrer;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the Target Url.
		/// </summary>
		public Uri Target
		{
			get { return _target; }
		}

		/// <summary>
		/// Gets the Url referrer.
		/// </summary>
		public string Referrer
		{
			get { return _referrer; }
		}

		/// <summary>
		/// Gets whether the Url refers to an image.
		/// </summary>
		public bool IsImage
		{
			get
			{
				bool result = IsUrlOfKnownType(_target.ToString(), ConfigurationManager.AppSettings["imageFileExtensions"]);

				return result;
			}
		}

		/// <summary>
		/// Gets whether the Url refers to a file type that should be ignored.
		/// </summary>
		public bool IsIgnoredFileType
		{
			get
			{
				bool result = IsUrlOfKnownType(_target.ToString(), ConfigurationManager.AppSettings["fileExtensionsToIgnore"]);

				return result;
			}
		}

		/// <summary>
		/// Gets whether the Url refers to some javascript.
		/// </summary>
		public bool IsJavascript
		{
			get
			{
				return _target.ToString().ToLower().Contains("javascript");
			}
		}

		/// <summary>
		/// Gets whether the Url refers to a mailto: link.
		/// </summary>
		public bool IsMailto
		{
			get
			{
				return _target.ToString().ToLower().Contains("mailto");
			}
		}

		/// <summary>
		/// Gets whether the Url refers to a relative link.
		/// </summary>
		public bool IsPageRelative
		{
			get
			{
				return _target.ToString().ToLower().Contains("#");
			}
		}

		/// <summary>
		/// Gets whether the Url is a Url that we want to crawl.
		/// </summary>
		public bool IsDesirable
		{
			get
			{
				bool isNotDesirable = (IsIgnoredFileType || IsJavascript || IsMailto || IsPageRelative);

				return !isNotDesirable;
			}
		}

		#endregion

		#region Constructors

		public SpiderUrl(Uri target, string referrer)
		{
			_target = target;
			_referrer = referrer;

			SanitizeTarget();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Sanaitizes Urls so that they have proper tags or domains.
		/// </summary>
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

		/// <summary>
		/// Determines whether the passed Url falls into a known configured type.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="appSetting"></param>
		/// <returns></returns>
		private bool IsUrlOfKnownType(string url, string appSetting)
		{
			bool result = false;
			Regex regex = new Regex(String.Format("({0}){{1}}", appSetting), RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

			if (regex.IsMatch(url))
			{
				result = true;
			}

			return result;
		}

		/// <summary>
		/// Returns a string representation of this SpiderUrl.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return String.Format("Target={0}; Referrer={1}; IsImage={2}", Target, Referrer, IsImage);
		}

		#endregion
	}
}

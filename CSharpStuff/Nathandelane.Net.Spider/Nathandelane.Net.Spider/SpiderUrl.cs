using System;
using System.Configuration;
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

		private string _target;
		private string _referrer;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the Target Url.
		/// </summary>
		public Uri Target
		{
			get
			{
				Uri resultantTarget = null;

				if (!Uri.TryCreate(_target, UriKind.Absolute, out resultantTarget))
				{
					if (_target.StartsWith("/", StringComparison.InvariantCultureIgnoreCase))
					{
						_target = String.Format("{0}{1}", ConfigurationManager.AppSettings["startingUrl"], _target);
					}
					else
					{
						int lastIndexOfPathSeparator = _referrer.LastIndexOf("/", StringComparison.InvariantCultureIgnoreCase);
						string newReferrer = _referrer.Substring(0, lastIndexOfPathSeparator);

						_target = String.Format("{0}/{1}", newReferrer, _target);
					}

					Uri.TryCreate(_target, UriKind.Absolute, out resultantTarget);
				}

				return resultantTarget;
			}
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

		public SpiderUrl(string target, string referrer)
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
			_target = _target.Replace("&amp;", "&");
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

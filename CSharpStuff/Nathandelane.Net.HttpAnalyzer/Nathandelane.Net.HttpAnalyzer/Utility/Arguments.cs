using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.Net.HttpAnalyzer.Utility
{
	public class Arguments
	{
		#region Fields

		private static readonly List<string> __allowedArguments = new List<string>()
		{
			String.Empty,
			"attributes",
			"cookies",
			"data",
			"find",
			"headers",
			"help",
			"ignore-bad-certs",
			"no-attributes",
			"no-innerhtml",
			"proxy",
			"suppress",
			"scrub",
			"uri"
		};

		private static Dictionary<string, string> __parameters;

		#endregion

		#region Properties

		/// <summary>
		/// Gets a parameter by name.
		/// </summary>
		/// <param name="parameterName"></param>
		/// <returns></returns>
		public string this[string parameterName]
		{
			get { return __parameters[parameterName]; }
		}

		#endregion

		#region Constructors

		private Arguments()
		{
			__parameters = new Dictionary<string, string>();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Parses an argument list passed in as an array of strings.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static Arguments Parse(string[] args)
		{
			Arguments argumentsObject = new Arguments();
			Regex splitter = new Regex(@"^-{1,2}|^/|=|:", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			Regex remover = new Regex(@"^['""]?(.*?)['""]?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			string nextParameterName = String.Empty;
			string[] parameterParts;

			foreach (string parameter in args)
			{
				parameterParts = splitter.Split(parameter, 3);

				if (parameterParts.Length == 1)
				{
					if (!String.IsNullOrEmpty(nextParameterName))
					{
						if (!__parameters.ContainsKey(nextParameterName))
						{
							parameterParts[0] = remover.Replace(parameterParts[0], "$1");

							__parameters.Add(nextParameterName, parameterParts[0]);
						}
					}
				}
				else if (parameterParts.Length == 2)
				{
					if (!String.IsNullOrEmpty(nextParameterName))
					{
						if (!__parameters.ContainsKey(nextParameterName))
						{
							__parameters.Add(nextParameterName, "true");
						}
					}

					nextParameterName = parameterParts[1];

					if (!IsValidArgument(nextParameterName))
					{
						Console.WriteLine("Warning! {0} is not recognized as a valid argument", nextParameterName);
					}
				}
				else if (parameterParts.Length == 3)
				{
					if (!String.IsNullOrEmpty(nextParameterName))
					{
						if (!__parameters.ContainsKey(nextParameterName))
						{
							__parameters.Add(nextParameterName, "true");
						}
					}

					nextParameterName = parameterParts[1];

					if (!IsValidArgument(nextParameterName))
					{
						Console.WriteLine("Warning! {0} is not recognized as a valid argument", nextParameterName);
					}

					if (!__parameters.ContainsKey(nextParameterName))
					{
						parameterParts[2] = remover.Replace(parameterParts[2], "$1");

						__parameters.Add(nextParameterName, parameterParts[2]);
					}

					nextParameterName = String.Empty;
				}

				if (!String.IsNullOrEmpty(nextParameterName))
				{
					if (!__parameters.ContainsKey(nextParameterName))
					{
						__parameters.Add(nextParameterName, "true");
					}
				}
			}

			return argumentsObject;
		}

		/// <summary>
		/// Returns whether the collection of arguments contains a specific parameter.
		/// </summary>
		/// <param name="parameterName">Name of the parameter to find</param>
		/// <returns></returns>
		public bool Contains(string parameterName)
		{
			return __parameters.ContainsKey(parameterName);
		}

		/// <summary>
		/// Returns whether the argument is contained in the allowed arguments list.
		/// </summary>
		/// <param name="key">Parameter Name</param>
		/// <returns></returns>
		private static bool IsValidArgument(string key)
		{
			bool result = false;

			if (__allowedArguments.Contains(key))
			{
				result = true;
			}

			return result;
		}

		#endregion
	}
}

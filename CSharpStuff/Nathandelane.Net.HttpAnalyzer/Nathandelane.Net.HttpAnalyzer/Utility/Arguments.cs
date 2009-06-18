using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.Net.HttpAnalyzer.Utility
{
	public class Arguments
	{
		#region Fields

		private static Dictionary<string, string> _parameters;

		#endregion

		#region Properties

		/// <summary>
		/// Gets a parameter by name.
		/// </summary>
		/// <param name="parameterName"></param>
		/// <returns></returns>
		public string this[string parameterName]
		{
			get { return _parameters[parameterName]; }
		}

		#endregion

		#region Constructors

		private Arguments()
		{
			_parameters = new Dictionary<string, string>();
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
						if (!_parameters.ContainsKey(nextParameterName))
						{
							parameterParts[0] = remover.Replace(parameterParts[0], "$1");

							_parameters.Add(nextParameterName, parameterParts[0]);
						}
					}
				}
				else if (parameterParts.Length == 2)
				{
					if (!String.IsNullOrEmpty(nextParameterName))
					{
						if (!_parameters.ContainsKey(nextParameterName))
						{
							_parameters.Add(nextParameterName, "true");
						}
					}

					nextParameterName = parameterParts[1];
				}
				else if (parameterParts.Length == 3)
				{
					if (!String.IsNullOrEmpty(nextParameterName))
					{
						if (_parameters.ContainsKey(nextParameterName))
						{
							_parameters.Add(nextParameterName, "true");
						}
					}

					nextParameterName = parameterParts[1];

					if (!_parameters.ContainsKey(nextParameterName))
					{
						parameterParts[2] = remover.Replace(parameterParts[2], "$1");

						_parameters.Add(nextParameterName, parameterParts[2]);
					}

					nextParameterName = String.Empty;
				}

				if (!String.IsNullOrEmpty(nextParameterName))
				{
					if (_parameters.ContainsKey(nextParameterName))
					{
						_parameters.Add(nextParameterName, "true");
					}
				}
			}

			return argumentsObject;
		}

		/// <summary>
		/// Returns whether the collection of arguments contains a specific parameter.
		/// </summary>
		/// <param name="parameterName"></param>
		/// <returns></returns>
		public bool Contains(string parameterName)
		{
			return _parameters.ContainsKey(parameterName);
		}

		#endregion
	}
}

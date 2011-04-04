/*
 * HttpGrep - Tool for grepping the Internet
 * Copyright (C) 2011 Nathan Lane, Nathandelane.com
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using Nathandelane.System.ClassExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.Net.HttpGrep
{
	/// <summary>
	/// This class represents the current context of the HTTP Grep session.
	/// </summary>
	public class Context
	{
		#region Fields

		public const string Url = "Url";
		public const string Help = "Help";
		public const string Find = "Find";
		public const string Request = "Request";
		public const string Response = "Response";
		public const string Data = "Data";
		public const string Post = "Post";
		public const string Put = "Put";
		public const string Proxy = "Proxy";
		public const string IgnoreBadCerts = "Ignorebadcerts";
		public const string Method = "Method";
		public const string NoHeaders = "Noheaders";
		public const string PsHashes = "Pshashes";
		public const string PostContentType = "Postcontenttype";
		public const string BasicAuth = "Basicauth";

		public static string GeneralHelp = "Usage: HttpGrep <url> [<options>]" + Environment.NewLine +
			"Options (specifying no options returns the response headers):" + Environment.NewLine +
			"-Help                    Displays this help message." + Environment.NewLine +
			"-Url=<url>               Sets the URL for the current GREP operation. Same as <url>." + Environment.NewLine +
			"-Find=<xpath>            Uses XPath to locate certain elements in the resultant document." + Environment.NewLine +
			"-Request                 Displays the request headers." + Environment.NewLine +
			"-Response                Displays the response headers." + Environment.NewLine +
			"-Data                    Displays the response body." + Environment.NewLine +
			"-Post                    Sets the request mode to post and puts HTTP Grep into interactive mode to set the post-body." + Environment.NewLine +
			"-Put                     Sets the request mode to pput and puts HTTP Grep into interactive mode to set the put-body." + Environment.NewLine +
			"-Proxy=<url>             Sets the HTTP proxy for use with this session." + Environment.NewLine +
			"-IgnoreBadCerts          Ignores bad SSL certificates when they are encountered." + Environment.NewLine +
			"-Method=<method>         Method may be POST, GET, HEAD, PUT, OPTIONS, DELETE, TRACE, CONNECT, extension-method=token. GET is the default." + Environment.NewLine +
			"-NoHeaders               Output has no headers." + Environment.NewLine +
			"-PsHashes                Outputs Find results as Powershell hashes." + Environment.NewLine +
			"-PostContentType=<type>  Sets the content type for the post body." + Environment.NewLine +
			"-BasicAuth=<name:pass>   Sets basic authentication credentials when required." + Environment.NewLine + Environment.NewLine;

		private static Context __instance;
		private static Dictionary<string, Regex> __allowedArguments = new Dictionary<string, Regex>()
		{
			{ Context.Url, new Regex("^(http|https){1}(://)", RegexOptions.CultureInvariant | RegexOptions.Compiled) },
			{ Context.Help, null },
			{ Context.Find, new Regex("^(/|\\.|@|\\*){1}([a-zA-Z/]+)",  RegexOptions.CultureInvariant | RegexOptions.Compiled) },
			{ Context.Request, null },
			{ Context.Response, null },
			{ Context.Data, null },
			{ Context.Post, null },
			{ Context.Put, null },
			{ Context.Proxy, new Regex("[\\w\\d:#@%/;$()~_?\\+-=\\\\.&]*", RegexOptions.CultureInvariant | RegexOptions.Compiled) },
			{ Context.IgnoreBadCerts, null },
			{ Context.Method, new Regex("^((get|post|head|options|put|delete|trace|connect){1}|(extension-method)=([a-z\\d-]+))$") },
			{ Context.NoHeaders, null },
			{ Context.PsHashes, null },
			{ Context.PostContentType, new Regex("^\\w+/(\\w)(([-+;=\\s]){0,1}|\\w)*$") },
			{ Context.BasicAuth, new Regex("^([A-Za-z\\d!@#$%^&*()+-_=<>,.?/\\\\s]+):([A-Za-z\\d!@#$%^&*()+-_=<>,.?/\\\\s]+)$") }
		};

		private Dictionary<string, string> _actualArguments;

		#endregion

		#region Properties

		/// <summary>
		/// Gets a context value.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string this[string key]
		{
			get
			{
				string retVal = null;

				if (_actualArguments.ContainsKey(key))
				{
					retVal = _actualArguments[key];
				}

				return retVal;
			}
		}

		/// <summary>
		/// Gets the number of arguments in the context.
		/// </summary>
		public int ArgCount
		{
			get
			{
				return _actualArguments.Count;
			}
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Private constructor so that context is protected from being instantiated.
		/// Use getContext instead.
		/// </summary>
		/// <param name="args"></param>
		private Context(string[] args)
		{
			_actualArguments = new Dictionary<string, string>();

			for (int argIndex = 0; argIndex < args.Length; argIndex++)
			{
				string nextArgument = args[argIndex];

				if (nextArgument.StartsWith("-", StringComparison.InvariantCultureIgnoreCase))
				{
					nextArgument = nextArgument.Substring(1);
				}

				nextArgument = nextArgument.Capitalize();

				if (Context.__allowedArguments[Context.Url].IsMatch(nextArgument) && argIndex == 0)
				{
					_actualArguments[Context.Url] = nextArgument;
				}
				else
				{
					if (nextArgument.IndexOf("=", StringComparison.InvariantCultureIgnoreCase) > -1)
					{
						int indexOfEquals = nextArgument.IndexOf("=", StringComparison.InvariantCultureIgnoreCase);
						string argument = nextArgument.Substring(0, indexOfEquals).Capitalize();

						if (Context.__allowedArguments.ContainsKey(argument))
						{
							if (Context.__allowedArguments[argument] != null)
							{
								string value = nextArgument.Substring((indexOfEquals + 1));
								Regex regex = Context.__allowedArguments[argument];

								if (regex.IsMatch(value))
								{
									_actualArguments[argument] = value;
								}
								else
								{
									throw new ArgumentException(String.Format("Value for argument {0} was not of the expected format {1}.", argument, regex.ToString()));
								}
							}
							else
							{
								throw new ArgumentException(String.Format("The argument {0} expects a value that was not supplied.", argument));
							}
						}
						else
						{
							throw new ArgumentException(String.Format("The argument {0} is not recognized as a valid argument.", argument));
						}
					}
					else
					{
						string argument = nextArgument;

						if (Context.__allowedArguments.ContainsKey(argument))
						{
							if (Context.__allowedArguments[argument] == null)
							{
								if (argument.Equals("Post", StringComparison.InvariantCultureIgnoreCase) || argument.Equals("Put", StringComparison.InvariantCultureIgnoreCase))
								{
									ConsoleKeyInfo userInput;
									string inputBody = String.Empty;

									Console.WriteLine("Type the text you'd like for the {0} body, then press Ctrl+D:", argument);

									while ((userInput = Console.ReadKey()) != null && !(userInput.Modifiers == ConsoleModifiers.Control && userInput.Key == ConsoleKey.D))
									{
										inputBody += userInput.KeyChar;
									}

									_actualArguments[argument] = inputBody;
								}
								else
								{
									_actualArguments[argument] = null;
								}
							}
							else
							{
								throw new ArgumentException(String.Format("The argument {0} expects a value that was not supplied.", argument));
							}
						}
						else
						{
							throw new ArgumentException(String.Format("The argument {0} is not recognized as a valid argument.", argument));
						}
					}
				}
			}
		}

		#endregion

		#region Methods

		public bool ArgumentIsDefined(string argument)
		{
			bool result = false;

			if (_actualArguments.ContainsKey(argument))
			{
				result = true;
			}

			return result;
		}

		#endregion

		#region Static Methods

		/// <summary>
		/// Gets a reference to the context.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static Context GetContext()
		{
			return Context.__instance;
		}

		/// <summary>
		/// Sets the instance of the Context with the arguments supplied.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static Context SetContext(string[] args)
		{
			if (Context.__instance == null)
			{
				Context.__instance = new Context(args);
			}

			return Context.__instance;
		}

		#endregion
	}
}

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
		public const string Proxy = "Proxy";

		public static string GeneralHelp = "Usage: HttpGrep <url> [<options>]" + Environment.NewLine +
			"Options (specifying no options returns the response headers):" + Environment.NewLine +
			"-Help                    Displays this help message." + Environment.NewLine +
			"-Url=<url>               Sets the URL for the current GREP operation. Same as <url>." + Environment.NewLine +
			"-Find=<xpath>            Uses XPath to locate certain elements in the resultant document." + Environment.NewLine +
			"-Request                 Displays the request headers." + Environment.NewLine +
			"-Response                Displays the response headers." + Environment.NewLine +
			"-Data                    Displays the response body." + Environment.NewLine +
			"-Post                    Sets the request mode to post and puts HTTP Grep into interactive mode to set the post body." + Environment.NewLine +
			"-Proxy=<url>             Sets the HTTP proxy for use with this session." + Environment.NewLine + Environment.NewLine;

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
			{ Context.Proxy, new Regex("[\\w\\d:#@%/;$()~_?\\+-=\\\\.&]*", RegexOptions.CultureInvariant | RegexOptions.Compiled) }
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
					if (nextArgument.Contains('='))
					{
						string[] parts = nextArgument.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
						string argument = parts[0].Capitalize();

						if (Context.__allowedArguments.ContainsKey(argument))
						{
							if (Context.__allowedArguments[argument] != null)
							{
								string value = (parts.Length > 1) ? parts[1] : null;
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
								if(argument.Equals("Post", StringComparison.InvariantCultureIgnoreCase))
								{
									ConsoleKeyInfo userInput;
									string inputBody = String.Empty;

									while ((userInput = Console.ReadKey()) != null && !(userInput.Modifiers == ConsoleModifiers.Control && userInput.Key == ConsoleKey.D))
									{
										inputBody += userInput.KeyChar;
									}

									_actualArguments[Context.Post] = inputBody;
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

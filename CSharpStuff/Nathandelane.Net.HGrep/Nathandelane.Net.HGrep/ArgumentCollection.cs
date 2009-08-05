using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.Net.HGrep
{
	public class ArgumentCollection : Dictionary<string, object>
	{
		#region Fields

		public static readonly string CountOnlyArg = "count-only";
		public static readonly string FindArg = "find";
		public static readonly string FindRegexpArg = "find-regexp";
		public static readonly string HelpArg = "help";
		public static readonly string IgnoreBadCertsArg = "ignore-bad-certs";
		public static readonly string NoAttributesArg = "no-attributes";
		public static readonly string NoInnerHtmlArg = "no-inner-html";
		public static readonly string NoNumberingArg = "no-numbering";
		public static readonly string PostBodyArg = "post-body";
		public static readonly string ReturnAttributesArg = "return-attributes";
		public static readonly string ReturnDataArg = "return-data";
		public static readonly string ReturnHeadersArg = "return-headers";
		public static readonly string ReturnUrlArg = "return-url";
		public static readonly string ScrubArg = "scrub";
		public static readonly string UriArg = "uri";
		public static readonly string VersionArg = "version";

		private static readonly Dictionary<string, ArgumentType> __map = new Dictionary<string, ArgumentType>()
		{
			{ CountOnlyArg, ArgumentType.Null },
			{ FindArg, ArgumentType.String },
			{ FindRegexpArg, ArgumentType.String },
			{ HelpArg, ArgumentType.String },
			{ IgnoreBadCertsArg, ArgumentType.Null },
			{ NoAttributesArg, ArgumentType.Null },
			{ NoInnerHtmlArg, ArgumentType.Null },
			{ NoNumberingArg, ArgumentType.Null },
			{ PostBodyArg, ArgumentType.String },
			{ ReturnAttributesArg, ArgumentType.StringArray },
			{ ReturnDataArg, ArgumentType.Null },
			{ ReturnHeadersArg, ArgumentType.Null },
			{ ReturnUrlArg, ArgumentType.Null },
			{ ScrubArg, ArgumentType.Null },
			{ UriArg, ArgumentType.String },
			{ VersionArg, ArgumentType.Null }
		};

		#endregion

		#region Constructors

		private ArgumentCollection()
			: base()
		{
		}

		#endregion

		#region Methods

		public static ArgumentCollection Parse(string[] args)
		{
			ArgumentCollection collection = new ArgumentCollection();

			foreach (string nextArg in args)
			{
				string currentArg = StripArgumentIdentifyer(nextArg);
				string argName = String.Empty;
				object argValue = null;

				if (currentArg.Contains("="))
				{
					int indexOfEqualsSign = currentArg.IndexOf("=");
					argName = currentArg.Substring(0, indexOfEqualsSign);

					if (__map.ContainsKey(argName))
					{
						if (__map[argName] == ArgumentType.Null)
						{
							argValue = null;
						}
						else if (__map[argName] == ArgumentType.String)
						{
							argValue = currentArg.Substring(indexOfEqualsSign + 1);
						}
						else if (__map[argName] == ArgumentType.StringArray)
						{
							argValue = currentArg.Substring(indexOfEqualsSign + 1).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
						}
					}
					else
					{
						throw new ArgumentException(String.Format("{0} is not recognized as a valid argument", argName));
					}
				}
				else
				{
					argName = currentArg;
				}

				collection.Add(argName, argValue);
			}

			return collection;
		}

		private static string StripArgumentIdentifyer(string arg)
		{
			if (arg.StartsWith("--"))
			{
				arg = arg.Substring(2);
			}
			else if (arg.StartsWith("-"))
			{
				arg = arg.Substring(1);
			}
			else if (arg.StartsWith("/"))
			{
				arg = arg.Substring(1);
			}

			return arg;
		}

		#endregion
	}
}

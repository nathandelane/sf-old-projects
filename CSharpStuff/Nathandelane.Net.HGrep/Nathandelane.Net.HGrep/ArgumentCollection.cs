using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.Net.HGrep
{
	public class ArgumentCollection : Dictionary<string, object>
	{
		#region Fields

		private static readonly Dictionary<string, ArgumentType> __map = new Dictionary<string, ArgumentType>()
		{
			{ "uri", ArgumentType.String },
			{ "find", ArgumentType.String },
			{ "help", ArgumentType.Null },
			{ "return-attributes", ArgumentType.StringArray },
			{ "return-headers", ArgumentType.Null }
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
							argValue = currentArg.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
						}

						collection.Add(argName, argValue);
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
			else
			{
				arg = arg;
			}

			return arg;
		}

		#endregion
	}
}

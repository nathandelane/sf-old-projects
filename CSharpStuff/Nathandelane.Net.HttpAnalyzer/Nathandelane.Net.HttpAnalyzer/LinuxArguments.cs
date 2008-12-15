using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.Net.HttpAnalyzer
{
	public class LinuxArguments
	{
		#region Fields

		private Dictionary<string, object> _args;
		private Dictionary<string, CommandLineArgument> _argMap;

		#endregion

		#region Properties

		public string[] Args
		{
			set { _args = ParseArguments(value); }
		}

		public Dictionary<string, CommandLineArgument> ArgMap
		{
			set { _argMap = value; }
		}

		public object this[string key]
		{
			get { return _args[key]; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Default Constructor
		/// </summary>
		public LinuxArguments()
		{
			_args = new Dictionary<string, object>();
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="args"></param>
		public LinuxArguments(string[] args)
		{
			_args = ParseArguments(args);
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="args"></param>
		/// <param name="argMap"></param>
		public LinuxArguments(string[] args, Dictionary<string, CommandLineArgument> argMap)
		{
			_argMap = argMap;
			_args = ParseArguments(args);
		}

		#endregion

		public bool IsDefined(string key)
		{
			return _args.ContainsKey(key);
		}

		#region Private Methods

		/// <summary>
		/// Parse the arguments into usable bits
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		private Dictionary<string, object> ParseArguments(string[] args)
		{
			_args = new Dictionary<string, object>();
			List<string> keys = new List<string>();
			List<object> values = new List<object>();
			string lastKey = String.Empty;

			foreach (string arg in args)
			{
				if (arg.StartsWith("--"))
				{
					int index = arg.LastIndexOf("--") + 2;
					string argS = arg.Substring(index);
					keys.Add(argS);
					lastKey = argS;
				}
				else if (arg.StartsWith("-"))
				{
					int index = arg.LastIndexOf('-') + 1;
					string argS = arg.Substring(index);
					keys.Add(argS);
					lastKey = argS;
				}
				else
				{
					values.Add(Preprocess(arg, lastKey));
				}
			}

			for (int i = 0, j=0; i < keys.Count; i++, j++)
			{
				if (_argMap[keys[i]].ArgType != null)
				{
					_args.Add(keys[i], values[j]);
				}
				else
				{
					_args.Add(keys[i], null);
					j--;
				}
			}

			return _args;
		}

		/// <summary>
		/// Perprocess argument parts that are surrounded by quotes
		/// </summary>
		/// <param name="arg"></param>
		/// <returns></returns>
		private object Preprocess(string arg, string forArg)
		{
			object result = null;

			if (_argMap.ContainsKey(forArg))
			{
				switch (_argMap[forArg].GetType().FullName)
				{
					case "System.String":
						result = arg;
						break;
					case "System.String[]":
						string[] delimiters = ((string[])_argMap[forArg].ArgType);
						if (delimiters.Length == 1)
						{
							string[] values = arg.Split(new string[] { delimiters[0] }, StringSplitOptions.RemoveEmptyEntries);
							result = values;
						}
						else if (delimiters.Length == 2)
						{
							string[] pairs = arg.Split(new string[] { delimiters[0] }, StringSplitOptions.RemoveEmptyEntries);
							Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
							foreach (string pair in pairs)
							{
								string[] keyValuePair = pair.Split(new string[] { delimiters[1] }, StringSplitOptions.RemoveEmptyEntries);
								keyValuePairs.Add(keyValuePair[0], keyValuePair[1]);
							}
							result = keyValuePairs;
						}
						break;
					case "System.Int64":
						result = long.Parse(arg);
						break;
					default:
						result = arg;
						break;
				}
			}
			else
			{
				result = arg;
			}

			return result;
		}

		#endregion
	}
}

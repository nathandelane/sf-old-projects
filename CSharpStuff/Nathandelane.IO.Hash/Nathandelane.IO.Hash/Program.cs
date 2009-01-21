using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Nathandelane.IO.Hash
{
	class Program
	{
		private enum ArgumentType
		{
			Name,
			Value
		}

		public Program(Dictionary<string, string> arguments)
		{
			string resource = String.Empty;
			ResourceType resourceType = ResourceType.File;
			HashType hashType = HashType.MD5;

			foreach (string key in arguments.Keys)
			{
				switch (key)
				{
					case "-f":
						resourceType = ResourceType.File;
						resource = arguments[key];
						break;
					case "-s":
						resourceType = ResourceType.String;
						resource = arguments[key];
						break;
					case "-c":
						switch (arguments[key].ToLower())
						{
							case "md5":
								hashType = HashType.MD5;
								break;
							case "sha1":
								hashType = HashType.SHA1;
								break;
						}
						break;
				}
			}

			using (Hash hash = new Hash(resource, resourceType, hashType))
			{
				Console.WriteLine("{0}", hash);
			}
		}

		static void Main(string[] args)
		{
			if (args.Length == 4 && (String.Join(" ", args).Contains("-c") && (String.Join(" ", args).Contains("-s") || String.Join(" ", args).Contains("-f"))))
			{
				new Program(ParseArguments(args));
			}
			else
			{
				Console.WriteLine("Usage: hash (-s string|-f filename) -c (md5|sha1)");
			}
		}

		private static Dictionary<string, string> ParseArguments(string[] args)
		{
			Dictionary<string, string>  arguments = new Dictionary<string, string>();

			ArgumentType argType = ArgumentType.Name;
			string name = String.Empty;
			string value = String.Empty;
			foreach (string arg in args)
			{
				if (argType == ArgumentType.Name)
				{
					name = arg;
					argType = ArgumentType.Value;
				}
				else if (argType == ArgumentType.Value)
				{
					value = arg;
					arguments.Add(name, value);
					argType = ArgumentType.Name;
				}
			}

			return arguments;
		}
	}
}

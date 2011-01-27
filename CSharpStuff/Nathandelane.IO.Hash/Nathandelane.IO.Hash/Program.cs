using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Nathandelane.IO.Hash
{
	class HashProgram
	{
		/// <summary>
		/// Represents argument types.
		/// </summary>
		private enum ArgumentType
		{
			Name,
			Value
		}

		/// <summary>
		/// Constructor for the program.
		/// </summary>
		/// <param name="arguments"></param>
		public HashProgram(Dictionary<string, string> arguments)
		{
			string resource = String.Empty;
			ResourceType resourceType = ResourceType.File;
			HashType hashType = HashType.MD5;
			string desKey = String.Empty;

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
							case "sha256":
								hashType = HashType.SHA256;
								break;
							case "sha384":
								hashType = HashType.SHA384;
								break;
							case "sha512":
								hashType = HashType.SHA512;
								break;
							case "des":
								hashType = HashType.DES;
								break;
							case "binary7":
								hashType = HashType.BINARY7;
								break;
						}
						break;
					default:
						desKey = key;
						break;
				}
			}

			if (hashType == HashType.DES)
			{
				using (Hash desHash = new Hash(resource, resourceType, hashType, desKey))
				{
					Console.WriteLine("{0}", desHash);
				}
			}
			else
			{
				using (Hash hash = new Hash(resource, resourceType, hashType))
				{
					Console.WriteLine("{0}", hash);
				}
			}
		}

		/// <summary>
		/// Preogram entry point.
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			if (args.Length >= 4 && (String.Join(" ", args).Contains("-c") && (String.Join(" ", args).Contains("-s") || String.Join(" ", args).Contains("-f"))))
			{
				try
				{
					new HashProgram(ParseArguments(args));
				}
				catch (ArgumentException ex)
				{
					Console.WriteLine("Exception caught: {0}", ex.Message);
				}
			}
			else
			{
				Console.WriteLine("Usage: hash (-s string|-f filename) -c (md5|sha1|sha256|sha384|sha512|des|binary7)");
			}
		}

		/// <summary>
		/// Parses command-line arguments.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		private static Dictionary<string, string> ParseArguments(string[] args)
		{
			Dictionary<string, string>  arguments = new Dictionary<string, string>();
			string[] paddedArgs = args;

			if ((paddedArgs.Length % 2) != 0)
			{
				paddedArgs = new string[args.Length + 1];
				for (int index = 0; index < args.Length; index++)
				{
					paddedArgs[index] = args[index];
				}
				paddedArgs[paddedArgs.Length - 1] = String.Empty;
			}

			ArgumentType argType = ArgumentType.Name;
			string name = String.Empty;
			string value = String.Empty;
			foreach (string arg in paddedArgs)
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

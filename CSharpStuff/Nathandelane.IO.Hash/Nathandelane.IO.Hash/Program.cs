using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Nathandelane.IO.Hash
{
	class Program
	{
		public Program(string[] args)
		{
			string fileName = "";
			string hashMethod = "";
			string stringToBeHashed = "";
			string result = "";

			for (int i = 0; i < args.Length; i++)
			{
				if (args[i].Equals("-f") || args[i].Equals("--file"))
				{
					i++;
					fileName = args[i];
				}
				else if (args[i].Equals("-m") || args[i].Equals("--method"))
				{
					i++;
					hashMethod = args[i];
				}
				else if (args[i].Equals("-s") || args[i].Equals("--string"))
				{
					i++;
					stringToBeHashed = args[i];
				}
			}

			if (String.IsNullOrEmpty(fileName) && String.IsNullOrEmpty(stringToBeHashed))
			{
				throw new ArgumentException();
			}
			else
			{
				if (!String.IsNullOrEmpty(fileName))
				{
					if (File.Exists(fileName))
					{
						FileStream fs = new FileStream(fileName, FileMode.Open);

						switch (hashMethod)
						{
							case "md5":
								MD5 md5 = MD5.Create();

								foreach (byte b in md5.ComputeHash(fs))
								{
									result += ByteToHex(b);
								}

								Console.WriteLine("Result: {0}", result);
								break;
							case "sha1":
								SHA1 sha1 = SHA1.Create();

								foreach (byte b in sha1.ComputeHash(fs))
								{
									result += ByteToHex(b);
								}

								Console.WriteLine("Result: {0}", result);
								break;
							case "simple":
								int res = fs.GetHashCode();
								Console.WriteLine("Result: {0}", res);
								break;
						}
					}
					else
					{
						Console.WriteLine("Cannot find file or directory named '{0}'", fileName);
					}
				}
				else if (!String.IsNullOrEmpty(stringToBeHashed))
				{
					ASCIIEncoding enc = new ASCIIEncoding();

					switch (hashMethod)
					{
						case "md5":
							MD5 md5 = MD5.Create();

							foreach (byte b in md5.ComputeHash(enc.GetBytes(stringToBeHashed)))
							{
								result += ByteToHex(b);
							}

							Console.WriteLine("Result: {0}", result);
							break;
						case "sha1":
							SHA1 sha1 = SHA1.Create();

							foreach (byte b in sha1.ComputeHash(enc.GetBytes(stringToBeHashed)))
							{
								result += ByteToHex(b);
							}

							Console.WriteLine("Result: {0}", result);
							break;
						case "simple":
							int res = stringToBeHashed.GetHashCode();
							Console.WriteLine("Result: {0}", res);
							break;
					}
				}
			}
		}

		private string ByteToHex(byte next)
		{
			string retVal = "";

			retVal = Convert.ToString(next, 16);

			return retVal;
		}

		static void Main(string[] args)
		{
			string argument = String.Join(" ", args);

			if (argument.Contains("-h") || argument.Contains("--help"))
			{
				Help();
			}
			else if (args.Length < 2)
			{
				Usage();
			}
			else if (args.Length >= 2)
			{
				if ((argument.Contains("-m") && (argument.Contains("-s") || argument.Contains("-f"))) || (argument.Contains("--method") && (argument.Contains("--string") || argument.Contains("--file"))))
				{
					try
					{
						new Program(args);
					}
					catch (Exception)
					{
						Console.WriteLine("Missing arguments");
					}
				}
				else
				{
					Console.WriteLine("Missing arguments");
				}
			}
		}

		public static void Help()
		{
			Usage();

			Console.WriteLine("-s, --string     hashstring      Hashes the string you supply.");
			Console.WriteLine("-f, --file       hashfile        Hashes the file you supply.");
			Console.WriteLine("-m, --method     hashmethod      Hashes using the supplied method.");
			Console.WriteLine("                   Valid methods include simple, md5, sha1");
		}

		public static void Usage()
		{
			Console.WriteLine("\nUsage: Hash (-f fileName|-s string) -m hashingMethod");
		}
	}
}

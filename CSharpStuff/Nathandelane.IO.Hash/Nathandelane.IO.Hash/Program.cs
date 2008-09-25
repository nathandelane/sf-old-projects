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
						HashAlgorithm algorithm = null;

						switch (hashMethod)
						{
							case "hmac":
								algorithm = HMAC.Create();
								break;
							case "hmacmd5":
								algorithm = HMACMD5.Create();
								break;
							case "hmacripemd160":
								algorithm = HMACRIPEMD160.Create();
								break;
							case "hmacsha1":
								algorithm = HMACSHA1.Create();
								break;
							case "hmacsha256":
								algorithm = HMACSHA256.Create();
								break;
							case "hmacsha384":
								algorithm = HMACSHA384.Create();
								break;
							case "hmacsha512":
								algorithm = HMACSHA512.Create();
								break;
							case "md5":
								algorithm = MD5.Create();
								break;
							case "sha1":
								algorithm = SHA1Managed.Create();
								break;
							case "sha256":
								algorithm = SHA256Managed.Create();
								break;
							case "sha384":
								algorithm = SHA384Managed.Create();
								break;
							case "sha512":
								algorithm = SHA512Managed.Create();
								break;
							case "simple":
								result = String.Format("{0}", fs.GetHashCode());
								break;
						}

						if (algorithm != null)
						{
							foreach (byte b in algorithm.ComputeHash(fs))
							{
								result += ByteToHex(b);
							}
						}

						Console.WriteLine("Result: {0}", result);
					}
					else
					{
						Console.WriteLine("Cannot find file or directory named '{0}'", fileName);
					}
				}
				else if (!String.IsNullOrEmpty(stringToBeHashed))
				{
					ASCIIEncoding enc = new ASCIIEncoding();
					HashAlgorithm algorithm = null;

					switch (hashMethod)
					{
						case "hmac":
							algorithm = HMAC.Create();
							break;
						case "hmacmd5":
							algorithm = HMACMD5.Create();
							break;
						case "hmacripemd160":
							algorithm = HMACRIPEMD160.Create();
							break;
						case "hmacsha1":
							algorithm = HMACSHA1.Create();
							break;
						case "hmacsha256":
							algorithm = HMACSHA256.Create();
							break;
						case "hmacsha384":
							algorithm = HMACSHA384.Create();
							break;
						case "hmacsha512":
							algorithm = HMACSHA512.Create();
							break;
						case "md5":
							algorithm = MD5.Create();
							break;
						case "sha1":
							algorithm = SHA1Managed.Create();
							break;
						case "sha256":
							algorithm = SHA256Managed.Create();
							break;
						case "sha384":
							algorithm = SHA384Managed.Create();
							break;
						case "sha512":
							algorithm = SHA512Managed.Create();
							break;
						case "simple":
							result = String.Format("{0}", stringToBeHashed.GetHashCode());
							break;
					}

					if (algorithm != null)
					{
						foreach (byte b in algorithm.ComputeHash(ASCIIEncoding.ASCII.GetBytes(stringToBeHashed)))
						{
							result += ByteToHex(b);
						}
					}

					Console.WriteLine("Result: {0}", result);
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
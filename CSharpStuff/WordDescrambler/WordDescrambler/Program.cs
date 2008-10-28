using System;
using System.Collections.Generic;
using System.IO;

namespace WordDescrambler
{
	class Program
	{
		static void Main(string[] args)
		{
			using (StreamReader reader = new StreamReader("wordlist.txt"))
			{
				string[] words = reader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

				Console.WriteLine("Total words is {0}", words.Length);

				foreach (string arg in args)
				{
					foreach (string word in words)
					{
						if (word.Length == arg.Length)
						{
							Dictionary<int, string> matchedChars = new Dictionary<int, string>();
							for (int i = 0; i < arg.Length; i++)
							{
								int index = word.IndexOf(arg.Substring(i, 1));

								if (index > -1)
								{
									if (matchedChars.ContainsKey(index))
									{
										int j = index;
										while ((index = word.IndexOf(arg.Substring(i, 1), j)) > -1)
										{
											if (matchedChars.ContainsKey(index))
											{
												j = index + 1;
											}
											else
											{
												break;
											}
										}

										if (index > -1)
										{
											matchedChars.Add(index, word.Substring(index, 1));
										}
									}
									else
									{
										matchedChars.Add(index, word.Substring(index, 1));
									}
								}
							}

							if (matchedChars.Count == arg.Length)
							{
								Console.Write("{0},", word);
								break;
							}
							else
							{
								//Console.WriteLine("FAILED: {0}", word);
							}
						}
					}
				}
			}

			Console.WriteLine("");
			Console.ReadKey();
		}
	}
}

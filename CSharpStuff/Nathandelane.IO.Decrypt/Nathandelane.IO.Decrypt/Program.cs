using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Nathandelane.IO.Decrypt
{
	class Program
	{
		static void Main(string[] args)
		{
			int charSpace = 762;
			string cypherText = "C:\\convertedmsg.txt";
			string plaintext = "C:\\plainmsg.txt";

			using (StreamReader reader = new StreamReader(cypherText))
			{
				using (StreamWriter writer = new StreamWriter(plaintext))
				{
					string str = "";
					while ((str = reader.ReadLine()) != null)
					{
						int i = int.Parse(str) - charSpace;
						string c = ((char)i).ToString();

						writer.Write("{0}", c);
					}
				}
			}
		}
	}
}

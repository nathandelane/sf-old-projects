using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Nathandelane.IO.ConvertToBytes
{
	class Program
	{
		static void Main(string[] args)
		{
			using (StreamReader reader = new StreamReader("C:\\encryptedmsg.txt"))
			{
				using (StreamWriter writer = new StreamWriter("C:\\convertedmsg.txt"))
				{
					string str = "";
					while ((str = reader.ReadLine()) != null)
					{
						string[] ints = str.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);

						if (ints.Length == 3)
						{
							int total = int.Parse(ints[0]) + int.Parse(ints[1]) + int.Parse(ints[2]);
							writer.WriteLine("{0}", total);
						}
					}
				}
			}
		}
	}
}

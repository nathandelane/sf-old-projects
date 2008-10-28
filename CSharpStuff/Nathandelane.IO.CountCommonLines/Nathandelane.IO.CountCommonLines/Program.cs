using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Nathandelane.IO.CountCommonLines
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> lines = new List<string>();

			using (StreamReader reader = new StreamReader("C:\\convertedmsg.txt"))
			{
				string str = "";
				while ((str = reader.ReadLine()) != null)
				{
					lines.Add(str);
				}
			}

			using (StreamWriter writer = new StreamWriter("C:\\msgheuristics.txt"))
			{
				List<string> uniques = new List<string>();

				foreach (string s in lines)
				{
					if (!uniques.Contains(s))
					{
						uniques.Add(s);
					}
				}

				uniques.Sort();

				foreach (string s in uniques)
				{
					int i = 0;

					foreach (string st in lines)
					{
						if (st.Equals(s))
						{
							i++;
						}
					}

					writer.WriteLine("{0}\t{1}", s, i);
				}
			}
		}
	}
}

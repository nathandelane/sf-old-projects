using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nathandelane.Net.WebGet
{
	class Program
	{
		private Program(IList<string> urls)
		{
			foreach(string nextUrl in urls)
			{
				using(WebGet webGet = new WebGet(nextUrl))
				{
					webGet.Run();
				}
			}
		}
		
		public static void Main(string[] args)
		{
			if(args.Length < 1)
			{
				DisplayHelp();
			}
			else
			{
				IList<string> urls = new List<string>(args);
				
				new Program(urls);
			}
		}
		
		private static void DisplayHelp()
		{
			Console.WriteLine("Usage: {0} url", Assembly.GetEntryAssembly().GetName().Name);
		}
	}
}
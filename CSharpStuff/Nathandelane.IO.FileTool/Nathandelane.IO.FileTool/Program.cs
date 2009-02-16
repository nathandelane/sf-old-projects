using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.FileTool
{
	class Program
	{
		static void Main(string[] args)
		{
			FileInfo fi = new FileInfo(@"C:\WINDOWS\system32\notepad.exe");
			Console.WriteLine("{0}", fi.ToString());
		}
	}
}

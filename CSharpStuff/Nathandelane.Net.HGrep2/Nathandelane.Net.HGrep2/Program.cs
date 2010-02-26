using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.Net.HGrep2
{
	class Program
	{
		#region Constructors

		private Program(string[] args)
		{
			CollectSettings(args);
		}

		#endregion

		#region Methods

		private void Run()
		{
		}

		/// <summary>
		/// Gets command-line arguments and places them in the context.
		/// </summary>
		/// <param name="args"></param>
		private void CollectSettings(string[] args)
		{
			Context context = Context.GetInstance();

			foreach(string arg in args)
			{
				if (arg.Contains("="))
				{
					string[] keyValuePair = arg.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);

					if (keyValuePair.Length == 2)
					{
						context[keyValuePair[0]] = keyValuePair[1];
					}
				}
				else
				{
					context[arg] = String.Empty;
				}
			}
		}

		/// <summary>
		/// Program entry point.
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			Program program = new Program(args);
		}

		#endregion
	}
}

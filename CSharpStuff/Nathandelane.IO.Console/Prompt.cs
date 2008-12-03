using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.Console
{
	public class Prompt
	{
		private static string _value;

		public string Value
		{
			get { return _value; }
		}

		public Prompt(string value)
		{
			_value = value;
		}

		public static string Parse(Settings settings)
		{
			string encodedPrompt = settings["prompt"];

			if (encodedPrompt.Contains('%'))
			{
				List<string> parts = new List<string>();
				int stringIndex = 0;

				while (stringIndex < encodedPrompt.Length)
				{
					int startIndex = encodedPrompt.IndexOf('%', stringIndex) + 1;
					int endIndex = encodedPrompt.IndexOf('%', startIndex);
					string decodeValue = String.Empty;

					if (endIndex != 0)
					{
						decodeValue = encodedPrompt.Substring(startIndex, (endIndex - startIndex));

						try
						{
							parts.Add(settings[decodeValue]);
						}
						catch (Exception)
						{
							throw;
						}

						stringIndex = endIndex + 1;
					}
					else
					{
						parts.Add(encodedPrompt.Substring(stringIndex));

						stringIndex = encodedPrompt.Length;
					}
				}

				_value = String.Join("", parts.ToArray());
			}
			else
			{
				_value = encodedPrompt;
			}

			return _value;
		}
	}
}

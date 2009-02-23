using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.Encrypt
{
	internal class Xor
	{
		#region Fields

		private string _key;
		private string _inputFilePath;

		#endregion

		#region Constructors

		public Xor(string key, string inputFilePath)
		{
			_key = key;
			_inputFilePath = inputFilePath;
		}

		#endregion

		#region Public Methods

		public void Run()
		{
			string data = String.Empty;

			using (StreamReader reader = new StreamReader(_inputFilePath))
			{
				data = reader.ReadToEnd();
			}

			byte[] bytes = ASCIIEncoding.UTF8.GetBytes(data);
			byte[] newData = new byte[bytes.Length];

			using (StreamWriter writer = new StreamWriter("Xor.temp"))
			{
				int keyIndex = 0;
				byte[] keyArray = ASCIIEncoding.UTF8.GetBytes(_key);
				for (long index = 0; index < bytes.Length; index++)
				{
					newData[index] = (byte)((int)bytes[index] ^ (int)keyArray[keyIndex]);
					keyIndex++;

					if (keyIndex > _key.Length)
					{
						keyIndex = 0;
					}
				}
			}

			File.Copy("Xor.temp", _inputFilePath, true);
		}

		#endregion
	}
}

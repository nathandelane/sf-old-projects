using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Nathandelane.IO.Hash
{
	internal class Hash : IDisposable
	{
		private string _resourceValue;
		private string _resultingHash;
		private ResourceType _resourceType;
		private HashType _hashType;
		private bool _isDisposed;

		public string ResourceValue
		{
			get { return _resourceValue; }
		}

		public ResourceType ResourceType
		{
			get { return _resourceType; }
		}

		public HashType HashType
		{
			get { return _hashType; }
		}

		public Hash(string resourceValue, ResourceType resourceType, HashType hashType)
		{
			_resourceValue = resourceValue;
			_resourceType = resourceType;
			_hashType = hashType;
			_resultingHash = String.Empty;
			_isDisposed = false;
		}

		public override string ToString()
		{
			if (String.IsNullOrEmpty(_resultingHash))
			{
				_resultingHash = CreateHash();
			}

			return _resultingHash;
		}

		private string CreateHash()
		{
			StringBuilder sb = new StringBuilder();

			byte[] input = ASCIIEncoding.ASCII.GetBytes(GetResource());
			byte[] output = GetHash(input);

			for (int outputIndex = 0; outputIndex < output.Length; outputIndex++)
			{
				sb.Append(output[outputIndex].ToString("X2"));
			}

			return sb.ToString();
		}

		private string GetResource()
		{
			string resource = String.Empty;

			if (_resourceType == ResourceType.File)
			{
				using (StreamReader reader = new StreamReader(new FileStream(_resourceValue, FileMode.Open, FileAccess.Read)))
				{
					resource = reader.ReadToEnd();
				}
			}
			else if(_resourceType == ResourceType.String)
			{
				resource = _resourceValue;
			}

			return resource;
		}

		private byte[] GetHash(byte[] input)
		{
			byte[] output = new byte[0];

			switch (_hashType)
			{
				case HashType.MD5:
					output = MD5.Create().ComputeHash(input);
					break;
				case HashType.SHA1:
					output = SHA1.Create().ComputeHash(input);
					break;
				default:
					output = MD5.Create().ComputeHash(input);
					break;
			}

			return output;
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (!_isDisposed)
			{
				_isDisposed = true;
				_resourceValue = String.Empty;
				_resultingHash = String.Empty;
			}
		}

		#endregion
	}
}

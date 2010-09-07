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
		private string _desKey;
		private ResourceType _resourceType;
		private HashType _hashType;
		private bool _isDisposed;

		/// <summary>
		/// Gets the hash resource value.
		/// </summary>
		public string ResourceValue
		{
			get { return _resourceValue; }
		}

		/// <summary>
		/// Gets the resource type for the hash encryption.
		/// </summary>
		public ResourceType ResourceType
		{
			get { return _resourceType; }
		}

		/// <summary>
		/// Gets the hash type.
		/// </summary>
		public HashType HashType
		{
			get { return _hashType; }
		}

		/// <summary>
		/// Creates an instance of Hash.
		/// </summary>
		/// <param name="resourceValue"></param>
		/// <param name="resourceType"></param>
		/// <param name="hashType"></param>
		public Hash(string resourceValue, ResourceType resourceType, HashType hashType)
		{
			_resourceValue = resourceValue;
			_resourceType = resourceType;
			_hashType = hashType;
			_resultingHash = String.Empty;
			_isDisposed = false;
		}

		/// <summary>
		/// Creates an instance of Hash.
		/// </summary>
		/// <param name="resourceValue"></param>
		/// <param name="resourceType"></param>
		/// <param name="hashType"></param>
		/// <param name="desKey"></param>
		public Hash(string resourceValue, ResourceType resourceType, HashType hashType, string desKey)
		{
			if (desKey.Length < 8)
			{
				throw new ArgumentException("DES key must be at least eight (8) characters long.");
			}

			_resourceValue = resourceValue;
			_resourceType = resourceType;
			_hashType = hashType;
			_desKey = desKey;
			_resultingHash = String.Empty;
			_isDisposed = false;
		}

		/// <summary>
		/// Returns a string representation of Hash.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if (String.IsNullOrEmpty(_resultingHash))
			{
				_resultingHash = CreateHash();
			}

			return _resultingHash;
		}

		/// <summary>
		/// Creates the hash.
		/// </summary>
		/// <returns>String representation of the resultant hash.</returns>
		private string CreateHash()
		{
			StringBuilder sb = new StringBuilder();

			byte[] input = ASCIIEncoding.ASCII.GetBytes(GetResource());
			byte[] output = GetHash(input);

			if (_hashType == HashType.DES)
			{
				for (int outputIndex = 0; outputIndex < output.Length; outputIndex++)
				{
					sb.Append(Convert.ToChar(output[outputIndex]).ToString());
				}
			}
			else
			{
				for (int outputIndex = 0; outputIndex < output.Length; outputIndex++)
				{
					sb.Append(output[outputIndex].ToString("X2"));
				}
			}

			return sb.ToString();
		}

		/// <summary>
		/// Gets the resource.
		/// </summary>
		/// <returns>String representation of the resource.</returns>
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

		/// <summary>
		/// Gets a byte array representing the resultant hash.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
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
				case HashType.SHA256:
					output = SHA256.Create().ComputeHash(input);
					break;
				case HashType.SHA384:
					output = SHA384.Create().ComputeHash(input);
					break;
				case HashType.SHA512:
					output = SHA512.Create().ComputeHash(input);
					break;
				case HashType.DES:
					output = ComputeDES(input);
					break;
				default:
					output = MD5.Create().ComputeHash(input);
					break;
			}

			return output;
		}

		/// <summary>
		/// Computes a special DES hash variant.
		/// </summary>
		/// <param name="plaint"></param>
		/// <returns></returns>
		private byte[] ComputeDES(byte[] plaint)
		{
			byte[] result;

			DES des = new DESCryptoServiceProvider();
			des.Mode = CipherMode.CBC;
			byte[] plaintext = plaint;
			byte[] key = Encoding.ASCII.GetBytes(_desKey);
			byte[] iv = Encoding.ASCII.GetBytes("init vec");
			des.Key = key;
			des.IV = iv;
			ICryptoTransform transform = des.CreateEncryptor(des.Key, des.IV);
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write(plaintext, 0, plaintext.Length);
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			result = memoryStream.ToArray();

			return result;
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

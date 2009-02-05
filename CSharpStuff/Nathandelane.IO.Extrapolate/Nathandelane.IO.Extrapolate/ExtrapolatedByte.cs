using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.Extrapolate
{
	internal class ExtrapolatedByte
	{
		private byte _value;
		private byte[] _extrapolation;

		public byte[] Extrapolation
		{
			get { return _extrapolation; }
		}

		public ExtrapolatedByte(byte value)
		{
			_value = value;
		}

		private void ExtrapolateByte()
		{

		}
	}
}

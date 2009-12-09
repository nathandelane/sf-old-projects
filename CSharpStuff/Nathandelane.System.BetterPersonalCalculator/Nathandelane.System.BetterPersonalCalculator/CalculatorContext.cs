using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class CalculatorContext
	{
		#region Fields

		public const string LastResult = "$";

		private static CalculatorContext __instance = new CalculatorContext();
		private static Object __lockObject = new Object();

		private Dictionary<string, Token> _values;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets a context item.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public Token this[string key]
		{
			get
			{
				Token token = new NullToken();

				if (_values.ContainsKey(key))
				{
					token = (Token)_values[key];
				}

				return token;
			}
			set
			{
				if (!_values.ContainsKey(key))
				{
					_values.Add(key, value);
				}
				else
				{
					if (value == null)
					{
						_values.Remove(key);
					}
					else
					{
						_values[key] = value;
					}
				}
			}
		}

		#endregion

		#region Constructors

		private CalculatorContext()
		{
			_values = new Dictionary<string, Token>();

			this["$"] = new NumberToken();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets a reference to the singleton instance of CalculatorContext.
		/// </summary>
		/// <returns></returns>
		public static CalculatorContext GetInstance()
		{
			if (CalculatorContext.__instance == null)
			{
				lock (CalculatorContext.__lockObject)
				{
					CalculatorContext.__instance = new CalculatorContext();
				}
			}

			return CalculatorContext.__instance;
		}

		/// <summary>
		/// Gets the last result.
		/// </summary>
		/// <returns></returns>
		public Token GetLastResult()
		{
			return (Token)this[LastResult];
		}

		/// <summary>
		/// Determines whether a certain key is contained in the context.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool ContainsKey(string key)
		{
			return _values.ContainsKey(key);
		}

		#endregion
	}
}

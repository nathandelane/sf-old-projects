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
		public const string FunctionOperandMap = "fom";

		private static CalculatorContext __instance = new CalculatorContext();
		private static Object __lockObject = new Object();

		private Dictionary<string, object> _values;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets a context item.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public object this[string key]
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
			_values = new Dictionary<string, object>();

			this["$"] = new NumberToken();
			this["fom"] = new Dictionary<string, int>
			{
				{ "cos", 1},
				{ "sin", 1},
				{ "tan", 1},
				{ "acos", 1},
				{ "asin", 1},
				{ "atan", 1},
				{ "sqrt", 1},
				{ "**", 1},
				{ "//", 1},
				{ "%", 1}
			};
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


		public Token GetLastResult()
		{
			return (Token)this[LastResult];
		}

		public Dictionary<string, int> GetFunctionOperandMap()
		{
			return (Dictionary<string, int>)this[FunctionOperandMap];
		}

		#endregion
	}
}

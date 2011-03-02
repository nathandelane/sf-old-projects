using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HttpNetTest.Events;

namespace HttpNetTest.Rules
{
	public abstract class ValidationRule
	{
		#region Fields

		private string _ruleDescription;
		private string _ruleName;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the description of this ValidationRule.
		/// </summary>
		public string RuleDescription
		{
			get { return _ruleDescription; }
			set { _ruleName = value; }
		}

		/// <summary>
		/// Gets or sets the name of this ValidationRule.
		/// </summary>
		public string RuleName
		{
			get { return _ruleName; }
			set { _ruleName = value; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates an instance of ValidationRule.
		/// </summary>
		public ValidationRule()
		{			
		}

		/// <summary>
		/// Creates an instance of ValidationRule.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="description"></param>
		public ValidationRule(string name, string description)
		{
			_ruleName = name;
			_ruleDescription = description;
		}

		/// <summary>
		/// Returns a string representation of this ValidationRule based on the name and description.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return String.Format("{0}: {1}", _ruleName, _ruleDescription);
		}

		/// <summary>
		/// When overridden in a derived class, this validates both the request and response.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public virtual void Validate(Object sender, ValidationEventArgs e)
		{

		}

		#endregion
	}
}

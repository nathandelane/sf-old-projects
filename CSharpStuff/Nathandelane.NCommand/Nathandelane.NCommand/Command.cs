using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.NCommand
{
	public abstract class Command
	{
		private string _commandName;
		private string _usage;
		private Dictionary<string, bool> _parameters;

		public Command(string commandName, string[] parameters)
		{
			_commandName = commandName;
			_usage = "";
			_parameters = new Dictionary<string, bool>();
		}

		public Command(string commandName, string[] parameters, string usage)
		{
			_commandName = commandName;
			_usage = usage;
			_parameters = new Dictionary<string, bool>();
		}

		public string CommandName
		{
			get { return _commandName; }
		}

		public string Usage
		{
			get { return _usage; }
			set
			{
				if (value != null)
				{
					_usage = value;
				}
				else
				{
					_usage = "";
				}
			}
		}

		public bool HasParameter(string parameter)
		{
			bool retVal = false;

			if (_parameters.ContainsKey(parameter))
			{
				retVal = true;
			}

			return retVal;
		}

		public bool IsRequiredParameter(string parameter)
		{
			bool retVal = false;

			if (HasParameter(parameter))
			{
				retVal = _parameters[parameter];
			}

			return retVal;
		}

		public void Execute(Dictionary<string, string> parameters)
		{
			if (HasRequiredParameters(parameters))
			{
			}
			else
			{
				throw new ArgumentException("");
			}
		}

		private bool HasRequiredParameters(Dictionary<string, string> parameters)
		{
			bool retVal = true;

			foreach (string param in _parameters.Keys)
			{
				if (IsRequiredParameter(param) && !parameters.ContainsKey(param))
				{
					retVal = false;
				}
			}

			return retVal;
		}
	}
}

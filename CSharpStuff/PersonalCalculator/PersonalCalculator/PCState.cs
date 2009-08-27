using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace Nathandelane.Math.PersonalCalculator
{
    internal sealed class PCState
    {
        #region Fields

        private static PCState instance = null;
        private static Dictionary<string, object> _states = null;

        #endregion

        #region Properties

        public object this[string key]
        {
            get 
            {
                object retVal = null;

				if (_states.ContainsKey(key))
				{
					retVal = _states[key];
				}

                return retVal;
            }

            set
            {
                if (!_states.ContainsKey(key))
                {
                    _states.Add(key, value);
                }
                else
                {
                    _states[key] = value;
                }
            }
        }

        public static PCState Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PCState();
                }

                return instance;
            }
        }

        #endregion

        #region Constructors

        private PCState()
        {
            _states = new Dictionary<string, object>();

            if (File.Exists("pc.exe.config"))
            {
                string[] keys = ConfigurationManager.AppSettings.AllKeys;

                foreach (string key in keys)
                {
                    _states.Add(key, ConfigurationManager.AppSettings[key]);
                }
            }
        }

        #endregion
    }
}

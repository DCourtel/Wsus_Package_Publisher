using System;
using System.Collections.Generic;
using System.Text;

namespace CustomUpdateEngine.Exceptions
{
    internal class ExpandEnvironmentVariableException:Exception
    {
        private string _message = string.Empty;

        internal ExpandEnvironmentVariableException(string message)
        {
            _message = message;
        }

        public override string Message
        {
            get
            {
                return _message;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CustomUpdateEngine.Exceptions
{
    class FileNotFoundException:Exception
    {
        private string _message = string.Empty;

        internal FileNotFoundException(string message)
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

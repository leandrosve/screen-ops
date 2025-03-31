using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public abstract class ControllerException : Exception
    {
        private string _message;
        public abstract int GetHttpCode();
        public string GetMessage()
        {
            return _message;
        }

        public ControllerException(string message)
        {
            _message = message;
        }
    }
}

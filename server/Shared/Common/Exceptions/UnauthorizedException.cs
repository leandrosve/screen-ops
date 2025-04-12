using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class UnauthorizedException : ControllerException
    {
        public UnauthorizedException(string message) : base(message)
        {
        }

        public UnauthorizedException() : base("unauthorized")
        {
        }

        public override int GetHttpCode()
        {
            return 401;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Exceptions
{
    public class NotEnoughUnitException : Exception
    {
        public NotEnoughUnitException()
        {
        }

        public NotEnoughUnitException(string? message) : base(message)
        {
        }

        public NotEnoughUnitException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

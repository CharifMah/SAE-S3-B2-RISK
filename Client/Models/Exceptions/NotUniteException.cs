using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Exceptions
{
    public class NotUniteException : Exception
    {
        public NotUniteException()
        {
        }

        public NotUniteException(string? message) : base(message)
        {
        }

        public NotUniteException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

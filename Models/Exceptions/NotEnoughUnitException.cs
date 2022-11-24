using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Exceptions
{
    public class NotEnoughUniteBasexception : Exception
    {
        public NotEnoughUniteBasexception()
        {
        }

        public NotEnoughUniteBasexception(string? message) : base(message)
        {
        }

        public NotEnoughUniteBasexception(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

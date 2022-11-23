using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Exceptions
{
    public class NotYourTerritoryException: Exception
    {
        public NotYourTerritoryException() { }

        public NotYourTerritoryException(string message) : base(message) { }

        public NotYourTerritoryException(string message, Exception inner): base(message,inner) { }
    }
}

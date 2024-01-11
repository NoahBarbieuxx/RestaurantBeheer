using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.DL.Exceptions
{
    public class TafelRepositoryException : Exception
    {
        public TafelRepositoryException(string? message) : base(message)
        {
        }

        public TafelRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
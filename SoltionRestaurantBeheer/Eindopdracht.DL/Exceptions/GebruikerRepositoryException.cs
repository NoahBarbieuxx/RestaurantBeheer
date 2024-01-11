using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.DL.Exceptions
{
    public class GebruikerRepositoryException : Exception
    {
        public GebruikerRepositoryException(string? message) : base(message)
        {
        }

        public GebruikerRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.DL.Exceptions
{
    public class MapperException : Exception
    {
        public MapperException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
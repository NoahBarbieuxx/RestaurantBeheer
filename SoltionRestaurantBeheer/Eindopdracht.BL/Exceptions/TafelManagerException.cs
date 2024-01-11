using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.BL.Exceptions
{
    public class TafelManagerException : Exception
    {
        public TafelManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
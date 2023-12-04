using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.BL.Exceptions
{
    public class RestaurantException : Exception
    {
        public RestaurantException(string? message) : base(message)
        {
        }

        public RestaurantException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
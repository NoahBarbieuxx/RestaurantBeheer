﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.DL.Exceptions
{
    public class RestaurantRepositoryException : Exception
    {
        public RestaurantRepositoryException(string? message) : base(message)
        {
        }

        public RestaurantRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.BL.Exceptions
{
    public class ContactgegevensException : Exception
    {
        public ContactgegevensException(string? message) : base(message)
        {
        }
    }
}
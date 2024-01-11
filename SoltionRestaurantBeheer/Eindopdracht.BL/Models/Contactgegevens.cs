using Eindopdracht.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.BL.Models
{
    public class Contactgegevens
    {
        public Contactgegevens(string telefoonnummer, string email)
        {
            Telefoonnummer = telefoonnummer;
            Email = email;
        }

        private string _telefoonnummer;
        public string Telefoonnummer
        {
            get
            {
                return _telefoonnummer;
            }
            set
            {
                if (!value.All(char.IsDigit) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ContactgegevensException("Telefoonnummer is ongeldig! (getallen)");
                }
                else
                {
                    _telefoonnummer = value;
                }
            }
        }

        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (!value.Contains('@') || string.IsNullOrWhiteSpace(value))
                {
                    throw new ContactgegevensException("Email is ongeldig! (@)");
                }
                else
                {
                    _email = value;
                }
            }
        }
    }
}
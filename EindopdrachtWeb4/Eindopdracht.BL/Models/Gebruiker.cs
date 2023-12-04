using Eindopdracht.BL.Exceptions;
using Eindopdracht.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.BL
{
    public class Gebruiker
    {
        public Gebruiker(int klantnummer, string naam, string email, string telefoonnummer, Locatie locatie)
        {
            _klantnummer = klantnummer;
            _naam = naam;
            _email = email;
            _telefoonnummer = telefoonnummer;
            _locatie = locatie;
        }

        private int _klantnummer;
        public int Klantnummer
        {
            get
            {
                return _klantnummer;
            }
            set
            {
                if (value <= 0)
                {
                    throw new GebruikerException("Klantnummer is ongeldig!");
                }
                else
                {
                    _klantnummer = value;
                }
            }
        }

        private string _naam;
        public string Naam
        {
            get
            {
                return _naam;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new GebruikerException("Naam is ongeldig!");
                }
                else
                {
                    _naam = value;
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
                if ((string.IsNullOrWhiteSpace(value)) || (!value.Contains('@')))
                {
                    throw new GebruikerException("Email is ongeldig!");
                }
                else
                {
                    _email = value;
                }
            }
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
                if (!value.All(char.IsDigit))
                {
                    throw new GebruikerException("Telefoonnummer is ongeldig!");
                }
                else
                {
                    _telefoonnummer = value;
                }
            }
        }

        private Locatie _locatie;
        public Locatie Locatie
        {
            get
            {
                return _locatie;
            }
            set
            {
                if (value == null)
                {
                    throw new GebruikerException("Locatie is ongeldig!");
                }
                else
                {
                    _locatie = value;
                }
            }
        }
    }
}
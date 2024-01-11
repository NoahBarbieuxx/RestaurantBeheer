using Eindopdracht.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.BL.Models
{
    public class Gebruiker
    {
        public Gebruiker(int klantnummer, string naam, string email, string telefoonnummer, int actief, Locatie locatie)
        {
            Klantnummer = klantnummer;
            Naam = naam;
            Email = email;
            Telefoonnummer = telefoonnummer;
            Actief = actief;
            Locatie = locatie;
        }

        public Gebruiker(string naam, string email, string telefoonnummer, Locatie locatie, int actief)
        {
            Naam = naam;
            Email = email;
            Telefoonnummer = telefoonnummer;
            Locatie = locatie;
            Actief = actief;
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
                    throw new GebruikerException("Klantnummer is ongeldig! (Groter dan 0)");
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
                    throw new GebruikerException("Naam is ongeldig! (Invullen)");
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
                if (!value.Contains('@') || string.IsNullOrWhiteSpace(value))
                {
                    throw new GebruikerException("Email is ongeldig! (@)");
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
                if (!value.All(char.IsDigit) || string.IsNullOrWhiteSpace(value) )
                {
                    throw new GebruikerException("Telefoonnummer is ongeldig! (Getallen)");
                }
                else
                {
                    _telefoonnummer = value;
                }
            }
        }

        private int _actief;
        public int Actief
        {
            get
            {
                return _actief;
            }
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new GebruikerException("ActiefNr is ongeldig! (0 of 1)");
                }
                else
                {
                    _actief = value;
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
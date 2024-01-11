using Eindopdracht.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.BL.Models
{
    public class Locatie
    {
        public Locatie()
        {

        }

        public Locatie(string postcode, string gemeentenaam, string straatnaam, string huisnummer)
        {
            Postcode = postcode;
            Gemeentenaam = gemeentenaam;
            Straatnaam = straatnaam;
            Huisnummer = huisnummer;
        }

        public Locatie(string locatieLijn)
        {
            string[] delen = locatieLijn.Split(new char[] { '|' });
            Postcode = delen[0];
            Gemeentenaam = delen[1];
            Straatnaam = delen[2];
            Huisnummer = delen[3];
        }

        private string _postcode;
        public string Postcode
        {
            get
            {
                return _postcode;
            }
            set
            {
                if (!value.All(char.IsDigit) && value.Length != 4)
                {
                    throw new LocatieException("Postcode is ongeldig!");
                }
                else
                {
                    _postcode = value;
                }
            }
        }

        private string _gemeentenaam;
        public string Gemeentenaam
        {
            get
            {
                return _gemeentenaam;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new LocatieException("Gemeentenaam is ongeldig!");
                }
                else
                {
                    _gemeentenaam = value;
                }
            }
        }

        private string _straatnaam;
        public string Straatnaam
        {
            get
            {
                return _straatnaam;
            }
            set
            {
                _straatnaam = value;
            }
        }

        private string _huisnummer;
        public string Huisnummer
        {
            get
            {
                return _huisnummer;
            }
            set
            {
                _huisnummer = value;
            }
        }

        public override string ToString()
        {
            return $"[{Postcode}] {Gemeentenaam} - {Straatnaam} - {Huisnummer}";
        }

        public string ToLocatieLijn()
        {
            return $"{Postcode}|{Gemeentenaam}|{Straatnaam}|{Huisnummer}";
        }
    }
}
using Eindopdracht.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.BL.Models
{
    public class Restaurant
    {
        public Restaurant(string naam, Locatie locatie, string keuken, Contactgegevens contactgegevens)
        {
            _naam = naam;
            _locatie = locatie;
            _keuken = keuken;
            _contactgegevens = contactgegevens;
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
                    throw new RestaurantException("Naam is ongeldig!");
                }
                else
                {
                    _naam = value;
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
                    throw new RestaurantException("Locatie is ongeldig!");
                }
                else
                {
                    _locatie = value;
                }
            }
        }

        private string _keuken;
        public string Keuken
        {
            get
            {
                return _keuken;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new RestaurantException("Keuken is ongeldig");
                }
                else
                {
                    _keuken = value;
                }
            }
        }

        private Contactgegevens _contactgegevens;
        public Contactgegevens Contactgegevens
        {
            get
            {
                return _contactgegevens;
            }
            set
            {
                if (value == null)
                {
                    throw new RestaurantException("Contactgegevens is ongeldig!");
                }
                else
                {
                    _contactgegevens = value;
                }
            }
        }
    }
}
using Eindopdracht.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.BL.Models
{
    public class Reservatie
    {
        public Reservatie(int reservatienummer, Restaurant restaurantinfo, Gebruiker contactpersoon, int aantalPlaatsen, DateTime datum, Tafel tafel)
        {
            Reservatienummer = reservatienummer;
            Restaurantinfo = restaurantinfo;
            Contactpersoon = contactpersoon;
            AantalPlaatsen = aantalPlaatsen;
            Datum = datum;
            Tafel = tafel;
        }

        public Reservatie(Restaurant restaurantinfo, Gebruiker contactpersoon, int aantalPlaatsen, DateTime datum, Tafel tafel)
        {
            Restaurantinfo = restaurantinfo;
            Contactpersoon = contactpersoon;
            AantalPlaatsen = aantalPlaatsen;
            Datum = datum;
            Tafel = tafel;
        }

        private int _reservatienummer;
        public int Reservatienummer
        {
            get
            {
                return _reservatienummer;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ReservatieException("Reservatienummer is ongeldig! (Groter dan 0)");
                }
                else
                {
                    _reservatienummer = value;
                }
            }
        }

        private Restaurant _restaurantinfo;
        public Restaurant Restaurantinfo
        {
            get
            {
                return _restaurantinfo;
            }
            set
            {
                if (value == null)
                {
                    throw new ReservatieException("Restaurantinfo is ongeldig!");
                }
                else
                {
                    _restaurantinfo = value;
                }
            }
        }

        private Gebruiker _contactpersoon;
        public Gebruiker Contactpersoon
        {
            get
            {
                return _contactpersoon;
            }
            set
            {
                if (value == null)
                {
                    throw new ReservatieException("Contactpersoon is ongeldig!");
                }
                else
                {
                    _contactpersoon = value;
                }
            }
        }

        private int _aantalPlaatsen;
        public int AantalPlaatsen
        {
            get
            {
                return _aantalPlaatsen;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ReservatieException("Aantal plaatsen is ongeldig! (Groter dan 0)");
                }
                else
                {
                    _aantalPlaatsen = value;
                }
            }
        }

        private DateTime _datum;
        public DateTime Datum
        {
            get
            {
                return _datum;
            }
            set
            {
                if (value < DateTime.Now)
                {
                    throw new ReservatieException("Datum is ongeldig! (Toekomst)");
                }
                else
                {
                    _datum = value;
                }
            }
        }

        private Tafel _tafel;
        public Tafel Tafel
        {
            get
            {
                return _tafel;
            }
            set
            {
                if (value == null)
                {
                    throw new ReservatieException("Tafel is ongeldig!");
                }
                else
                {
                    _tafel = value;
                }
            }
        }
    }
}
using Eindopdracht.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.BL.Models
{
    public class Reservatie
    {
        public Reservatie(int reservatienummer, Restaurant restaurantinfo, Gebruiker contactpersoon, int aantalPlaatsen, DateTime datum, TimeSpan uur, int tafelnummer)
        {
            _reservatienummer = reservatienummer;
            _restaurantinfo = restaurantinfo;
            _contactpersoon = contactpersoon;
            _aantalPlaatsen = aantalPlaatsen;
            _datum = datum;
            _uur = uur;
            _tafelnummer = tafelnummer;
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
                    throw new ReservatieException("Reservatienummer is ongeldig!");
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
                    throw new ReservatieException("Aantal plaatsen is ongeldig!");
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
                    throw new ReservatieException("Datum is ongeldig!");
                }
                else
                {
                    _datum = value;
                }
            }
        }

        private TimeSpan _uur;
        public TimeSpan Uur
        {
            get
            {
                return _uur;
            }
            set
            {
                if (value < TimeSpan.Zero)
                {
                    throw new ReservatieException("Uur is ongeldig!");
                }
                else
                {
                    _uur = value;
                }
            }
        }

        private int _tafelnummer;
        public int Tafelnummer
        {
            get
            {
                return _tafelnummer;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ReservatieException("Tafelnummer is ongeldig!");
                }
                else
                {
                    _tafelnummer = value;
                }
            }
        }
    }
}
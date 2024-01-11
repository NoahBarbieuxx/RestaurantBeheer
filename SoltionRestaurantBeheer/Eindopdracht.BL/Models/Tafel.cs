using Eindopdracht.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.BL.Models
{
    public class Tafel
    {
        public Tafel(int tafelId, string tafelnummer, int plaatsen, Restaurant restaurant)
        {
            TafelId = tafelId;
            Tafelnummer = tafelnummer;
            Plaatsen = plaatsen;
            Restaurant = restaurant;
        }

        public Tafel(string tafelnummer, int plaatsen, Restaurant restaurant)
        {
            Tafelnummer = tafelnummer;
            Plaatsen = plaatsen;
            Restaurant = restaurant;
        }

        private int _tafelId;
        public int TafelId
        {
            get
            {
                return _tafelId;
            }
            set
            {
                if (value <= 0)
                {
                    throw new TafelException("TafelId is ongeldig!");
                }
            }
        }

        private string _tafelnummer;
        public string Tafelnummer
        {
            get
            {
                return _tafelnummer;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new TafelException("Tafelnummer is ongeldig!");
                }
                else
                {
                    _tafelnummer = value;
                }
            }
        }

        private int _plaatsen;
        public int Plaatsen
        {
            get
            {
                return _plaatsen;
            }
            set
            {
                if (value <= 0)
                {
                    throw new TafelException("PLaatsen is ongeldig!");
                }
                else 
                {
                    _plaatsen = value;
                }
            }
        }

        private Restaurant _restaurant;
        public Restaurant Restaurant
        {
            get
            {
                return _restaurant;
            }
            set
            {
                if (value == null)
                {
                    throw new TafelException("Restaurant is ongeldig!");
                }
                else
                {
                    _restaurant = value;
                }
            }
        }
    }
}
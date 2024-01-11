using Eindopdracht.DL.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Eindopdracht.BL.Models;

namespace EindopdrachtGebruiker.REST.Models.Output
{
    public class ReservatieOutput
    {
        public ReservatieOutput(int reservatienummer, int aantalPlaatsen, DateTime datum, Tafel tafel, Restaurant restaurant, Gebruiker gebruiker)
        {
            Reservatienummer = reservatienummer;
            AantalPlaatsen = aantalPlaatsen;
            Datum = datum;
            Tafel = tafel;
            Restaurant = restaurant;
            Gebruiker = gebruiker;
        }

        public int Reservatienummer { get; set; }
        public int AantalPlaatsen { get; set; }
        public DateTime Datum { get; set; }
        public Tafel Tafel { get; set; }
        public Restaurant Restaurant { get; set; }
        public Gebruiker Gebruiker { get; set; }
    }
}
using Eindopdracht.BL;
using Eindopdracht.BL.Models;

namespace Eindopdracht.REST.Models
{
    public class ReservatieOutput
    {
        public ReservatieOutput(int reservatienummer, int aantalPlaatsen, DateTime datum, TimeSpan uur, int tafelnummer, Gebruiker gebruiker, Restaurant restaurant)
        {
            Reservatienummer = reservatienummer;
            AantalPlaatsen = aantalPlaatsen;
            Datum = datum;
            Uur = uur;
            Tafelnummer = tafelnummer;
            Gebruiker = gebruiker;
            Restaurant = restaurant;
        }

        public int Reservatienummer { get; set; }
        public int AantalPlaatsen { get; set; }
        public DateTime Datum { get; set; }
        public TimeSpan Uur { get; set; }
        public int Tafelnummer { get; set; }
        public Gebruiker Gebruiker { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}

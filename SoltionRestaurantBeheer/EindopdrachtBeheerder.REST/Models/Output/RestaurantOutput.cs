using Eindopdracht.BL.Models;

namespace EindopdrachtBeheerder.REST.Models.Output
{
    public class RestaurantOutput
    {
        public RestaurantOutput(string naam, Locatie locatie, string keuken, Contactgegevens contactgegevens, List<Tafel> tafels)
        {
            Naam = naam;
            Locatie = locatie;
            Keuken = keuken;
            Contactgegevens = contactgegevens;
            Tafels = tafels;
        }

        public string Naam { get; set; }
        public Locatie Locatie { get; set; }
        public string Keuken { get; set; }
        public Contactgegevens Contactgegevens { get; set; }
        public List<Tafel> Tafels { get; set; }
    }
}
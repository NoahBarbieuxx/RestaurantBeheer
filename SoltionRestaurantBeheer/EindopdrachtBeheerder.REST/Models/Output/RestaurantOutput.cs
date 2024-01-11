using Eindopdracht.BL.Models;

namespace EindopdrachtBeheerder.REST.Models.Output
{
    public class RestaurantOutput
    {
        public RestaurantOutput(string naam, Locatie locatie, string keuken, Contactgegevens contactgegevens)
        {
            Naam = naam;
            Locatie = locatie;
            Keuken = keuken;
            Contactgegevens = contactgegevens;
        }

        public string Naam { get; set; }
        public Locatie Locatie { get; set; }
        public string Keuken { get; set; }
        public Contactgegevens Contactgegevens { get; set; }
    }
}
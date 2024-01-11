using Eindopdracht.BL.Models;

namespace EindopdrachtBeheerder.REST.Models.Input
{
    public class RestaurantInput
    {
        public RestaurantInput(string naam, Locatie locatie, string keuken, Contactgegevens contactgegevens)
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
using Eindopdracht.BL.Models;

namespace EindopdrachtBeheerder.REST.Models.Input
{
    public class RestaurantInput
    {
        public Locatie Locatie { get; set; }
        public string Keuken { get; set; }
        public Contactgegevens Contactgegevens { get; set; }
    }
}
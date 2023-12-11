namespace Eindopdracht.REST.Models
{
    public class ReservatieInput
    {
        //public int Reservatienummer { get; set; }
        public int AantalPlaatsen { get; set; }
        public DateTime Datum { get; set; }
        public TimeSpan Uur { get; set; }
        public int Tafelnummer { get; set; }
    }
}
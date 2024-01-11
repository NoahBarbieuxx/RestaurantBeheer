namespace EindopdrachtBeheerder.REST.Models.Output
{
    public class TafelOutput
    {
        public TafelOutput(int tafelId, string tafelnummer, int plaatsen)
        {
            TafelId = tafelId;
            Tafelnummer = tafelnummer;
            Plaatsen = plaatsen;
        }

        public int TafelId { get; set; }
        public string Tafelnummer { get; set; }
        public int Plaatsen { get; set; }
    }
}
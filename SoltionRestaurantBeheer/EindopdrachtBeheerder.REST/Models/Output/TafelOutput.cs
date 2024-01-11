using Eindopdracht.BL.Models;

namespace EindopdrachtBeheerder.REST.Models.Output
{
    public class TafelOutput
    {
        public TafelOutput(int tafelId, int plaatsen)
        {
            TafelId = tafelId;
            Plaatsen = plaatsen;
        }

        public int TafelId { get; set; }
        public int Plaatsen { get; set; }
    }
}
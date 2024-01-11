using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.DL.Model
{
    public class ReservatieEF
    {
        public ReservatieEF()
        {

        }

        public ReservatieEF(int reservatienummer, RestaurantEF restaurant, GebruikerEF gebruiker, int aantalPlaatsen, DateTime datum, TafelEF tafel)
        {
            Reservatienummer = reservatienummer;
            Restaurant = restaurant;
            Gebruiker = gebruiker;
            AantalPlaatsen = aantalPlaatsen;
            Datum = datum;
            Tafel = tafel;
        }

        [Key]
        public int Reservatienummer { get; set; }

        [Required]
        public int AantalPlaatsen { get; set; }

        [Column(TypeName = "datetime")]
        [Required]
        public DateTime Datum { get; set; }

        public TafelEF Tafel { get; set; }
        public RestaurantEF Restaurant { get; set; }
        public GebruikerEF Gebruiker { get; set; }
    }
}
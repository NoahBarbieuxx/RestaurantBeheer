using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.DL.Model
{
    public class TafelEF
    {
        public TafelEF()
        {
        }

        public TafelEF(int tafelId, string tafelnummer, int plaatsen, RestaurantEF restaurant)
        {
            TafelId = tafelId;
            Tafelnummer = tafelnummer;
            Plaatsen = plaatsen;
            Restaurant = restaurant;
        }

        [Key]
        public int TafelId { get; set; }

        [Column(TypeName = "varchar(250)")]
        [Required]
        public string Tafelnummer { get; set; }

        [Required]
        public int Plaatsen { get; set; }

        public RestaurantEF Restaurant { get; set; }
    }
}
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

        public TafelEF(int tafelId, int plaatsen)
        {
            TafelId = tafelId;
            Plaatsen = plaatsen;
        }

        [Key]
        public int TafelId { get; set; }

        [Required]
        public int Plaatsen { get; set; }

        public RestaurantEF Restaurant { get; set; }
    }
}
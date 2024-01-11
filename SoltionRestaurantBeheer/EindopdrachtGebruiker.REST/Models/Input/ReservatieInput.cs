using Eindopdracht.DL.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Eindopdracht.BL.Models;

namespace EindopdrachtGebruiker.REST.Models.Input
{
    public class ReservatieInput
    {
        public int AantalPlaatsen { get; set; }
        public DateTime Datum { get; set; }
    }
}
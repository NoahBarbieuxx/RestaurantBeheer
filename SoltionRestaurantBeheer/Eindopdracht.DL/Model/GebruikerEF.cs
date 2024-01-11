using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Eindopdracht.DL.Model
{
    public class GebruikerEF
    {
        public GebruikerEF()
        {

        }

        public GebruikerEF(int klantnummer, string naam, string email, string telefoonnummer, int actief, string postcode, string gemeentenaam, string straatnaam, string huisnummer)
        {
            Klantnummer = klantnummer;
            Naam = naam;
            Email = email;
            Telefoonnummer = telefoonnummer;
            Actief = actief;
            Postcode = postcode;
            Gemeentenaam = gemeentenaam;
            Straatnaam = straatnaam;
            Huisnummer = huisnummer;
        }

        [Key]
        public int Klantnummer { get; set; }

        [Column(TypeName = "varchar(250)")]
        [Required]
        public string Naam { get; set; }

        [Column(TypeName = "varchar(250)")]
        [Required]
        public string Email { get; set; }

        [Column(TypeName = "varchar(250)")]
        [Required]
        public string Telefoonnummer { get; set; }

        [Required]
        public int Actief { get; set; }

        [Column(TypeName = "varchar(4)")]
        [Required]
        public string Postcode { get; set; }

        [Column(TypeName = "varchar(250)")]
        [Required]
        public string Gemeentenaam { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string Straatnaam { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Huisnummer { get; set; }

        public List<ReservatieEF> Reservaties { get; set; } = new List<ReservatieEF>();
    }
}
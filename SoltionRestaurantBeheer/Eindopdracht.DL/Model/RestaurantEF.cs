using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.DL.Model
{
    public class RestaurantEF
    {
        public RestaurantEF(string naam, string postcode, string gemeentenaam, string straatnaam, string huisnummer, string keuken, string email, string telefoonnummer)
        {
            Naam = naam;
            Postcode = postcode;
            Gemeentenaam = gemeentenaam;
            Straatnaam = straatnaam;
            Huisnummer = huisnummer;
            Keuken = keuken;
            Email = email;
            Telefoonnummer = telefoonnummer;
        }

        public RestaurantEF(string naam, string postcode, string gemeentenaam, string straatnaam, string huisnummer, string keuken, string email, string telefoonnummer, List<TafelEF> tafels)
        {
            Naam = naam;
            Postcode = postcode;
            Gemeentenaam = gemeentenaam;
            Straatnaam = straatnaam;
            Huisnummer = huisnummer;
            Keuken = keuken;
            Email = email;
            Telefoonnummer = telefoonnummer;
            Tafels = tafels;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Naam { get; set; }

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

        [Column(TypeName = "varchar(250)")]
        [Required]
        public string Keuken { get; set; }

        [Column(TypeName = "varchar(250)")]
        [Required]
        public string Email { get; set; }

        [Column(TypeName = "varchar(250)")]
        [Required]
        public string Telefoonnummer { get; set; }

        public List<ReservatieEF> Reservaties { get; set; } = new List<ReservatieEF>();
        public List<TafelEF> Tafels { get; set; } = new List<TafelEF>();
    }
}
using Eindopdracht.BL.Models;
using System;

namespace EindopdrachtGebruiker.REST.Models.Output
{
    public class GebruikerOutput
    {
        public GebruikerOutput(int klantnummer, string naam, string email, int actief, Locatie locatie)
        {
            Klantnummer = klantnummer;
            Naam = naam;
            Email = email;
            Actief = actief;
            Locatie = locatie;
        }

        public int Klantnummer { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public int Actief { get; set; }
        public Locatie Locatie { get; set; }
    }
}
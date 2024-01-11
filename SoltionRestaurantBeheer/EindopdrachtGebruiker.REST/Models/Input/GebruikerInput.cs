using Eindopdracht.BL.Models;
using System;

namespace EindopdrachtGebruiker.REST.Models.Input
{
    public class GebruikerInput
    {
        public GebruikerInput(string naam, string email, string telefoonnummer, Locatie locatie)
        {
            Naam = naam;
            Email = email;
            Telefoonnummer = telefoonnummer;
            Locatie = locatie;
        }

        public string Naam { get; set; }
        public string Email { get; set; }
        public string Telefoonnummer { get; set; }
        public Locatie Locatie { get; set; }
    }
}
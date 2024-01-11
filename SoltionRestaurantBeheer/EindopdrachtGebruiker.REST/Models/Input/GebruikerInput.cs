using Eindopdracht.BL.Models;
using System;

namespace EindopdrachtGebruiker.REST.Models.Input
{
    public class GebruikerInput
    {
        public string Naam { get; set; }
        public string Email { get; set; }
        public string Telefoonnummer { get; set; }
        public int Actief { get; set; }
        public Locatie Locatie { get; set; }
    }
}
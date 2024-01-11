using Eindopdracht.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.BL.Interfaces
{
    public interface IGebruikerRepository
    {
        // CRUD
        void RegistreerGebruiker(Gebruiker gebruiker); 
        Gebruiker GeefGebruikerById(int klantnummer);
        void PasGebruikerAan(Gebruiker gebruiker);
        void SchrijfGebruikerUit(int klantnummer);

        // ANDERE
        bool HeeftGebruiker(Gebruiker gebruiker);
    }
}
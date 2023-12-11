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
        void RegistreerGebruiker(Gebruiker gebruiker);
        void PasGebruikerAan(Gebruiker gebruiker);
        void SchrijfGebruikerUit(Gebruiker gebruiker);
        bool HeeftGebruiker(int klantnummer);
        Gebruiker GeefGebruikerById(int klantnummer);
    }
}
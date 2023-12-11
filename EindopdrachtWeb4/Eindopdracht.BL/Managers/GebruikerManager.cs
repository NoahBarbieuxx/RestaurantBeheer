using Eindopdracht.BL;
using Eindopdracht.BL.Exceptions;
using Eindopdracht.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.BL.Managers
{
    public class GebruikerManager
    {
        private readonly IGebruikerRepository _repo;

        public GebruikerManager(IGebruikerRepository repo)
        {
            _repo = repo;
        }

        public void RegistreerGebruiker(Gebruiker gebruiker)
        {
            try
            {
                _repo.RegistreerGebruiker(gebruiker);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("RegistreerGebruiker", ex);
            }
        }

        public void PasGebruikerAan(Gebruiker gebruiker)
        {
            try
            {
                _repo.PasGebruikerAan(gebruiker);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("PasGebruikerAan", ex);
            }
        }

        public void SchrijfGebruikerUit(Gebruiker gebruiker)
        {
            try
            {
                _repo.SchrijfGebruikerUit(gebruiker);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("SchrijfGebruikerUit", ex);
            }
        }

        public bool HeeftGebruiker(int klantnummer)
        {
            try
            {
                return _repo.HeeftGebruiker(klantnummer);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("HeeftGebruiker", ex);
            }
        }

        public Gebruiker GeefGebruikerById(int klantnummer)
        {
            try
            {
                return _repo.GeefGebruikerById(klantnummer);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("GeefGebruikerById", ex);
            }
        }
    }
}
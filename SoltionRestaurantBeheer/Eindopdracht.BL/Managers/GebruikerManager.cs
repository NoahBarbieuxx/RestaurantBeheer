using Eindopdracht.BL.Exceptions;
using Eindopdracht.BL.Interfaces;
using Eindopdracht.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.BL.Managers
{
    public class GebruikerManager
    {
        private readonly IGebruikerRepository _gebruikerRepository;

        public GebruikerManager(IGebruikerRepository gebruikerRepository)
        {
            _gebruikerRepository = gebruikerRepository;
        }

        public virtual void RegistreerGebruiker(Gebruiker gebruiker)
        {
            try
            {
                _gebruikerRepository.RegistreerGebruiker(gebruiker);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("RegistreerGebruiker", ex);
            }
        }

        public virtual Gebruiker GeefGebruikerById(int klantnummer)
        {
            try
            {
                return _gebruikerRepository.GeefGebruikerById(klantnummer);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("GeefGebruikerById", ex);
            }
        }

        public virtual void PasGebruikerAan(Gebruiker gebruiker)
        {
            try
            {
                _gebruikerRepository.PasGebruikerAan(gebruiker);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("PasGebruikerAan", ex);
            }
        }

        public virtual void SchrijfGebruikerUit(int klantnummer)
        {
            try
            {
                _gebruikerRepository.SchrijfGebruikerUit(klantnummer);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("SchrijfGebruikerUit", ex);
            }
        }

        public virtual bool HeeftGebruiker(Gebruiker gebruiker)
        {
            try
            {
                return _gebruikerRepository.HeeftGebruiker(gebruiker);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("HeeftGebruiker", ex);
            }
        }
    }
}
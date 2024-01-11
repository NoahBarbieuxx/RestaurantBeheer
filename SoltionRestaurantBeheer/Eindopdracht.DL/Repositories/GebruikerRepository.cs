using Eindopdracht.BL.Interfaces;
using Eindopdracht.BL.Models;
using Eindopdracht.DL.Exceptions;
using Eindopdracht.DL.Mappers;
using Eindopdracht.DL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.DL.Repositories
{
    public class GebruikerRepository : IGebruikerRepository
    {
        private readonly RestaurantBeheerContext _ctx;

        public GebruikerRepository(string connectionString)
        {
            _ctx = new RestaurantBeheerContext(connectionString);
        }

        public Gebruiker GeefGebruikerById(int klantnummer)
        {
            try
            {
                GebruikerEF gebruikerEF = _ctx.Gebruikers
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Klantnummer == klantnummer);

                if (gebruikerEF == null)
                {
                    return null;
                }
                else
                {
                    return MapGebruiker.MapToDomain(gebruikerEF);
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefGebruikerById - Repository", ex);
            }
        }

        public void RegistreerGebruiker(Gebruiker gebruiker)
        {
            try
            {
                GebruikerEF gebruikerEF = MapGebruiker.MapToDB(gebruiker);
                _ctx.Gebruikers.Add(gebruikerEF);
                SaveAndClear();
                gebruiker.Klantnummer = gebruikerEF.Klantnummer;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("RegistreerGebruiker - Repository", ex);
            }
        }

        public void PasGebruikerAan(Gebruiker gebruiker)
        {
            try
            {
                if (gebruiker == null)
                {
                    throw new RepositoryException("Gebruiker is null");
                }
                else
                {
                    _ctx.Gebruikers.Update(MapGebruiker.MapToDB(gebruiker));
                    SaveAndClear();
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException("PasGebruikerAan - Repository", ex);
            }
        }


        public void SchrijfGebruikerUit(int klantnummer)
        {
            try
            {
                GebruikerEF gebruikerEF = _ctx.Gebruikers.FirstOrDefault(x => x.Klantnummer == klantnummer);

                gebruikerEF.Actief = 0;

                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("SchrijfGebruikerUit - Repository", ex);
            }
        }

        public bool HeeftGebruiker(Gebruiker gebruiker)
        {
            try
            {
                return _ctx.Gebruikers.Any(x => x.Email == gebruiker.Email && x.Telefoonnummer == gebruiker.Telefoonnummer);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("HeeftGebruiker - Repository", ex);
            }
        }

        private void SaveAndClear()
        {
            _ctx.SaveChanges();
            _ctx.ChangeTracker.Clear();
        }
    }
}
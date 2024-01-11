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

        public void RegistreerGebruiker(Gebruiker gebruiker)
        {
            try
            {
                if (!HeeftGebruiker(gebruiker))
                {
                    GebruikerEF gebruikerEF = MapGebruiker.MapToDB(gebruiker);
                    _ctx.Gebruikers.Add(gebruikerEF);
                    SaveAndClear();
                    gebruiker.Klantnummer = gebruikerEF.Klantnummer;
                }
                else
                {
                    throw new GebruikerRepositoryException("Gebruiker bestaat al in het systeem! (Email & telefoonnummer)");
                }
            }
            catch (Exception ex)
            {
                throw new GebruikerRepositoryException("RegistreerGebruiker", ex);
            }
        }

        public Gebruiker GeefGebruikerById(int klantnummer)
        {
            try
            {
                if (_ctx.Gebruikers.Any(x => x.Klantnummer == klantnummer))
                {
                    GebruikerEF gebruikerEF = _ctx.Gebruikers
                        .AsNoTracking()
                        .FirstOrDefault(x => x.Klantnummer == klantnummer);

                    return MapGebruiker.MapToDomain(gebruikerEF);
                }
                else
                {
                    throw new GebruikerRepositoryException($"Gebruiker met klantnummer: {klantnummer} niet gevonden!");
                }
            }
            catch (Exception ex)
            {
                throw new GebruikerRepositoryException("GeefGebruikerById", ex);
            }
        }

        public void PasGebruikerAan(Gebruiker gebruiker)
        {
            try
            {
                if (_ctx.Gebruikers.Any(x => x.Klantnummer == gebruiker.Klantnummer))
                {
                    _ctx.Gebruikers.Update(MapGebruiker.MapToDB(gebruiker));
                    SaveAndClear();
                }
                else
                {
                    throw new GebruikerRepositoryException($"Gebruiker met klantnummer: {gebruiker.Klantnummer} niet gevonden!");
                }
            }
            catch (Exception ex)
            {
                throw new GebruikerRepositoryException("PasGebruikerAan", ex);
            }
        }

        public void SchrijfGebruikerUit(int klantnummer)
        {
            try
            {
                GebruikerEF gebruikerEF = _ctx.Gebruikers.Find(klantnummer);

                if (gebruikerEF != null)
                {
                    gebruikerEF.Actief = 0;
                    SaveAndClear();
                }
                else
                {
                    throw new GebruikerRepositoryException($"Gebruiker met klantnummer: {klantnummer} niet gevonden!");
                }
            }
            catch (Exception ex)
            {
                throw new GebruikerRepositoryException("SchrijfGebruikerUit", ex);
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
                throw new GebruikerRepositoryException("HeeftGebruiker", ex);
            }
        }

        private void SaveAndClear()
        {
            _ctx.SaveChanges();
            _ctx.ChangeTracker.Clear();
        }
    }
}
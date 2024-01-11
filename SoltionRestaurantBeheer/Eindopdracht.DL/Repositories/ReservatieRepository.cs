using Eindopdracht.BL.Exceptions;
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
    public class ReservatieRepository : IReservatieRepository
    {
        private readonly RestaurantBeheerContext _ctx;

        public ReservatieRepository(string connectionString)
        {
            _ctx = new RestaurantBeheerContext(connectionString);
        }
        public Reservatie GeefReservatieById(int reservatienummer)
        {
            try
            {
                ReservatieEF reservatieEF = _ctx.Reservaties.Where(x => x.Reservatienummer == reservatienummer)
                    .Include(x => x.Restaurant)
                    .Include(x => x.Gebruiker)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (reservatieEF == null)
                {
                    return null;
                }
                else
                {
                    return MapReservatie.MapToDomain(reservatieEF);

                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefReservatieById - Repository", ex);
            }
        }

        public List<Reservatie> ZoekReservaties(DateTime datum)
        {
            try
            {
                List<Reservatie> reservaties = new List<Reservatie>();

                List<ReservatieEF> reservatiesEF = _ctx.Reservaties
                    .Where(x => x.Datum.Date == datum.Date)
                    .Include(x => x.Restaurant).ThenInclude(x => x.Tafels)
                    .Include(x => x.Gebruiker)
                    .AsNoTracking()
                    .ToList();

                foreach(ReservatieEF reservatieEF in reservatiesEF)
                {
                    Reservatie reservatie = MapReservatie.MapToDomain(reservatieEF);
                    reservaties.Add(reservatie);
                }

                return reservaties;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("ZoekReservaties - Repository", ex);
            }
        }

        public List<Reservatie> GeefOverzichtReservaties(string naam, DateTime datum)
        {
            try
            {
                List<Reservatie> reservaties = new List<Reservatie>();

                List<ReservatieEF> reservatiesEF = _ctx.Reservaties
                    .Where(x => x.Restaurant.Naam == naam)
                    .Where(x => x.Datum.Date == datum.Date)
                    .Include(x => x.Restaurant).ThenInclude(x => x.Tafels)
                    .Include(x => x.Gebruiker)
                    .AsNoTracking()
                    .ToList();


                foreach (ReservatieEF reservatieEF in reservatiesEF)
                {
                    Reservatie reservatie = MapReservatie.MapToDomain(reservatieEF);
                    reservaties.Add(reservatie);
                }

                return reservaties;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefOverzichtReservaties - Repository", ex);
            }
        }

        public void MaakReservatie(Reservatie reservatie)
        {
            try
            {
                ReservatieEF reservatieEF = MapReservatie.MapToDB(reservatie, _ctx);
                _ctx.Reservaties.Add(reservatieEF);
                SaveAndClear();
                reservatie.Reservatienummer = reservatieEF.Reservatienummer;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("MaakReservtie - Repository", ex);
            }
        }

        public void PasReservatieAan(Reservatie reservatie)
        {
            try
            {
                _ctx.Reservaties.Update(MapReservatie.MapToDB(reservatie, _ctx));
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("PasReservatieAan - Repository", ex);
            }
        }

        public void AnnuleerReservatie(int reservatienummer)
        {
            try
            {
                ReservatieEF reservatieEF = _ctx.Reservaties.FirstOrDefault(x => x.Reservatienummer == reservatienummer);
                _ctx.Reservaties.Remove(reservatieEF);
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("AnnuleerReservatie - Repository", ex);
            }
        }

        public bool HeeftReservatie(Reservatie reservatie)
        {
            try
            {
                return _ctx.Reservaties.Any(x => x.Datum == reservatie.Datum);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("HeeftReservatie - Repository", ex);
            }
        }
        private void SaveAndClear()
        {
            _ctx.SaveChanges();
            _ctx.ChangeTracker.Clear();
        }
    }
}
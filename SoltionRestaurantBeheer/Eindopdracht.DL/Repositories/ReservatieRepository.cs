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

        public void MaakReservatie(Reservatie reservatie)
        {
            try
            {
                if (!HeeftReservatie(reservatie))
                {
                    ReservatieEF reservatieEF = MapReservatie.MapToDB(reservatie, _ctx);
                    _ctx.Reservaties.Add(reservatieEF);
                    SaveAndClear();
                    reservatie.Reservatienummer = reservatieEF.Reservatienummer;
                }
                else
                {
                    throw new ReservatieRepositoryException($"Reservatie op tijdstip: {reservatie.Datum} bestaat al!");
                }
            }
            catch (Exception ex)
            {
                throw new ReservatieRepositoryException("MaakReservatie", ex);
            }
        }

        public Reservatie GeefReservatieById(int reservatienummer)
        {
            try
            {
                if (_ctx.Reservaties.Any(x => x.Reservatienummer == reservatienummer))
                {
                    ReservatieEF reservatieEF = _ctx.Reservaties.Where(x => x.Reservatienummer == reservatienummer)
                        .Include(x => x.Restaurant)
                        .Include(x => x.Tafel)
                        .Include(x => x.Gebruiker)
                        .AsNoTracking()
                        .FirstOrDefault();

                    return MapReservatie.MapToDomain(reservatieEF);
                }
                else
                {
                    throw new ReservatieRepositoryException($"Reservatie met reservatienummer: {reservatienummer} niet gevonden!");
                }


            }
            catch (Exception ex)
            {
                throw new ReservatieRepositoryException("GeefReservatieById", ex);
            }
        }

        public void PasReservatieAan(Reservatie reservatie)
        {
            try
            {
                if (_ctx.Reservaties.Any(x => x.Reservatienummer == reservatie.Reservatienummer))
                {
                    _ctx.Reservaties.Update(MapReservatie.MapToDB(reservatie, _ctx));
                    SaveAndClear();
                }
                else
                {
                    throw new ReservatieRepositoryException($"Reservatie met reservatienummer: {reservatie.Reservatienummer} niet gevonden!");
                }
            }
            catch (Exception ex)
            {
                throw new ReservatieRepositoryException("PasReservatieAan", ex);
            }
        }

        public void AnnuleerReservatie(int reservatienummer)
        {
            try
            {
                if (_ctx.Reservaties.Any(x => x.Reservatienummer == reservatienummer))
                {
                    ReservatieEF reservatieEF = _ctx.Reservaties.FirstOrDefault(x => x.Reservatienummer == reservatienummer);
                    _ctx.Reservaties.Remove(reservatieEF);
                    SaveAndClear();
                }
                else
                {
                    throw new ReservatieRepositoryException($"Reservatie met reservatienummer: {reservatienummer} niet gevonden!");
                }
            }
            catch (Exception ex)
            {
                throw new ReservatieRepositoryException("AnnuleerReservatie", ex);
            }
        }

        public List<Reservatie> ZoekReservaties(int klantnummer, DateTime? beginDatum)
        {
            try
            {
                if (_ctx.Gebruikers.Any(x => x.Klantnummer == klantnummer))
                {
                    GebruikerEF gebruikerEF = _ctx.Gebruikers.FirstOrDefault(x => x.Klantnummer == klantnummer);

                    IQueryable<ReservatieEF> query = _ctx.Reservaties
                        .Include(x => x.Restaurant).ThenInclude(x => x.Tafels)
                        .Include(x => x.Gebruiker)
                        .Include(x => x.Tafel)
                        .Where(x => x.Gebruiker.Klantnummer == klantnummer)
                        .AsNoTracking();

                    if (beginDatum.HasValue)
                    {
                        query = query.Where(x => x.Datum.Date == beginDatum.Value.Date);
                    }

                    return query.Select(x => MapReservatie.MapToDomain(x)).ToList();
                }
                else
                {
                    throw new ReservatieRepositoryException($"Gebruiker met klantnummer: {klantnummer} niet gevonden!");
                }
            }
            catch (Exception ex)
            {
                throw new ReservatieRepositoryException("ZoekReservaties", ex);
            }
        }

        public List<Reservatie> GeefOverzichtReservaties(string naam, DateTime beginDatum, DateTime? eindDatum)
        {
            try
            {
                if (_ctx.Restaurants.Any(x => x.Naam == naam))
                {
                    IQueryable<ReservatieEF> query = _ctx.Reservaties
                        .Include(x => x.Restaurant)
                        .Include(x => x.Gebruiker)
                        .Include(x => x.Tafel)
                        .Where(x => x.Restaurant.Naam == naam)
                        .AsNoTracking();

                    if (!eindDatum.HasValue)
                    {
                        query = query.Where(x => x.Datum.Date == beginDatum.Date);
                    }
                    if (eindDatum.HasValue && eindDatum > beginDatum)
                    {
                        query = query.Where(x => x.Datum.Date >= beginDatum.Date && x.Datum.Date <= eindDatum.Value.Date);
                    }

                    return query.Select(x => MapReservatie.MapToDomain(x)).ToList();
                }
                else
                {
                    throw new ReservatieRepositoryException($"Geen restaurant gevonden met naam: {naam}!");
                }

            }
            catch (Exception ex)
            {
                throw new ReservatieRepositoryException("GeefOverzichtReservaties", ex);
            }
        }

        public bool HeeftReservatie(Reservatie reservatie)
        {
            try
            {
                DateTime eindTijdReservatie = reservatie.Datum.AddHours(1.5);

                return _ctx.Reservaties.Any(x =>
                    x.Datum >= reservatie.Datum &&
                    x.Datum < eindTijdReservatie);
            }
            catch (Exception ex)
            {
                throw new ReservatieRepositoryException("HeeftReservatie", ex);
            }
        }

        private void SaveAndClear()
        {
            _ctx.SaveChanges();
            _ctx.ChangeTracker.Clear();
        }
    }
}
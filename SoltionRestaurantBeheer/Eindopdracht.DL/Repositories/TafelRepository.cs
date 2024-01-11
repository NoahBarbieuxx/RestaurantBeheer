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
    public class TafelRepository : ITafelRepository
    {
        private readonly RestaurantBeheerContext _ctx;

        public TafelRepository(string connectionString)
        {
            _ctx = new RestaurantBeheerContext(connectionString);
        }

        public Tafel GeefTafelById(int tafelId)
        {
            try
            {
                TafelEF tafelEF = _ctx.Tafels
                    .Include(x => x.Restaurant)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.TafelId == tafelId);

                if (tafelEF == null)
                {
                    return null;
                }
                else
                {
                    return MapTafel.MapToDomain(tafelEF);
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefTafelById - Repository", ex);
            }
        }

        public Tafel GeefTafelByNummer(string nummer)
        {
            try
            {
                TafelEF tafelEF = _ctx.Tafels
                    .Include(x => x.Restaurant)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Tafelnummer == nummer);

                return MapTafel.MapToDomain(tafelEF);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefTafelByNummer - Repository", ex);
            }
        }

        public List<Tafel> GeefTafelsByDatum(DateTime datum)
        {
            try
            {
                List<Tafel> tafels = new List<Tafel>();

                List<TafelEF> tafelsEF = _ctx.Tafels
                    .Include(x => x.Restaurant)
                    .AsNoTracking()
                    .ToList();

                foreach (TafelEF tafelEF in tafelsEF)
                {
                    Tafel tafel = MapTafel.MapToDomain(tafelEF);
                    tafels.Add(tafel);
                }

                return tafels;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefTafelsByDatum - Repository", ex);
            }
        }

        public void MaakTafel(Tafel tafel)
        {
            try
            {
                TafelEF tafelEF = MapTafel.MapToDB(tafel, _ctx);
                _ctx.Tafels.Add(tafelEF);
                SaveAndClear();
                tafel.TafelId = tafelEF.TafelId;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("MaakTafel - Repository", ex);
            }
        }

        public bool HeeftTafel(Tafel tafel)
        {
            try
            {
                return _ctx.Tafels.Any(x => x.Tafelnummer == tafel.Tafelnummer);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("HeeftRestaurant - Repository", ex);
            }
        }

        public Tafel KiesTafel(int plaatsen)
        {
            try
            {
                List<TafelEF> beschikbareTafels = _ctx.Tafels
                    .Include(x => x.Restaurant)
                    .Where(x => x.Plaatsen >= plaatsen)
                    .ToList();
                List<Tafel> tafels = new List<Tafel>();

                beschikbareTafels.Sort((t1, t2) => t1.Plaatsen.CompareTo(t2.Plaatsen));

                foreach (TafelEF tafelEF in beschikbareTafels)
                {
                    Tafel tafel = MapTafel.MapToDomain(tafelEF);
                    tafels.Add(tafel);
                }

                Tafel gekozenTafel = tafels.FirstOrDefault();

                if (gekozenTafel == null)
                {
                    throw new RepositoryException("KiesTafel - Geen geschikte tafel gevonden - Repository");
                }

                return gekozenTafel;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("KiesTafel - Repository", ex);
            }
        }

        private void SaveAndClear()
        {
            _ctx.SaveChanges();
            _ctx.ChangeTracker.Clear();
        }
    }
}
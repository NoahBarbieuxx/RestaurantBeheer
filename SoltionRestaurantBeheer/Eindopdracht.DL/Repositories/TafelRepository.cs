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

        public void MaakTafel(string naam, Tafel tafel)
        {
            try
            {
                RestaurantEF restaurantEF = _ctx.Restaurants
                    .FirstOrDefault(x => x.Naam == naam);

                if (restaurantEF != null)
                {
                    TafelEF tafelEF = MapTafel.MapToDB(tafel);
                    restaurantEF.Tafels.Add(tafelEF);

                    SaveAndClear();

                    tafel.TafelId = tafelEF.TafelId;
                }
                else
                {
                    throw new TafelRepositoryException($"Restaurant met naam: {naam} niet gevonden!");
                }
            }
            catch (Exception ex)
            {
                throw new TafelRepositoryException("MaakTafel", ex);
            }
        }

        public Tafel KiesTafel(int plaatsen)
        {
            try
            {
                List<TafelEF> beschikbareTafels = _ctx.Tafels
                    .Where(x => x.Plaatsen >= plaatsen)
                    .ToList();

                List<Tafel> tafels = new List<Tafel>();

                beschikbareTafels.Sort((t1, t2) => t1.Plaatsen.CompareTo(t2.Plaatsen));

                foreach (TafelEF tafelEF in beschikbareTafels)
                {
                    Tafel tafel = MapTafel.MapToDomain(tafelEF);
                    tafels.Add(tafel);
                }

                TafelEF gekozenTafel = beschikbareTafels.FirstOrDefault();

                if (gekozenTafel == null)
                {
                    throw new TafelRepositoryException("Geen geschikte tafel gevonden!");
                }

                return MapTafel.MapToDomain(gekozenTafel);
            }
            catch (Exception ex)
            {
                throw new TafelRepositoryException("KiesTafel", ex);
            }
        }

        private void SaveAndClear()
        {
            _ctx.SaveChanges();
            _ctx.ChangeTracker.Clear();
        }
























        Tafel ITafelRepository.GeefTafelById(int tafelId)
        {
            throw new NotImplementedException();
        }

        List<Tafel> ITafelRepository.GeefTafelsByDatum(DateTime datum)
        {
            throw new NotImplementedException();
        }














        //public Tafel GeefTafelById(int tafelId)
        //{
        //    try
        //    {
        //        TafelEF tafelEF = _ctx.Tafels
        //            .Include(x => x.Restaurant)
        //            .AsNoTracking()
        //            .FirstOrDefault(x => x.TafelId == tafelId);

        //        if (tafelEF == null)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            return MapTafel.MapToDomain(tafelEF);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new RepositoryException("GeefTafelById - Repository", ex);
        //    }
        //}

        //public List<Tafel> GeefTafelsByDatum(DateTime datum)
        //{
        //    try
        //    {
        //        List<Tafel> tafels = new List<Tafel>();

        //        List<TafelEF> tafelsEF = _ctx.Tafels
        //            .Include(x => x.Restaurant)
        //            .AsNoTracking()
        //            .ToList();

        //        foreach (TafelEF tafelEF in tafelsEF)
        //        {
        //            Tafel tafel = MapTafel.MapToDomain(tafelEF);
        //            tafels.Add(tafel);
        //        }

        //        return tafels;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new RepositoryException("GeefTafelsByDatum - Repository", ex);
        //    }
        //}






    }
}
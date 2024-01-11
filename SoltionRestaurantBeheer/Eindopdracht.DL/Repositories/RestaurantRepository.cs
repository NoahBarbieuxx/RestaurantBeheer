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
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantBeheerContext _ctx;

        public RestaurantRepository(string connectionString)
        {
            _ctx = new RestaurantBeheerContext(connectionString);
        }

        public void RegistreerRestaurant(Restaurant restaurant)
        {
            try
            {
                if (!HeeftRestaurant(restaurant))
                {
                    RestaurantEF restaurantEF = MapRestaurant.MapToDB(restaurant);
                    _ctx.Restaurants.Add(restaurantEF);
                    SaveAndClear();
                }
                else
                {
                    throw new RestaurantRepositoryException($"Restaurant met naam: {restaurant.Naam} bestaat al!");
                }
            }
            catch (Exception ex)
            {
                throw new RestaurantRepositoryException("RegistreerRestaurant", ex);
            }
        }

        public void PasRestaurantAan(Restaurant restaurant)
        {
            try
            {
                if (_ctx.Restaurants.Any(x => x.Naam == restaurant.Naam))
                {
                    _ctx.Restaurants.Update(MapRestaurant.MapToDB(restaurant));
                    SaveAndClear();
                }
                else
                {
                    throw new RestaurantRepositoryException($"Restaurant met naam: {restaurant.Naam} niet gevonden!");
                }
            }
            catch (Exception ex)
            {
                throw new RestaurantRepositoryException("PasRestaurantAan", ex);
            }
        }

        public void VerwijderRestaurant(string naam)
        {
            try
            {
                if (_ctx.Restaurants.Any(x => x.Naam == naam))
                {
                    RestaurantEF restaurantEF = _ctx.Restaurants.FirstOrDefault(x => x.Naam == naam);
                    _ctx.Restaurants.Remove(restaurantEF);
                    SaveAndClear();
                }
                else
                {
                    throw new RestaurantRepositoryException($"Restaurant met naam: {naam} niet gevonden!");
                }
            }
            catch (Exception ex)
            {
                throw new RestaurantRepositoryException("VerwijderRestaurant", ex);
            }
        }

        public bool HeeftRestaurant(Restaurant restaurant)
        {
            try
            {
                return _ctx.Restaurants.Any(x => x.Naam == restaurant.Naam);
            }
            catch (Exception ex)
            {
                throw new RestaurantRepositoryException("HeeftRestaurant", ex);
            }
        }

        public List<Restaurant> ZoekRestaurants(string? postcode, string? keuken)
        {
            try
            {
                IQueryable<RestaurantEF> query = _ctx.Restaurants;

                List<RestaurantEF> resultaten = new List<RestaurantEF>();

                if (!string.IsNullOrEmpty(postcode))
                {
                    var postcodeResultaten = query.Where(x => x.Postcode == postcode).ToList();
                    resultaten.AddRange(postcodeResultaten);
                }

                if (!string.IsNullOrEmpty(keuken))
                {
                    var keukenResultaten = query.Where(x => x.Keuken == keuken).ToList();
                    resultaten.AddRange(keukenResultaten);
                }

                if (string.IsNullOrEmpty(postcode) && string.IsNullOrEmpty(keuken))
                {
                    return query.Select(x => MapRestaurant.MapToDomain(x)).ToList();
                }

                List<Restaurant> gemapteResultaten = resultaten
                    .GroupBy(x => x.Naam)
                    .Select(group => group.First())
                    .Select(x => MapRestaurant.MapToDomain(x))
                    .ToList();

                return gemapteResultaten;
            }
            catch (Exception ex)
            {
                throw new RestaurantRepositoryException("ZoekRestaurants", ex);
            }
        }



        private void SaveAndClear()
        {
            _ctx.SaveChanges();
            _ctx.ChangeTracker.Clear();
        }
        
































        List<Restaurant> IRestaurantRepository.ZoekVrijeRestaurants(DateTime datum, int aantalPlaatsen, string? postcode, string? keuken)
        {
            throw new NotImplementedException();
        }




















        //public List<Restaurant> ZoekVrijeRestaurants(DateTime datum, int aantalPlaatsen, string? postcode, string? keuken)
        //{
        //    try
        //    {
        //        IQueryable<RestaurantEF> query = _ctx.Restaurants;

        //        query = query.Where(x => x.Reservaties.All(reservatie => reservatie.Datum == datum));

        //        List<Restaurant> resultaten = query
        //            .Select(x => MapRestaurant.MapToDomain(x))
        //            .ToList();

        //        return resultaten;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new RepositoryException("ZoekVrijeRestaurants - Repository", ex);
        //    }
        //}

















        public Restaurant GeefRestaurantByNaam(string naam)
        {
            try
            {
                RestaurantEF restaurantEF = _ctx.Restaurants
                    .AsNoTracking()
                    .Include(x => x.Tafels)
                    .FirstOrDefault(x => x.Naam == naam);

                if (restaurantEF == null)
                {
                    return null;
                }
                else
                {
                    return MapRestaurant.MapToDomain(restaurantEF);
                }
            }
            catch (Exception ex)
            {
                throw new RestaurantRepositoryException("GeefRestaurantByNaam - Repository", ex);
            }
        }








    }
}
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

        public Restaurant GeefRestaurantByNaam(string naam)
        {
            try
            {
                RestaurantEF restaurantEF = _ctx.Restaurants
                    .AsNoTracking()
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
                throw new RepositoryException("GeefRestaurantByNaam - Repository", ex);
            }
        }

    public List<Restaurant> ZoekRestaurants(string postcode, string keuken)
        {
            try
            {
                IQueryable<RestaurantEF> query = _ctx.Restaurants;

                if (!string.IsNullOrEmpty(postcode))
                {
                    query = query.Where(x => x.Postcode == postcode);
                }

                if (!string.IsNullOrEmpty(keuken))
                {
                    query = query.Where(x => x.Keuken == keuken);
                }

                List<Restaurant> resultaten = query
                    .Select(x => MapRestaurant.MapToDomain(x))
                    .ToList();

                return resultaten;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("ZoekRestaurants - Repository", ex);
            }
        }

        public List<Restaurant> ZoekVrijeRestaurants(DateTime datum, int aantalPlaatsen)
        {
            try
            {
                IQueryable<RestaurantEF> query = _ctx.Restaurants;

                query = query.Where(x => x.Reservaties.All(reservatie => reservatie.Datum == datum));

                List<Restaurant> resultaten = query
                    .Select(x => MapRestaurant.MapToDomain(x))
                    .ToList();

                return resultaten;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("ZoekVrijeRestaurants - Repository", ex);
            }
        }

        public void RegistreerRestaurant(Restaurant restaurant)
        {
            try
            {
                RestaurantEF restaurantEF = MapRestaurant.MapToDB(restaurant);
                _ctx.Restaurants.Add(restaurantEF);
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("RegistreerRestaurant - Repository", ex);
            }
        }

        public void PasRestaurantAan(Restaurant restaurant)
        {
            try
            {
                _ctx.Restaurants.Update(MapRestaurant.MapToDB(restaurant));
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("PasRestaurantAan - Repository", ex);
            }
        }

        public void VerwijderRestaurant(string naam)
        {
            try
            {
                RestaurantEF restaurantEF = _ctx.Restaurants.FirstOrDefault(x => x.Naam == naam);
                _ctx.Restaurants.Remove(restaurantEF);
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("VerwijderRestaurant - Repository", ex);
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
                throw new RepositoryException("HeeftRestaurant - Repository", ex);
            }
        }

        private void SaveAndClear()
        {
            _ctx.SaveChanges();
            _ctx.ChangeTracker.Clear();
        }
    }
}
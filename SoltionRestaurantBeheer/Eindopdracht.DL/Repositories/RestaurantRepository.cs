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

        public Restaurant GeefRestaurantByNaam(string naam, bool toonTafels)
        {
            try
            {
                if (_ctx.Restaurants.Any(x => x.Naam == naam))
                {
                    IQueryable<RestaurantEF> query = _ctx.Restaurants.AsNoTracking();

                    if (toonTafels)
                    {
                        query = query.Include(x => x.Tafels);
                    }

                    RestaurantEF restaurantEF = query.FirstOrDefault(x => x.Naam == naam);

                    return MapRestaurant.MapToDomain(restaurantEF);
                }
                else
                {
                    throw new RestaurantRepositoryException($"Restaurant met naam: {naam} niet gevonden!");
                }
            }
            catch (Exception ex)
            {
                throw new RestaurantRepositoryException("GeefRestaurantByNaam - Repository", ex);
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

        public List<Restaurant> ZoekVrijeRestaurants(DateTime datum, int aantalPlaatsen, string? postcode, string? keuken)
        {
            try
            {
                IQueryable<RestaurantEF> query = _ctx.Restaurants
                    .Include(x => x.Tafels)
                    .Include(x => x.Reservaties)
                    .AsNoTracking();

                DateTime eindTijdGrens = datum.AddMinutes(90);

                List<Restaurant> resultaten = query
                    .Where(restaurant =>
                        restaurant.Tafels.Any(tafel =>
                            tafel.Plaatsen >= aantalPlaatsen &&
                            !restaurant.Reservaties.Any(reservatie =>
                                (datum >= reservatie.Datum && datum < reservatie.Datum.AddMinutes(90)) ||
                                (eindTijdGrens > reservatie.Datum && eindTijdGrens <= reservatie.Datum.AddMinutes(90))
                            )
                        )
                    )
                    .Select(x => MapRestaurant.MapToDomain(x))
                    .ToList();

                foreach (Restaurant restaurant in resultaten)
                {
                    List<Tafel> tafelsTeVerwijderen = new List<Tafel>();

                    foreach (Tafel tafel in restaurant.Tafels)
                    {
                        if (tafel.Plaatsen < aantalPlaatsen)
                        {
                            tafelsTeVerwijderen.Add(tafel);
                        }
                    }

                    foreach (Tafel tafel in tafelsTeVerwijderen)
                    {
                        restaurant.Tafels.Remove(tafel);
                    }
                }
                if (!string.IsNullOrEmpty(postcode))
                {
                    var postcodeResultaten = query.Where(x => x.Postcode == postcode)
                        .Select(x => MapRestaurant.MapToDomain(x))
                        .ToList();
                    resultaten.AddRange(postcodeResultaten);
                }

                if (!string.IsNullOrEmpty(keuken))
                {
                    var keukenResultaten = query.Where(x => x.Keuken == keuken)
                         .Select(x => MapRestaurant.MapToDomain(x))
                        .ToList();
                    resultaten.AddRange(keukenResultaten);
                }

                return resultaten;
            }
            catch (Exception ex)
            {
                throw new ReservatieRepositoryException("ZoekVrijeRestaurants", ex);
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

        private void SaveAndClear()
        {
            _ctx.SaveChanges();
            _ctx.ChangeTracker.Clear();
        }
    }
}
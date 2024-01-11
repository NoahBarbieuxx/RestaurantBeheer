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
    public class RestaurantManager
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantManager(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public void RegistreerRestaurant(Restaurant restaurant)
        {
            try
            {
                _restaurantRepository.RegistreerRestaurant(restaurant);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("RegistreerRestaurant", ex);
            }
        }

        public Restaurant GeefRestaurantByNaam(string naam)
        {
            try
            {
                return _restaurantRepository.GeefRestaurantByNaam(naam);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("GeefRestaurantByNaam", ex);
            }
        }

        public void PasRestaurantAan(Restaurant restaurant)
        {
            try
            {
                _restaurantRepository.PasRestaurantAan(restaurant);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("PasRestaurantAan", ex);
            }
        }

        public void VerwijderRestaurant(string naam)
        {
            try
            {
                _restaurantRepository.VerwijderRestaurant(naam);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("VerwijderRestaurant", ex);
            }
        }

        public List<Restaurant> ZoekRestaurants(string? postcode, string? keuken)
        {
            try
            {
                return _restaurantRepository.ZoekRestaurants(postcode, keuken);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("ZoekRestaurants", ex);
            }
        }

        public List<Restaurant> ZoekVrijeRestaurants(DateTime datum, int aantalPlaatsen, string? postcode, string? keuken)
        {
            try
            {
                return _restaurantRepository.ZoekVrijeRestaurants(datum, aantalPlaatsen, postcode, keuken);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("ZoekVrijeRestaurants", ex);
            }
        }

        public bool HeeftRestaurant(Restaurant restaurant)
        {
            try
            {
                return _restaurantRepository.HeeftRestaurant(restaurant);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("HeeftRestaurant", ex);
            }
        }
    }
}
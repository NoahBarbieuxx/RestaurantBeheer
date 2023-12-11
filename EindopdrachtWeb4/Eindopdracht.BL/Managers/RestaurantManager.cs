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
        private readonly IRestaurantRepository _repo;

        public RestaurantManager(IRestaurantRepository repo)
        {
            _repo = repo;
        }

        public List<Restaurant> ZoekRestaurants(string postcode, string keuken)
        {
            try
            {
                return _repo.ZoekRestaurants(postcode, keuken);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("ZoekRestaurant", ex);
            }
        }

        public List<Restaurant> ZoekVrijeRestaurants(DateTime datum, int aantalPlaatsen)
        {
            try
            {
                return _repo.ZoekVrijeRestaurants(datum, aantalPlaatsen);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("ZoekVrijeRestaurants", ex);
            }
        }

        public Restaurant GeefRestaurantByNaam(string naam)
        {
            try
            {
                return _repo.GeefRestaurantByNaam(naam);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("GeefRestaurantByNaam", ex);
            }
        }
    }
}
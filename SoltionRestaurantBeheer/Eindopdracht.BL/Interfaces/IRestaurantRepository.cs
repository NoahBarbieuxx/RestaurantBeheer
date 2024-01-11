using Eindopdracht.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.BL.Interfaces
{
    public interface IRestaurantRepository
    {
        // CRUD
        void RegistreerRestaurant(Restaurant restaurant);
        Restaurant GeefRestaurantByNaam(string naam);
        void PasRestaurantAan(Restaurant restaurant);
        void VerwijderRestaurant(string naam);

        // ANDERE
        List<Restaurant> ZoekRestaurants(string postcode, string keuken);
        List<Restaurant> ZoekVrijeRestaurants(DateTime datum, int aantalPlaatsen);
        bool HeeftRestaurant(Restaurant restaurant);
    }
}
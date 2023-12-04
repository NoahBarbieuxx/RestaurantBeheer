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
        List<Restaurant> ZoekRestaurants(string postcode, string keuken);
        List<Restaurant> ZoekVrijeRestaurants(DateTime datum, int aantalPlaatsen);
    }
}
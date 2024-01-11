using Eindopdracht.BL.Models;
using Eindopdracht.DL.Exceptions;
using Eindopdracht.DL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.DL.Mappers
{
    public class MapRestaurant
    {
        public static RestaurantEF MapToDB(Restaurant restaurant)
        {
            try
            {
                return new RestaurantEF(restaurant.Naam, restaurant.Locatie.Postcode, restaurant.Locatie.Gemeentenaam, restaurant.Locatie.Straatnaam, restaurant.Locatie.Huisnummer, restaurant.Keuken, restaurant.Contactgegevens.Email, restaurant.Contactgegevens.Telefoonnummer);
            }
            catch (Exception ex)
            {
                throw new MapperException("MapRestaurant - MapToDB", ex);
            }
        }

        public static Restaurant MapToDomain(RestaurantEF restaurantEF)
        {
            try
            {
                Locatie locatie = new Locatie(restaurantEF.Postcode, restaurantEF.Gemeentenaam, restaurantEF.Straatnaam, restaurantEF.Huisnummer);
                Contactgegevens contactgegevens = new Contactgegevens(restaurantEF.Telefoonnummer, restaurantEF.Email);

                return new Restaurant(restaurantEF.Naam, locatie, restaurantEF.Keuken, contactgegevens);
            }
            catch (Exception ex)
            {
                throw new MapperException("MapRestaurant - MapToDomain", ex);
            }
        }
    }
}
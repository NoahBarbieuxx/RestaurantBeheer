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
                RestaurantEF restaurantEF;

                if (restaurant.Tafels == null)
                {
                    restaurantEF = new RestaurantEF(restaurant.Naam, restaurant.Locatie.Postcode, restaurant.Locatie.Gemeentenaam, restaurant.Locatie.Straatnaam, restaurant.Locatie.Huisnummer, restaurant.Keuken, restaurant.Contactgegevens.Email, restaurant.Contactgegevens.Telefoonnummer);

                }
                else
                {
                    List<TafelEF> tafelsEF = new List<TafelEF>();
                    foreach (Tafel tafel in restaurant.Tafels)
                    {
                        TafelEF tafelEF = MapTafel.MapToDB(tafel);
                        tafelsEF.Add(tafelEF);
                    }

                    restaurantEF = new RestaurantEF(restaurant.Naam, restaurant.Locatie.Postcode, restaurant.Locatie.Gemeentenaam, restaurant.Locatie.Straatnaam, restaurant.Locatie.Huisnummer, restaurant.Keuken, restaurant.Contactgegevens.Email, restaurant.Contactgegevens.Telefoonnummer, tafelsEF);
                }
                return restaurantEF;
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
                Restaurant restaurant;

                if (restaurantEF.Tafels == null)
                {
                    restaurant = new Restaurant(restaurantEF.Naam, locatie, restaurantEF.Keuken, contactgegevens);
                }
                else
                {
                    List<Tafel> tafels = new List<Tafel>();
                    foreach (TafelEF tafelEF in restaurantEF.Tafels)
                    {
                        Tafel tafel = MapTafel.MapToDomain(tafelEF);
                        tafels.Add(tafel);
                    }

                    restaurant = new Restaurant(restaurantEF.Naam, locatie, restaurantEF.Keuken, contactgegevens, tafels);
                }

                return restaurant;
            }
            catch (Exception ex)
            {
                throw new MapperException("MapRestaurant - MapToDomain", ex);
            }
        }
    }
}
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
    public class MapTafel
    {
        public static TafelEF MapToDB(Tafel tafel, RestaurantBeheerContext ctx)
        {
            try
            {
                RestaurantEF restaurantEF = ctx.Restaurants.Find(tafel.Restaurant.Naam);

                TafelEF tafelEF = new TafelEF(tafel.TafelId, tafel.Tafelnummer, tafel.Plaatsen, null);

                tafelEF.Restaurant = restaurantEF;

                return tafelEF;
                
            }
            catch (Exception ex)
            {
                throw new MapperException("MapTafel - MapToDB", ex);
            }
        }

        public static Tafel MapToDomain(TafelEF tafelEF)
        {
            try
            {
                Locatie locatie = new Locatie(tafelEF.Restaurant.Postcode, tafelEF.Restaurant.Gemeentenaam, tafelEF.Restaurant.Straatnaam, tafelEF.Restaurant.Huisnummer);
                Contactgegevens contactgegevens = new Contactgegevens(tafelEF.Restaurant.Telefoonnummer, tafelEF.Restaurant.Email);
                Restaurant restaurant = new Restaurant(tafelEF.Restaurant.Naam, locatie, tafelEF.Restaurant.Keuken, contactgegevens);

                return new Tafel(tafelEF.TafelId, tafelEF.Tafelnummer, tafelEF.Plaatsen, restaurant);
            }
            catch (Exception ex)
            {
                throw new MapperException("MapTafel - MapToDomain", ex);
            }
        }
    }
}
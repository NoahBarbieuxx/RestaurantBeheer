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
    public class MapReservatie
    {
        public static ReservatieEF MapToDB(Reservatie reservatie, RestaurantBeheerContext ctx)
        {
            try
            {
                GebruikerEF gebruikerEF = ctx.Gebruikers.Find(reservatie.Contactpersoon.Klantnummer);
                RestaurantEF restaurantEF = ctx.Restaurants.Find(reservatie.Restaurantinfo.Naam);

                ReservatieEF reservatieEF = new ReservatieEF(reservatie.Reservatienummer, null, null, reservatie.AantalPlaatsen, reservatie.Datum, MapTafel.MapToDB(reservatie.Tafel, ctx));

                reservatieEF.Gebruiker = gebruikerEF;
                reservatieEF.Restaurant = restaurantEF;

                return reservatieEF;
            }
            catch (Exception ex)
            {
                throw new MapperException("MapReservatie - MapToDB", ex);
            }
        }

        public static Reservatie MapToDomain(ReservatieEF reservatieEF)
        {
            try
            {
                return new Reservatie(reservatieEF.Reservatienummer, MapRestaurant.MapToDomain(reservatieEF.Restaurant), MapGebruiker.MapToDomain(reservatieEF.Gebruiker), reservatieEF.AantalPlaatsen, reservatieEF.Datum, MapTafel.MapToDomain(reservatieEF.Tafel));
            }
            catch (Exception ex)
            {
                throw new MapperException("MapReservatie - MapToDomain", ex);
            }
        }
    }
}
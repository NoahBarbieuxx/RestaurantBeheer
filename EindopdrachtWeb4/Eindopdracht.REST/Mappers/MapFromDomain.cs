using Eindopdracht.BL.Models;
using Eindopdracht.REST.Exceptions;
using Eindopdracht.REST.Models;

namespace Eindopdracht.REST.Mappers
{
    public class MapFromDomain
    {
        public static ReservatieOutput MapFromReservatieDomain(Reservatie reservatie)
        {
            try
            {
                return new ReservatieOutput(reservatie.Reservatienummer, reservatie.AantalPlaatsen, reservatie.Datum, reservatie.Uur, reservatie.Tafelnummer, reservatie.Contactpersoon, reservatie.Restaurantinfo);
            }
            catch (Exception ex)
            {
                throw new MapException("MapFromReservatieDomain", ex);
            }
        }
    }
}
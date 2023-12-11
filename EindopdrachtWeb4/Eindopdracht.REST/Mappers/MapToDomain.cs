using Eindopdracht.BL.Models;
using Eindopdracht.REST.Exceptions;
using Eindopdracht.REST.Models;

namespace Eindopdracht.REST.Mappers
{
    public class MapToDomain
    {
        public static Reservatie MapToReservatieDomain(ReservatieInput dto)
        {
            try
            {
                return new Reservatie(dto.AantalPlaatsen, dto.Datum, dto.Uur, dto.Tafelnummer);
            }
            catch (Exception ex)
            {
                throw new MapException("MapToReservatieDomain", ex);
            }
        }
    }
}

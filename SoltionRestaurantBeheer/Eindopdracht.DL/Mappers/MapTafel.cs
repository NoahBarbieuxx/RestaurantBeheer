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
        public static TafelEF MapToDB(Tafel tafel)
        {
            try
            {                
                return new TafelEF(tafel.TafelId, tafel.Plaatsen);
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
                return new Tafel(tafelEF.TafelId, tafelEF.Plaatsen);
            }
            catch (Exception ex)
            {
                throw new MapperException("MapTafel - MapToDomain", ex);
            }
        }

    }
}
using Eindopdracht.BL.Interfaces;
using Eindopdracht.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.DL.Models
{
    public class ReservatieRepositoryADO : IReservatieRepository
    {
        private readonly string _connectionString;

        public ReservatieRepositoryADO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AnnuleerReservatie(Reservatie reservatie)
        {
            throw new NotImplementedException();
        }

        public void MaakReservatie(Reservatie reservatie)
        {
            throw new NotImplementedException();
        }

        public void PasReservatieAan(Reservatie reservatie)
        {
            throw new NotImplementedException();
        }

        public List<Reservatie> ZoekReservaties(DateTime datum)
        {
            throw new NotImplementedException();
        }
    }
}
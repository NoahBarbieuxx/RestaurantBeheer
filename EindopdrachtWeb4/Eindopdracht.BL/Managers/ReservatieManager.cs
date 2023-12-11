using Eindopdracht.BL.Exceptions;
using Eindopdracht.BL.Interfaces;
using Eindopdracht.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Eindopdracht.BL.Managers
{
    public class ReservatieManager
    {
        private readonly IReservatieRepository _repo;

        public ReservatieManager(IReservatieRepository repo)
        {
            _repo = repo;
        }

        public Reservatie MaakReservatie(Reservatie reservatie)
        {
            try
            {
                _repo.MaakReservatie(reservatie);
                return reservatie;
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("MaakReservatie", ex);
            }
        }

        public void PasReservatieAan(Reservatie reservatie)
        {
            try
            {
                _repo.PasReservatieAan(reservatie);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("PasReservatieAan", ex);
            }
        }

        public void AnnuleerReservatie(Reservatie reservatie)
        {
            try
            {
                _repo.AnnuleerReservatie(reservatie);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("AnnuleerReservatie", ex);
            }
        }

        public List<Reservatie> ZoekReservaties(DateTime datum)
        {
            try
            {
                return _repo.ZoekReservaties(datum);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("ZoekReservaties", ex);
            }
        }

        public Reservatie GeefReservatieById(int reservatienummer)
        {
            try
            {
                return _repo.GeefReservatieById(reservatienummer);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("GeefReservatieById");
            }
        }
    }
}
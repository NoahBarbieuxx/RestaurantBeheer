using Eindopdracht.BL.Exceptions;
using Eindopdracht.BL.Interfaces;
using Eindopdracht.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.BL.Managers
{
    public class ReservatieManager
    {
        private readonly IReservatieRepository _reservatieRepository;

        public ReservatieManager(IReservatieRepository reservatieRepository)
        {
            _reservatieRepository = reservatieRepository;
        }

        public void MaakReservatie(Reservatie reservatie)
        {
            try
            {
                _reservatieRepository.MaakReservatie(reservatie);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("MaakReservtie", ex);
            }
        }

        public Reservatie GeefReservatieById(int reservatienummer)
        {
            try
            {
                return _reservatieRepository.GeefReservatieById(reservatienummer);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("GeefReservatieById", ex);
            }
        }

        public void PasReservatieAan(Reservatie reservatie)
        {
            try
            {
                _reservatieRepository.PasReservatieAan(reservatie);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("PasReservatieAan", ex);
            }
        }

        public void AnnuleerReservatie(int reservatienummer)
        {
            try
            {
                _reservatieRepository.AnnuleerReservatie(reservatienummer);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("AnnuleerReservatie", ex);
            }
        }

        public List<Reservatie> ZoekReservaties(DateTime startdatum, DateTime einddatum)
        {
            try
            {
                return _reservatieRepository.ZoekReservaties(startdatum, einddatum);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("ZoekReservaties", ex);
            }
        }

        public List<Reservatie> GeefOverzichtReservaties(string naam, DateTime startdatum, DateTime einddatum)
        {
            try
            {
                return _reservatieRepository.GeefOverzichtReservaties(naam, startdatum, einddatum);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("GeefOverzichtReservaties", ex);
            }
        }

        public bool HeeftReservatie(Reservatie reservatie)
        {
            try
            {
                return _reservatieRepository.HeeftReservatie(reservatie);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("HeeftReservatie", ex);
            }
        }
    }
}
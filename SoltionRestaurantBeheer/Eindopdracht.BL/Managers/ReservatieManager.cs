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

        public virtual void MaakReservatie(Reservatie reservatie)
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

        public virtual Reservatie GeefReservatieById(int reservatienummer)
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

        public virtual void PasReservatieAan(Reservatie reservatie)
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

        public virtual void AnnuleerReservatie(int reservatienummer)
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

        public virtual List<Reservatie> ZoekReservaties(int klantnummer, DateTime? startdatum)
        {
            try
            {
                return _reservatieRepository.ZoekReservaties(klantnummer, startdatum);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("ZoekReservaties", ex);
            }
        }

        public virtual List<Reservatie> GeefOverzichtReservaties(string naam, DateTime startdatum, DateTime? einddatum)
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

        public virtual bool HeeftReservatie(Reservatie reservatie)
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
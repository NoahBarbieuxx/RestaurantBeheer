using Eindopdracht.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.BL.Interfaces
{
    public interface IReservatieRepository
    {
        // CRUD
        void MaakReservatie(Reservatie reservatie);
        Reservatie GeefReservatieById(int reservatienummer);
        void PasReservatieAan(Reservatie reservatie);
        void AnnuleerReservatie(int reservatienummer);

        // ANDERE
        List<Reservatie> ZoekReservaties(int klantnummer, DateTime? begindatum);
        List<Reservatie> GeefOverzichtReservaties(string naam, DateTime begindatum, DateTime? einddatum);
        bool HeeftReservatie(Reservatie reservatie);
    }
}
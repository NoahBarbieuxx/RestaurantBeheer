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
        void MaakReservatie(Reservatie reservatie);
        void PasReservatieAan(Reservatie reservatie);
        void AnnuleerReservatie(Reservatie reservatie);
        List<Reservatie> ZoekReservaties(DateTime datum);
    }
}
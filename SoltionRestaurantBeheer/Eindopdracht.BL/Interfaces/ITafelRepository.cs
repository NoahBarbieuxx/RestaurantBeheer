using Eindopdracht.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.BL.Interfaces
{
    public interface ITafelRepository
    {
        // CRUD
        void MaakTafel(string naam, Tafel tafel);
        Tafel GeefTafelById(int tafelId);

        // ANDERE
        Tafel KiesTafel(string naam, int plaatsen);
    }
}
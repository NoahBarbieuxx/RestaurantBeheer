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
    public class TafelManager
    {
        private readonly ITafelRepository _tafelRepository;

        public TafelManager(ITafelRepository tafelRepository)
        {
            _tafelRepository = tafelRepository;
        }

        public virtual void MaakTafel(string naam, Tafel tafel)
        {
            try
            {
                _tafelRepository.MaakTafel(naam, tafel);
            }
            catch (Exception ex)
            {
                throw new TafelManagerException("MaakTafel", ex);
            }
        }

        public virtual Tafel GeefTafelById(int tafelId)
        {
            try
            {
                return _tafelRepository.GeefTafelById(tafelId);
            }
            catch (Exception ex)
            {
                throw new TafelManagerException("GeefTafelById", ex);
            }
        }

        public virtual Tafel KiesTafel(string naam, int plaatsen)
        {
            try
            {
                return _tafelRepository.KiesTafel(naam, plaatsen);
            }
            catch (Exception ex)
            {
                throw new TafelManagerException("KiesTafel", ex);
            }
        }
    }
}
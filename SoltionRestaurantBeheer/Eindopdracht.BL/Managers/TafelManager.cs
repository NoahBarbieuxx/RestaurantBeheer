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

        public void MaakTafel(string naam, Tafel tafel)
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

        public Tafel GeefTafelById(int tafelId)
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

        public List<Tafel> GeefTafelsByDatum(DateTime datum)
        {
            try
            {
                return _tafelRepository.GeefTafelsByDatum(datum);
            }
            catch (Exception ex)
            {
                throw new TafelManagerException("GeefTafelsByDatum", ex);
            }
        }

        public Tafel KiesTafel(int plaatsen)
        {
            try
            {
                return _tafelRepository.KiesTafel(plaatsen);
            }
            catch (Exception ex)
            {
                throw new TafelManagerException("KiesTafel", ex);
            }
        }
    }
}
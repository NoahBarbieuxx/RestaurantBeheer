using Eindopdracht.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.BL.Models
{
    public class Tafel
    {
        public Tafel(int tafelId, int plaatsen)
        {
            TafelId = tafelId;
            Plaatsen = plaatsen;
        }

        public Tafel(int plaatsen)
        {
            Plaatsen = plaatsen;
        }

        private int _tafelId;
        public int TafelId
        {
            get
            {
                return _tafelId;
            }
            set
            {
                if (value <= 0)
                {
                    throw new TafelException("TafelId is ongeldig!");
                }
                else
                {
                    _tafelId = value;
                }
            }
        }

        private int _plaatsen;
        public int Plaatsen
        {
            get
            {
                return _plaatsen;
            }
            set
            {
                if (value <= 0)
                {
                    throw new TafelException("PLaatsen is ongeldig!");
                }
                else 
                {
                    _plaatsen = value;
                }
            }
        }
    }
}
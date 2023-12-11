using Eindopdracht.BL.Interfaces;
using Eindopdracht.BL.Models;
using Eindopdracht.DL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            try
            {
                string sql = "INSERT INTO Reservaties(aantalPlaatsen, datum, uur, tafelnummer, klantnummer, restaurantNaam) OUTPUT INSERTED.reservatienummer VALUES(@aantalPlaatsen, @datum, @uur, @tafelnummer, @klantnummer, @restaurantNaam)";
                using(SqlConnection conn = new SqlConnection(_connectionString))
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        cmd.CommandText = sql;
                        cmd.Transaction = transaction;

                        cmd.Parameters.AddWithValue("@aantalPlaatsen", reservatie.AantalPlaatsen);
                        cmd.Parameters.AddWithValue("@datum", reservatie.Datum);
                        cmd.Parameters.AddWithValue("@uur", reservatie.Uur);
                        cmd.Parameters.AddWithValue("@tafelnummer", reservatie.Tafelnummer);
                        cmd.Parameters.AddWithValue("@klantnummer", reservatie.Contactpersoon.Klantnummer);
                        cmd.Parameters.AddWithValue("@restaurantNaam", reservatie.Restaurantinfo.Naam);

                        int reservatienummer = (int)cmd.ExecuteScalar();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ReservatieRepositoryException("MaakReservatie", ex);
            }
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
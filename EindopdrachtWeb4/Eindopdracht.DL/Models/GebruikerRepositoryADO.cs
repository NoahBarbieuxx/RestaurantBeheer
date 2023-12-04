using Eindopdracht.BL;
using Eindopdracht.BL.Interfaces;
using Eindopdracht.DL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.DL.Models
{
    public class GebruikerRepositoryADO : IGebruikerRepository
    {
        private readonly string _connectionString;

        public GebruikerRepositoryADO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void PasGebruikerAan(Gebruiker gebruiker)
        {
            try
            {
                string sql = "UPDATE Gebruikers SET naam=@naam, email=@email, telefoonnummer=@telefoonnummer, locatie=@locatie WHERE klantnummer=@klantnummer";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        cmd.CommandText = sql;
                        cmd.Transaction = transaction;

                        cmd.Parameters.AddWithValue("@klantnummer", gebruiker.Klantnummer);
                        cmd.Parameters.AddWithValue("@naam", gebruiker.Naam);
                        cmd.Parameters.AddWithValue("@email", gebruiker.Email);
                        cmd.Parameters.AddWithValue("@telefoonnummer", gebruiker.Telefoonnummer);
                        cmd.Parameters.AddWithValue("@locatie", gebruiker.Locatie.ToLocatieLijn());

                        cmd.Parameters["@klantnummer"].Value = gebruiker.Klantnummer;

                        cmd.ExecuteNonQuery();

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
                throw new GebruikerRepositoryException("PasGebruikersAan", ex);
            }
        }

        public void RegistreerGebruiker(Gebruiker gebruiker)
        {
            try
            {
                string sql = "INSERT INTO Gebruikers(naam, email, telefoonnummer, locatie) OUTPUT INSERTED.klantnummer VALUES(@naam, @email, @telefoonnummer, @locatie)";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        cmd.CommandText = sql;
                        cmd.Transaction = transaction;

                        cmd.Parameters.AddWithValue("@naam", gebruiker.Naam);
                        cmd.Parameters.AddWithValue("@email", gebruiker.Email);
                        cmd.Parameters.AddWithValue("@telefoonnummer", gebruiker.Telefoonnummer);
                        cmd.Parameters.AddWithValue("@locatie", gebruiker.Locatie.ToLocatieLijn());

                        int klantnummer = (int)cmd.ExecuteScalar();
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
                throw new GebruikerRepositoryException("RegistreerGebruiker", ex);
            }
        }

        public void SchrijfGebruikerUit(Gebruiker gebruiker)
        {
            throw new NotImplementedException();
        }

        public bool HeeftGebruiker(int klantnummer)
        {
            string sql = "SELECT COUNT(*) FROM Gebruikers WHERE klantnummer=@klantnummer";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;

                    cmd.Parameters.AddWithValue("@klantnummer", klantnummer);

                    int n = (int)cmd.ExecuteScalar();

                    if (n > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw new GebruikerRepositoryException("HeeftGebruiker", ex);
                }
            }
        }
    }
}
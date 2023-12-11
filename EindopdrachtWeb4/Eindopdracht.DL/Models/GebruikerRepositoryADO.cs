using Eindopdracht.BL;
using Eindopdracht.BL.Interfaces;
using Eindopdracht.BL.Models;
using Eindopdracht.DL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
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
                string sql = "UPDATE Gebruikers SET naam=@naam, email=@email, telefoonnummer=@telefoonnummer, locatie=@locatie, actief=@actief WHERE klantnummer=@klantnummer";
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
                        cmd.Parameters.AddWithValue("@actief", gebruiker.Actief);

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
                string sql = "INSERT INTO Gebruikers(naam, email, telefoonnummer, locatie, actief) OUTPUT INSERTED.klantnummer VALUES(@naam, @email, @telefoonnummer, @locatie, @actief)";
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
                        cmd.Parameters.AddWithValue("@actief", gebruiker.Actief);

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
            try
            {
                string sql = "UPDATE Gebruikers SET actief=@actief WHERE klantnummer=@klantnummer";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        cmd.CommandText = sql;

                        cmd.Parameters.AddWithValue("@klantnummer", gebruiker.Klantnummer);
                        cmd.Parameters.AddWithValue("@actief", 0);

                        transaction.Commit();

                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new GebruikerRepositoryException("SchrijfGebruikerUit", ex);
            }
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

        public Gebruiker GeefGebruikerById(int klantnummer)
        {
            try
            {
                string sql = "SELECT * FROM Gebruikers WHERE klantnummer=@klantnummer";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;

                    cmd.Parameters.AddWithValue("@klantnummer", klantnummer);

                    IDataReader reader = cmd.ExecuteReader();
                    reader.Read();

                    string locatielijn = (string)reader["locatie"];

                    Gebruiker gebruiker = new Gebruiker(
                        (int)reader["klantnummer"],
                        (string)reader["naam"],
                        (string)reader["email"],
                        (string)reader["telefoonnummer"],
                        new Locatie(locatielijn),
                        (int)reader["actief"]);

                    reader.Close();

                    return gebruiker;
                }
            }
            catch (Exception ex)
            {
                throw new GebruikerRepositoryException("GeefGebruikerById", ex);
            }
        }
    }
}
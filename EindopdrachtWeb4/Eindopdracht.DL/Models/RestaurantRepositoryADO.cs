using Eindopdracht.BL;
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
    public class RestaurantRepositoryADO : IRestaurantRepository
    {
        private readonly string _connectionString;

        public RestaurantRepositoryADO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Restaurant> ZoekRestaurants(string postcode, string keuken)
        {
            try
            {
                string sql = "SELECT * FROM Restaurants WHERE (@postcode IS NULL OR LEFT(locatie, 4) = LEFT(@postcode, 4)) AND (@keuken IS NULL OR keuken = @keuken)";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;

                    cmd.Parameters.AddWithValue("@postcode", string.IsNullOrEmpty(postcode) ? (object)DBNull.Value : postcode);
                    cmd.Parameters.AddWithValue("@keuken", string.IsNullOrEmpty(keuken) ? (object)DBNull.Value : keuken);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Restaurant> restaurants = new List<Restaurant>();

                        while (reader.Read())
                        {
                            Restaurant restaurant = new Restaurant(
                                naam: SafeGetString(reader, "naam"),
                                locatie: new Locatie
                                {
                                    Postcode = SafeGetString(reader, "postcode"),
                                    Gemeentenaam = SafeGetString(reader, "gemeentenaam"),
                                    Straatnaam = SafeGetString(reader, "straatnaam"),
                                    Huisnummer = SafeGetString(reader, "huisnummer")
                                },
                                keuken: SafeGetString(reader, "keuken"),
                                contactgegevens: new Contactgegevens(
                                    telefoonnummer: SafeGetString(reader, "telefoonnummer"),
                                    email: SafeGetString(reader, "email")
                                )
                            );

                            restaurants.Add(restaurant);
                        }

                        return restaurants;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception details. You can use your preferred logging mechanism.
                Console.WriteLine($"An error occurred in ZoekRestaurants: {ex.Message}");
                throw; // Re-throw the exception for higher-level handling.
            }
        }

        private string SafeGetString(SqlDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return !reader.IsDBNull(columnIndex) ? reader.GetString(columnIndex) : null;
        }

        public List<Restaurant> ZoekVrijeRestaurants(DateTime datum, int aantalPlaatsen)
        {
            throw new NotImplementedException();
        }
    }
}
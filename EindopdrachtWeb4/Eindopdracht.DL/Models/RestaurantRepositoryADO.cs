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
            string sql = "SELECT * FROM Restaurants WHERE (@postcode IS NULL OR LEFT(postcode, 4) = LEFT(@postcode, 4)) AND (@keuken IS NULL OR keuken = @keuken)";

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
                            naam: Convert.ToString(reader["Naam"]!),
                            locatie: new Locatie
                            {
                                Postcode = Convert.ToString(reader["Postcode"]!),
                                Gemeentenaam = Convert.ToString(reader["Gemeentenaam"]!),
                                Straatnaam = Convert.ToString(reader["Straatnaam"]!),
                                Huisnummer = Convert.ToString(reader["Huisnummer"]!)
                            },
                            keuken: Convert.ToString(reader["Keuken"]!),
                            contactgegevens: new Contactgegevens(
                                telefoonnummer: Convert.ToString(reader["telefoonnummer"]!),
                                email: Convert.ToString(reader["email"]!)
                            )
                        );

                        restaurants.Add(restaurant);
                    }

                    return restaurants;
                }
            }
        }

        public List<Restaurant> ZoekVrijeRestaurants(DateTime datum, int aantalPlaatsen)
        {
            throw new NotImplementedException();
        }
    }
}
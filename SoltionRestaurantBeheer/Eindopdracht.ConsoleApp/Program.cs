using Eindopdracht.BL.Interfaces;
using Eindopdracht.BL.Models;
using Eindopdracht.DL.Model;
using Eindopdracht.DL.Repositories;

namespace Eindopdracht.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=RestaurantBeheer;Integrated Security=True";
            RestaurantBeheerContext ctx = new RestaurantBeheerContext(connectionString);
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            //IRestaurantRepository restaurantRepository = new RestaurantRepository(connectionString);
            //List<Restaurant> restaurants = restaurantRepository.ZoekRestaurants("9000", "Pampas");
            //foreach (Restaurant restaurant in restaurants)
            //{
            //    Console.WriteLine(restaurant.Naam);
            //}

            //IGebruikerRepository gebruikerRepository = new GebruikerRepository(connectionString);
            //Locatie locatie = new Locatie("9790", "Wortegem-Petegem", "Kastanejplein", "39");
            //Gebruiker gebruiker = new Gebruiker("Noah", "barbieuxnoah@hotmail.com", "0491449667", locatie, 1);
            //gebruikerRepository.RegistreerGebruiker(gebruiker);
        }
    }
}
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

            IGebruikerRepository gebruikerRepository = new GebruikerRepository(connectionString);
            Locatie locatieGebruiker = new Locatie("9790", "Wortegem-Petegem", "Kastanejplein", "39");
            Gebruiker gebruiker = new Gebruiker("Noah", "barbieuxnoah@hotmail.com", "0491449667", locatieGebruiker, 1);
            gebruikerRepository.RegistreerGebruiker(gebruiker);

            IRestaurantRepository restaurantRepository = new RestaurantRepository(connectionString);
            Locatie locatieRestaurant = new Locatie("9000", "Gent", "Gentstraat", "82");
            Contactgegevens contactgegevens = new Contactgegevens("055603682", "bookings@pampas.com");
            Restaurant restaurant = new Restaurant("Pampas", locatieRestaurant, "Argentijns", contactgegevens);
            restaurantRepository.RegistreerRestaurant(restaurant);
            Console.WriteLine(restaurantRepository.GeefRestaurantByNaam("Pampas"));
        }
    }
}
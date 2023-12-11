using Eindopdracht.BL.Interfaces;
using Eindopdracht.BL.Managers;
using Eindopdracht.DL.Models;
using System.Security.Cryptography.X509Certificates;

namespace Eindopdracht.REST
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=EindopdrachtWeb4;Integrated Security=True";

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IGebruikerRepository>(r => new GebruikerRepositoryADO(connectionString));
            builder.Services.AddSingleton<GebruikerManager>();
            builder.Services.AddSingleton<IReservatieRepository>(r => new ReservatieRepositoryADO(connectionString));
            builder.Services.AddSingleton<ReservatieManager>();
            builder.Services.AddSingleton<IRestaurantRepository>(r => new RestaurantRepositoryADO(connectionString));
            builder.Services.AddSingleton<RestaurantManager>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
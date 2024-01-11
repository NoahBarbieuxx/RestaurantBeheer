using Eindopdracht.BL.Interfaces;
using Eindopdracht.BL.Managers;
using Eindopdracht.DL.Repositories;

string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=RestaurantBeheer;Integrated Security=True";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IRestaurantRepository>(r => new RestaurantRepository(connectionString));
builder.Services.AddSingleton<RestaurantManager>();
builder.Services.AddSingleton<IReservatieRepository>(r => new ReservatieRepository(connectionString));
builder.Services.AddSingleton<ReservatieManager>();
builder.Services.AddSingleton<ITafelRepository>(r => new TafelRepository(connectionString));
builder.Services.AddSingleton<TafelManager>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

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
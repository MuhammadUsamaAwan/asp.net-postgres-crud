using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("GameStore");

builder.Services.AddNpgsql<GameStoreContext>(connectionString);

var app = builder.Build();

app.MapGamesEndpoints();

app.MapGenresEndpoints();

app.Run();

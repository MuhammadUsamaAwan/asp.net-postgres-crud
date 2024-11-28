using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;

namespace GameStore.Api.Endpoints;

public static class GenreEndpoints
{
    const string GetGenreEndpointName = "GetGenre";

    public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("genres");

        group.MapGet("/", (GameStoreContext dbContext) =>
        {
            return dbContext.Genres.ToList();
        });

        group.MapGet("/{id}", (int id, GameStoreContext dbContext) =>
        {
            var genre = dbContext.Genres.Find(id);
            return genre is null ? Results.NotFound() : Results.Ok(genre);
        }).WithName(GetGenreEndpointName);

        group.MapPost("/", (GameStoreContext dbContext, CreateGenreDto newGenre) =>
       {
           Genre genre = new() { Id = 1, Name = newGenre.Name };
           dbContext.Add(genre);
           dbContext.SaveChanges();
           return Results.CreatedAtRoute(GetGenreEndpointName, new { Id = genre.Id }, genre);
       }).WithParameterValidation();

        group.MapPut("/{id}", (int id, GameStoreContext dbContext, CreateGenreDto updatedGenre) =>
        {
            var genre = dbContext.Genres.Find(id);
            if (genre == null)
            {
                return Results.NotFound();
            }
            genre.Name = updatedGenre.Name;
            dbContext.SaveChanges();
            return Results.NoContent();
        }).WithParameterValidation();

        group.MapDelete("/{id}", (int id, GameStoreContext dbContext) =>
        {
            var genre = dbContext.Genres.Find(id);
            if (genre == null)
            {
                return Results.NotFound();
            }
            dbContext.Remove(genre);
            dbContext.SaveChanges();
            return Results.NoContent();
        }).WithParameterValidation();


        return group;
    }
}

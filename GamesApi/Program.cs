using System.Text.Encodings.Web;
using GamesApi.Indexes;
using GamesApi.Models;
using Oakton;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Raven.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ApplyOaktonExtensions();
builder.Services.AddRavenDbDocStore();
builder.Services.AddRavenDbAsyncSession();

var app = builder.Build();

app.MapGet("/", () => new [] {
    "/games?search=%22Final%20Fight%22",
    "/stats/top-100-publishers",
    "/stats/platforms",
    "/stats/genres",
    "/stats/esrb"
});

app.MapGet("/games", async (string? search, IAsyncDocumentSession session) =>
    await session.Query<Games.Result, Games>()
         // handle missing search parameter
        .Search(g => g.Name, search ?? "*")
        .OrderByScoreDescending()
        .Take(10)
        .As<Game>()
        .ToListAsync());

app.MapGet("/stats/top-100-publishers",
    async (IAsyncDocumentSession session) => await session
        .Query<Games_ByPublisher.Result, Games_ByPublisher>()
        .As<Games_ByPublisher.Result>()
        .OrderByDescending(x => x.Total)
        .Take(100)
        .ToListAsync());

app.MapGet("/stats/platforms",
    async (IAsyncDocumentSession session) => await session
        .Query<Games_ByPlatform.Result, Games_ByPlatform>()
        .As<Games_ByPlatform.Result>()
        .OrderByDescending(x => x.Total)
        .ToListAsync());

app.MapGet("/stats/genres",
    async (IAsyncDocumentSession session) => await session
        .Query<Games_ByGenre.Result, Games_ByGenre>()
        .As<Games_ByGenre.Result>()
        .OrderBy(x => x.Genre)
        .ToListAsync());

app.MapGet("/stats/esrb",
    async (IAsyncDocumentSession session) => await session
        .Query<Games_ByEsrbRating.Result, Games_ByEsrbRating>()
        .As<Games_ByEsrbRating.Result>()
        .OrderByDescending(x => x.Total)
        .ToListAsync());

app.RunOaktonCommands(args);
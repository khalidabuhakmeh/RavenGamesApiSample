using System.Globalization;
using System.IO.Compression;
using CsvHelper;
using GamesApi.Models;
using Oakton;
using Raven.Client.Documents;
using Raven.Client.Documents.BulkInsert;
using Raven.Client.Documents.Operations;
using Raven.Client.Exceptions;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace GamesApi.Commands;

[Description("Load Game Data", Name = "load-games")]
public class Load : OaktonCommand<NetCoreInput>
{
    public override bool Execute(NetCoreInput input)
    {
        using var host = input.BuildHost();
        using var store = host.Services.GetRequiredService<IDocumentStore>();
        var logger = host.Services.GetRequiredService<ILogger<Load>>();

        using var reader = new StreamReader("./game_info.csv");
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        EnsureDatabaseExists(store);

        csv.Context.RegisterClassMap<GameMap>();


        var bulk = store.BulkInsert(new BulkInsertOptions
        {
            SkipOverwriteIfUnchanged = true,
            CompressionLevel = CompressionLevel.Fastest
        });
        bulk.OnProgress += (_, args) =>
        {
            logger.LogInformation("Batch count: {BatchCount}\nTotal: {Total}", 
                args.Progress.BatchCount,
                args.Progress.Total);
        };
        // yes, this is memory intensive
        var games = csv.GetRecords<Game>();
        logger.LogInformation("Loaded CSV");

        // async bulk insert fails 🤷
        foreach (var game in games)
        {
            // ReSharper disable once MethodHasAsyncOverload
            bulk.Store(game);
        }

        return true;
    }

    private void EnsureDatabaseExists(IDocumentStore store)
    {
        var database = store.Database;
        try
        {
            store.Maintenance.ForDatabase(database).Send(new GetStatisticsOperation());
        }
        catch (DatabaseDoesNotExistException)
        {
            try
            {
                store.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(database)));
            }
            catch (ConcurrencyException)
            {
                // The database was already created before calling CreateDatabaseOperation
            }
        }
    }
}
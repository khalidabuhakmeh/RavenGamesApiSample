using Oakton;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;

namespace GamesApi.Commands;

[Description("Create indexes", Name = "create-indexes")]
public class Index : OaktonCommand<NetCoreInput>
{
    public override bool Execute(NetCoreInput input)
    {
        var host = input.BuildHost();
        var store = host.Services.GetRequiredService<IDocumentStore>();
        var logger = host.Services.GetRequiredService<ILogger<Index>>();
        
        logger.LogInformation("Loading indexes...");
        
        IndexCreation.CreateIndexes(typeof(Index).Assembly, store);

        return true;
    }
}
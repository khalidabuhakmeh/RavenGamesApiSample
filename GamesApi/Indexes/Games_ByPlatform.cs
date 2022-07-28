using GamesApi.Models;
using Raven.Client.Documents.Indexes;

namespace GamesApi.Indexes;

public class Games_ByPlatform : AbstractIndexCreationTask<Game, Games_ByPlatform.Result>
{
    public class Result
    {
        public string Platform { get; set; }
        public long Total { get; set; }
    }

    public Games_ByPlatform()
    {
        Map = games =>
            from game in games
            from platform in game.Platforms
            select new Result { Platform = platform, Total = 1 };

        Reduce = platforms =>
            from platform in platforms
            group platform by platform.Platform
            into g
            select new Result
            {
                Platform = g.Key,
                Total = g.Sum(x => x.Total)
            };

        StoreAllFields(FieldStorage.Yes);
    }
}
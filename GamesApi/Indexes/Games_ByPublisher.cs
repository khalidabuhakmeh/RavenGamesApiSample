using GamesApi.Models;
using Raven.Client.Documents.Indexes;
using Raven.Client.Documents.Session.Tokens;

namespace GamesApi.Indexes;

public class Games_ByPublisher : AbstractIndexCreationTask<Game, Games_ByPublisher.Result>
{
    public class Result
    {
        public string Publisher { get; set; }
        public int Total { get; set; }
    }

    public Games_ByPublisher()
    {
        Map = games => 
            from game in games
            from publisher in game.Publishers
            select new Result { Publisher = publisher, Total = 1 };

        Reduce = results =>
            from result in results
            group result by result.Publisher
            into g
            select new Result { Publisher = g.Key, Total = g.Sum(x => x.Total) };
        
        Analyzers.Add(x => x.Publisher, "StandardAnalyzer");
    }
}
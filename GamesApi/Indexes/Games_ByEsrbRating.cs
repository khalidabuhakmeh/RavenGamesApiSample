using GamesApi.Models;
using Raven.Client.Documents.Indexes;

namespace GamesApi.Indexes;

public class Games_ByEsrbRating : AbstractIndexCreationTask<Game, Games_ByEsrbRating.Result>
{
    public class Result
    {
        public string EsrbRating { get; set; }
        public int Total { get; set; }
    }

    public Games_ByEsrbRating()
    {
        Map = games => 
            from game in games
            select new Result { EsrbRating = game.EsrbRating, Total = 1 };

        Reduce = results =>
            from result in results
            group result by result.EsrbRating
            into g
            select new Result { EsrbRating = g.Key, Total = g.Sum(x => x.Total) };
    }
}
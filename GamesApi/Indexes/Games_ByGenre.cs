using GamesApi.Models;
using Raven.Client.Documents.Indexes;

namespace GamesApi.Indexes;

public class Games_ByGenre : AbstractIndexCreationTask<Game, Games_ByGenre.Result>
{
    public class Result
    {
        public string Genre { get; set; }
        public int Total { get; set; }
    }

    public Games_ByGenre()
    {
        Map = games => 
            from game in games
            from genre in game.Genres
            select new Result { Genre = genre, Total = 1 };

        Reduce = results =>
            from result in results
            group result by result.Genre
            into g
            select new Result { Genre = g.Key, Total = g.Sum(x => x.Total) };
    }
}
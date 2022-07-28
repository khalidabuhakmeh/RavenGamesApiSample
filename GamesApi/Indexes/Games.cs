using GamesApi.Models;
using Raven.Client.Documents.Indexes;

namespace GamesApi.Indexes;

public class Games : AbstractIndexCreationTask<Game>
{
    public class Result
    {
        public string Name { get; set; }
        public DateTime? Released { get; set; }
        public List<string> Developers { get; set; }
        public List<string> Publishers { get; set; }
        public List<string> Genres { get; set; }
        public List<string> Platforms { get; set; }
        public bool IsReleased { get; set; }
    }

    public Games()
    {
        Map = games => from game in games
            select new Result
            {
                Name = game.Name,
                Released = game.Released,
                IsReleased = game.Released.HasValue,
                Developers = game.Developers,
                Publishers = game.Publishers,
                Genres = game.Genres,
                Platforms = game.Platforms
            };

        Analyzers.Add(x => x.Name, "StandardAnalyzer");
        Analyzers.Add(x => x.Developers, "StandardAnalyzer");
        Analyzers.Add(x => x.Publishers, "StandardAnalyzer");
    }
}
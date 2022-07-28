namespace GamesApi.Models;

public class Game
{
    public string Id { get; set; } = "";
    public string Slug { get; set; } = "";
    public string Name { get; set; }= "";
    public decimal? Metacritic { get; set; }
    public DateTime? Released { get; set; }
    public bool Tba { get; set; } = false;
    public DateTime? Updated { get; set; }
    public string? Website { get; set; }
    public decimal Rating { get; set; }
    public int RatingTop { get; set; }
    public decimal Playtime { get; set; }
    public int AchievementsCount { get; set; }
    public int RatingsCount { get; set; }
    public int SuggestionsCount { get; set; }
    public int GameSeriesCount { get; set; }
    public int ReviewsCount { get; set; }
    public List<string> Platforms { get; set; } = new();
    public List<string> Developers { get; set; } = new();
    public List<string> Genres { get; set; }= new();
    public List<string> Publishers { get; set; }= new();
    public string EsrbRating { get; set; } = "";
    public int AddedStatusYet { get; set; }
    public int AddedStatusOwned { get; set; }
    public int AddedStatusBeaten { get; set; }
    public int AddedStatusToPlay { get; set; }
    public int AddedStatusDropped { get; set; }
    public int AddedStatusPlaying { get; set; }
}
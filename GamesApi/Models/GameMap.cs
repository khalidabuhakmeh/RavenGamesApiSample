using CsvHelper;
using CsvHelper.Configuration;

namespace GamesApi.Models;

public class GameMap : ClassMap<Game>
{
    public GameMap()
    {
        Map(m => m.Id).Name("id");
        Map(m => m.Slug).Name("slug");
        Map(m => m.Name).Name("name");
        Map(m => m.Metacritic).Name("metacritic");
        Map(m => m.Released).Name("released");
        Map(m => m.Tba).Name("tba");
        Map(m => m.Updated).Name("updated");
        Map(m => m.Website).Name("website");
        Map(m => m.Rating).Name("rating");
        Map(m => m.RatingTop).Name("rating_top");
        Map(m => m.Playtime).Name("playtime");
        Map(m => m.AchievementsCount).Name("achievements_count");
        Map(m => m.RatingsCount).Name("ratings_count");
        Map(m => m.SuggestionsCount).Name("suggestions_count");
        Map(m => m.GameSeriesCount).Name("game_series_count");
        Map(m => m.ReviewsCount).Name("reviews_count");
        Map(m => m.Platforms).Name("platforms").Convert(o => SplitCollectionString(o, "platforms"));
        Map(m => m.Developers).Name("developers").Convert(o => SplitCollectionString(o, "developers"));
        Map(m => m.Genres).Name("genres").Convert(o => SplitCollectionString(o, "genres"));
        Map(m => m.Publishers).Name("publishers").Convert(o => SplitCollectionString(o, "publishers"));
        Map(m => m.EsrbRating).Name("esrb_rating");
        Map(m => m.AddedStatusYet).Name("added_status_yet");
        Map(m => m.AddedStatusOwned).Name("added_status_owned");
        Map(m => m.AddedStatusBeaten).Name("added_status_beaten");
        Map(m => m.AddedStatusToPlay).Name("added_status_toplay");
        Map(m => m.AddedStatusDropped).Name("added_status_dropped");
        Map(m => m.AddedStatusPlaying).Name("added_status_playing");
    }

    private List<string> SplitCollectionString(ConvertFromStringArgs args, string field)
    {
        var value = args.Row.GetField<string>(field);
        var options = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
        return value?.Split("||", options).ToList() ?? new List<string>();
    }
}
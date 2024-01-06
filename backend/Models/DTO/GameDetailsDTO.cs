using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace VGL_Project.Models.DTO
{
    public class GameDetails
    {
        public bool Success { get; set; }
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("short_description")]
        public string ShortDescription { get; set; }

        [JsonProperty("detailed_description")]
        public string DetailedDescription { get; set; }

        [JsonProperty("developers")]
        public List<string> Developers { get; set; }

        [JsonProperty("steam_appid")]
        public string AppId { get; set; }

        [JsonProperty("release_date")]
        public ReleaseDate ReleaseDate { get; set; }

        [JsonProperty("genres")]
        public Genre[] Genres { get; set; } 

        public string SanitizedDetailedDescription => RemoveHtmlTags(DetailedDescription);

        private static string RemoveHtmlTags(string html)
        {
            return Regex.Replace(html, "\t|<.*?>|\r\n\t+", " ");
        }
    }

    public class ReleaseDate
    {
        [JsonProperty("coming_soon")]
        public bool ComingSoon { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }

    public class Genre
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}

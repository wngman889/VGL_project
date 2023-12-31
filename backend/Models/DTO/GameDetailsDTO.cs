﻿using Newtonsoft.Json;
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

        [JsonProperty("is_free")]
        public bool IsFree { get; set; }

        [JsonProperty("release_date")]
        public ReleaseDate ReleaseDate { get; set; }

        [JsonProperty("genres")]
        public Genre[] Genres { get; set; }

        [JsonProperty("recommendations")]
        public Recommendations Recommendations { get; set; }

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

    public class Recommendations
    {
        [JsonProperty("total")]
        public int TotalRecommendations { get; set; }
    }

    public class Genre
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class GameDetailsResponseDTO
    {
        public string AppId { get; set; } = "";
        public string Name { get; set; } = "";
        public string Developer { get; set; } = "";
        public string Genre { get; set; } = "";
        public string ReleaseDate { get; set; } = "";
        public int? Recommendations { get; set; }
        public bool IsFree { get; set; } = false;
        public string Description { get; set; } = "";
    }
}

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace VGL_Project.Models.DTO
{
    public class NewsDTO
    {
        [Required]
        public required string SteamId { get; set; }

        [Required]
        public required int MaxCount { get; set; }
    }
    public class NewsEndpointResponse
    {
        public string AppId { get; set; }
        public string GameName { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        private string _contents;

        [JsonProperty("contents")]
        public string Contents
        {
            get => _contents;
            set => _contents = RemoveNonTextContent(value);
        }

        private static string RemoveNonTextContent(string input)
        {
            // Remove HTML tags, images, and other non-text content
            string pattern = @"\{[^}]*\}|\[(\/?[^\]]+)\]|\[img\]|\[\/img\]|<[^>]+>|https?://\S+|\n";
            string cleanedInput = Regex.Replace(input, pattern, string.Empty);

            return cleanedInput.Length <= 500 ? cleanedInput : cleanedInput[..500];
        }

    }
}

using System.Text.Json.Serialization;

namespace OPP_back.Models.Dto
{
    public class TokensDto
    {
        [JsonPropertyName("AccessToken")]
        public string Access { get; set; }
        [JsonPropertyName("RefreshToken")]
        public string Refresh { get; set; }
    }
}

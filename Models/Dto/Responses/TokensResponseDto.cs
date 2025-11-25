using System.Text.Json.Serialization;

namespace OPP_back.Models.Dto.Responses
{
    public class TokensResponseDto
    {
        [JsonPropertyName("AccessToken")]
        public string Access { get; set; }
        [JsonPropertyName("RefreshToken")]
        public string Refresh { get; set; }
    }
}

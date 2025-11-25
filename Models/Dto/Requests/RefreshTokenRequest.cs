using System.Text.Json.Serialization;

namespace OPP_back.Models.Dto.Requests
{
    public class RefreshTokenRequest
    {
        [JsonPropertyName("RefreshToken")]
        public string Token { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace OPP_back.Models.Requests
{
    public class RefreshTokenRequest
    {
        [JsonPropertyName("RefreshToken")]
        public string Token { get; set; }
    }
}

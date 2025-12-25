using OPP_back.Models.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OPP_back.Models.Dto
{
    public class UserDto
    {
        [JsonPropertyName("Id")]
        public Guid Id { get; set; }
        [JsonPropertyName("Subjects")]
        [MinLength(0)]
        public List<SubjectDto> Subjects { get; set; }
        [JsonPropertyName("Teams")]
        [MinLength(0)]
        public List<TeamDto> Teams { get; set; }
    }
}

using OPP_back.Models.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OPP_back.Models.Dto
{
    public class TeamDto
    {
        [JsonPropertyName("Id")]
        public Guid Id { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Subjects")]
        [MinLength(0)]
        public List<Guid> Subjects { get; set; }
        [JsonPropertyName("Members")]
        [MinLength(0)]
        public List<MemberDto> Members { get; set; }
    }
}

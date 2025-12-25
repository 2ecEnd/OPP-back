using OPP_back.Models.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OPP_back.Models.Dto
{
    public class MemberDto
    {
        [JsonPropertyName("Id")]
        public Guid Id { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("Surname")]
        public string Surname { get; set; }
        [JsonPropertyName("Email")]
        public string? Email { get; set; }
        [JsonPropertyName("Specialization")]
        public string? Specialization { get; set; }

        [JsonPropertyName("AssignedTasks")]
        [MinLength(0)]
        public List<Guid>? AssignedTasks { get; set; }
    }
}

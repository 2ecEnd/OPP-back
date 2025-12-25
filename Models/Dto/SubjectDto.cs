using OPP_back.Models.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OPP_back.Models.Dto
{
    public class SubjectDto
    {
        [JsonPropertyName("Id")]
        public Guid Id { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("TeamId")]
        public Guid? TeamId { get; set; }
        [JsonPropertyName("Tasks")]
        [MinLength(0)]
        public List<TaskDto> Tasks { get; set; }
    }
}

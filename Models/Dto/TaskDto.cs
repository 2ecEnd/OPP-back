using OPP_back.Models.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OPP_back.Models.Dto
{
    public class TaskDto
    {
        [JsonPropertyName("Id")]
        public Guid Id { get; set; }
        [JsonPropertyName("Title")]
        public string Title { get; set; }
        [JsonPropertyName("Description")]
        public string Description { get; set; }
        [JsonPropertyName("CreateTime")]
        public DateTime CreateTime { get; set; }
        [JsonPropertyName("DeadLine")]
        public DateTime? DeadLine { get; set; }
        [JsonPropertyName("LeadTime")]
        public DateTime? LeadTime { get; set; }
        [JsonPropertyName("Status")]
        public string Status { get; set; }

        [JsonPropertyName("PosX")]
        public double PosX { get; set; }
        [JsonPropertyName("PosY")]
        public double PosY { get; set; }

        [JsonPropertyName("SubTasks")]
        [MinLength(0)]
        public List<Guid> SubTasks { get; set; } = new List<Guid>();
        [JsonPropertyName("AssignedTasks")]
        [MinLength(0)]
        public List<Guid> AssignedTasks { get; set; } = new List<Guid>();
    }
}

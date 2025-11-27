using OPP_back.Models.Data;

namespace OPP_back.Models.Dto
{
    public class SubjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid? TeamId { get; set; }
        public List<TaskDto> Tasks { get; set; }
    }
}

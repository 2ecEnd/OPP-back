using OPP_back.Models.Data;

namespace OPP_back.Models.Dto
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime DeadLine { get; set; }
        public DateTime LeadTime { get; set; }
        public string Status { get; set; }

        public double PosX { get; set; }
        public double PosY { get; set; }

        public Guid? SuperTaskId { get; set; }
        public List<Guid> SubTasks { get; set; }
        public List<Guid> AssignedTasks { get; set; }
    }
}

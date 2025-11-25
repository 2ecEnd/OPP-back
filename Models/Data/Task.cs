namespace OPP_back.Models.Data
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime DeadLine { get; set; }
        public DateTime LeadTime { get; set; }
        public Status Status { get; set; }

        public double PosX { get; set; }
        public double PosY { get; set; }

        public Guid? SuperTaskId { get; set; }
        public Task? SuperTask { get; set; }
        public List<Task> SubTasks { get; set; }
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }
        public List<AssignedTask> AssignedTasks { get; set; }
    }
}

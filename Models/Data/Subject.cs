namespace OPP_back.Models.Data
{
    public class Subject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public List<Task> Tasks { get; set; }
    }
}

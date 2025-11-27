namespace OPP_back.Models.Data
{
    public class Member
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get ; set; }
        public string? Email { get; set; } 
        public string? Specialization { get; set; }

        public Guid TeamId { get; set; }
        public Team Team{ get; set; }
        public List<AssignedTask> AssignedTasks { get; set; }
    }
}

namespace OPP_back.Models.Data
{
    public class Member
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get ; set; }
        public string? Email { get; set; } // Точно ли он nullable?
        public string? Specialization { get; set; } // А тут мб список строк, если у чел многозадачный?

        public Guid UserId { get; set; }
        public User User { get; set; }
        public List<AssignedTask> AssignedTasks { get; set; }
    }
}

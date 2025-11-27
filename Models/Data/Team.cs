namespace OPP_back.Models.Data
{
    public class Team
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Member> Members { get; set; }
    }
}

namespace OPP_back.Models.Data
{
    public class Teamlead
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public List<Subject> Subjects { get; set; }
        public List<Member> Members { get; set; }
    }
}

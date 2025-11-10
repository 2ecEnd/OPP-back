namespace OPP_back.Models.Data
{
    public class User
    {
        public Guid Id { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Member> Members { get; set; }
    }
}

using OPP_back.Models.Data;

namespace OPP_back.Models.Dto
{
    public class MemberDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Email { get; set; }
        public string? Specialization { get; set; }

        public List<Guid> AssignedTasks { get; set; }
    }
}

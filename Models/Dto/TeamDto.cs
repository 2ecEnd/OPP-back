using OPP_back.Models.Data;

namespace OPP_back.Models.Dto
{
    public class TeamDto
    {
        public Guid Id { get; set; }

        public List<Guid> Subjects { get; set; }
        public List<MemberDto> Members { get; set; }
    }
}

using OPP_back.Models.Data;

namespace OPP_back.Models.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public List<SubjectDto> Subjects { get; set; }
        public List<TeamDto> Teams { get; set; }
    }
}

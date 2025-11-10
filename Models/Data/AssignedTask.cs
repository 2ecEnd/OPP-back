namespace OPP_back.Models.Data
{
    public class AssignedTask
    {
        public Guid MemberId { get; set; }
        public Member Member { get; set; }
        public Guid TaskId { get; set; }
        public Task Task { get; set; }
    }
}

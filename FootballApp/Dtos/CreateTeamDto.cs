namespace FootballApp.Dtos
{
    public class CreateTeamDto
    {
        public string Name { get; set; }
        public List<int> MemberIds { get; set; }
    }
}

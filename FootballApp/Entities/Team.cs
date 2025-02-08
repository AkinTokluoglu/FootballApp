namespace FootballApp.Entities
{
    public class Team
    {
        public int Id { get; set; } 
        public string Name { get; set; } 
        public int CaptainId { get; set; } 
        public User Captain { get; set; } 
        public List<User> Members { get; set; } 
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public List<UserTeam> UserTeams { get; set; }
    }

}

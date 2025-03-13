namespace FootballApp.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Contact { get; set; }
        public string? Position { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string Role { get; set; } // "Captain" or "Player"
        public DateTime DateCreated { get; set; }

        public List<UserTeam> UserTeams { get; set; }
    }

}

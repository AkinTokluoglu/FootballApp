namespace FootballApp.Dtos
{
    public class UserRegisterDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string Contact { get; set; }
        public string PositionsPlayed { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

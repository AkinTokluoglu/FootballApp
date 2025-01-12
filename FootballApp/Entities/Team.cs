namespace FootballApp.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CaptainId { get; set; }
        public List<int> PlayerIds { get; set; } // List of User IDs
        public DateTime DateCreated { get; set; }
    }

}

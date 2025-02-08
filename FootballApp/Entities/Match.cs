namespace FootballApp.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int EnemyId { get; set; }
        public DateTime MatchDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Location { get; set; }
        public int TeamScore { get; set; }
        public int EnemyScore { get; set; }
        public bool IsCompleted { get; set; }
    }
}

namespace FootballApp.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public int TeamAId { get; set; }
        public int TeamBId { get; set; }
        public DateTime MatchDate { get; set; }
        public string Location { get; set; }
        public int TeamAScore { get; set; }
        public int TeamBScore { get; set; }
        public bool IsCompleted { get; set; }
    }
}

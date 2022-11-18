namespace LoopRareAPI.Models
{
    public class Rankings
    {
        public string? name { get; set; }
        public List<Ranking> rankings { get; set; } = new List<Ranking>();
    }
    public class Ranking
    {
        public int Id { get; set; }
        public int Rank { get; set; }
        public string? Tier { get; set; }
        public int Score { get; set; }

    }
}

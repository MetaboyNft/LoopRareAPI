namespace LoopRareAPI.Models
{
    public class Rankings
    {
        public string? collectionid { get; set; }
        public string? name { get; set; }
        public int total { get; set; }
        public List<Ranking> rankings { get; set; } = new List<Ranking>();

    }
    public class Ranking
    {
        public int Id { get; set; }
        public int Rank { get; set; }
        public string? Tier { get; set; }
        public decimal Score { get; set; }

    }
}

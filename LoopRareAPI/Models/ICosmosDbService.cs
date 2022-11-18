namespace LoopRareAPI.Models
{
    public interface ICosmosDbService
    {
        Task<Rankings?> GetRankingsAsync(string collectionId);
    }
}

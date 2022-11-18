namespace LoopRareAPI.Models
{
    public interface ICosmosDbService
    {
        Task<Rankings?> GetCollectionOverallRankingsAsync(string collectionId);
        Task<NftMetadata?> GetCollectionSingleRankingsAsync(string collectionId, int nftNumber);
    }
}

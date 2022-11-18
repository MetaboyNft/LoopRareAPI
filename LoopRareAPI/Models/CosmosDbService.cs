using Microsoft.Azure.Cosmos;

namespace LoopRareAPI.Models
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;
        public CosmosDbService(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }
        public async Task<Rankings?> GetCollectionOverallRankingsAsync(string collectionId)
        {
            try
            {
                Rankings? rankings = null;
                var query = new QueryDefinition(query: "SELECT * FROM data c WHERE c.name = @name and c.collectionid = @collectionid")
                    .WithParameter("@name", "ranks")
                    .WithParameter("@collectionid", collectionId);
                using (FeedIterator<Rankings> iterator = _container.GetItemQueryIterator<Rankings>(query))
                {
                    while (iterator.HasMoreResults)
                    {
                        FeedResponse<Rankings> response = await iterator.ReadNextAsync();
                        foreach (var item in response)
                        {
                            rankings = item;
                        }
                    }
                }
                return rankings;
            }
            catch (CosmosException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<NftMetadata?> GetCollectionSingleRankingsAsync(string collectionId, int nftNumber)
        {
            try
            {
                NftMetadata? nftMetadata = null;
                // Can write raw SQL, but the iteration is a little annoying. 
                var query = new QueryDefinition(query: "SELECT * FROM data c WHERE c.nftNumber = @nftNumber and c.collectionid = @collectionid")
                .WithParameter("@nftNumber", nftNumber)
                .WithParameter("@collectionid", collectionId);
                using (FeedIterator<NftMetadata> iterator = _container.GetItemQueryIterator<NftMetadata>(query))
                {
                    while (iterator.HasMoreResults)
                    {
                        FeedResponse<NftMetadata> response = await iterator.ReadNextAsync();
                        foreach (var item in response)
                        {
                            nftMetadata = item;
                        }
                    }
                }
                return nftMetadata;
            }
            catch (CosmosException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}

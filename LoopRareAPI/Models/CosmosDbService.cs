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
        public async Task<Rankings?> GetRankingsAsync(string collectionId)
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
                            Console.WriteLine($"Found item:\t{item.name}");
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
    }
}

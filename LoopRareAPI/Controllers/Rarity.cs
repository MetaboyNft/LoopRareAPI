using LoopRareAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace LoopRareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Rarity : ControllerBase
    {
        readonly CosmosClient _client;

        private Container _container;

        private IConfiguration? _config;

        public Rarity(IConfiguration config)
        {
            _config = config;
            _client = new CosmosClient(
               accountEndpoint: _config.GetValue<string>("CosmosDbEndpoint"),
               authKeyOrResourceToken: _config.GetValue<string>("CosmosDbAuthKey"),
               clientOptions: new CosmosClientOptions() { ApplicationName = _config.GetValue<string>("CosmosDbApplicationName") });
            _container = _client.GetContainer(databaseId: _config.GetValue<string>("CosmosDbDatabaseId"), 
                                              containerId: _config.GetValue<string>("CosmosDbContainerId"));
        }

        [HttpGet]
        [Route("rankings")]
        public async Task<Rankings> GetRankings(string collectionName)
        {
            try
            {
                Rankings rankings = null;
                var iterator = _container.GetItemQueryIterator<Rankings>($"SELECT * FROM c WHERE c.name = '{collectionName}-Ranks'");
                while (iterator.HasMoreResults)
                {
                    foreach (var item in (await iterator.ReadNextAsync()).Resource)
                    {
                        rankings = item;
                    }
                }
                return rankings;
            }
            catch (CosmosException ex)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }


    }
}

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
            _client = new CosmosClient(
               accountEndpoint: _config.GetValue<string>("CosmosDbEndpoint"),
               authKeyOrResourceToken: _config.GetValue<string>("CosmosDbAuthKey"),
               clientOptions: new CosmosClientOptions() { ApplicationName = _config.GetValue<string>("CosmosDbApplicationName") });
            _container = _client.GetContainer(databaseId: _config.GetValue<string>("CosmosDbDatabaseId"), 
                                              containerId: _config.GetValue<string>("CosmosDbContainerId"));
        }


    }
}

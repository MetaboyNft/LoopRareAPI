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
        private readonly ICosmosDbService _cosmosDbService;


        public Rarity(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(cosmosDbService));
        }

        [HttpGet]
        [Route("rankings")]
        public async Task<IActionResult> GetRankingsAsync(string collectionId)
        {
            return Ok(await _cosmosDbService.GetRankingsAsync(collectionId));
        }


    }
}

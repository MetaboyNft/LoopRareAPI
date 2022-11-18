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
        public async Task<IActionResult> GetRankingsOverallAsync(string collectionId)
        {
            return Ok(await _cosmosDbService.GetCollectionOverallRankingsAsync(collectionId));
        }


        [HttpGet]
        [Route("rankingsSingle")]
        public async Task<IActionResult> GetRankingsSingleAsync(string collectionId, int nftNumber)
        {
            return Ok(await _cosmosDbService.GetCollectionSingleRankingsAsync(collectionId, nftNumber));
        }
    }
}

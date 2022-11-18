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
            var result = await _cosmosDbService.GetCollectionOverallRankingsAsync(collectionId);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Something went wrong!");
            }

        }


        [HttpGet]
        [Route("rankingsSingle")]
        public async Task<IActionResult> GetRankingsSingleAsync(string collectionId, int nftNumber)
        {
            var result = await _cosmosDbService.GetCollectionSingleRankingsAsync(collectionId, nftNumber);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Something went wrong!");
            }
        }
    }
}

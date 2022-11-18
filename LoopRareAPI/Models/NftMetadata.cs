using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopRareAPI.Models
{

    public class NftMetadata
    {
        public string? collectionid { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }

        public string? animation_url { get; set; }
        public string? image { get; set; }

        public List<Trait>? attributes { get; set; }

        public int nftNumber { get; set; }

        public int Rank { get; set; }

        public string? Tier { get; set; }
        public decimal Score { get; set; }

        public string? id { get; set; }

    }

    public class Trait
    {
        public string? trait_type { get; set; }
        public string? value { get; set; } //value can be either string or int

        public int count { get; set; }

        public decimal score { get; set; }
    }

}

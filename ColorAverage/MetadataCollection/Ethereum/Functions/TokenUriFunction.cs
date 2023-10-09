using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Util;
using System.Numerics;

namespace ColorAverage.MetadataCollection.Ethereum.Functions
{
    [Function("tokenURI", "string")]
    public class TokenUriFunction : FunctionMessage
    {
        [Parameter("uint256", "tokenId")]
        public BigInteger TokenId { get; set; }
    }
}

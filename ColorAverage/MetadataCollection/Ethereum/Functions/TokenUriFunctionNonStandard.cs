using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ColorAverage.MetadataCollection.Ethereum.Functions
{
    [Function("uri", "string")]
    public class TokenUriFunctionNonStandard : FunctionMessage
    {
        [Parameter("uint256", "tokenId")]
        public BigInteger TokenId { get; set; }
    }
}

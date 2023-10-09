using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using Nethereum.Web3;
using Nethereum.Accounts;
using Nethereum.Web3.Accounts;
using ColorAverage.MetadataCollection.Ethereum.Functions;
using Nethereum.Contracts.ContractHandlers;
using System.Text.Json;
using Nethereum.JsonRpc.Client;
using System.Text;
using System.Data;

namespace ColorAverage.MetadataCollection.Ethereum
{
    public class EthereumMetadataService
    {
        private readonly string InfuraApiKey;

        private const string INFURA_URL = "https://mainnet.infura.io/v3/{0}";

        public EthereumMetadataService(string infuraApiKey) 
        {
            this.InfuraApiKey = infuraApiKey;
        }

        public async Task<string> GetNftMetadataUrl(string contractAddress, int tokenId)
        {
            TokenUriFunction tokenUriFunction = new TokenUriFunction()
            {
                TokenId = tokenId
            };

            IContractQueryHandler<TokenUriFunction> contractQueryHandler = new Web3(url: string.Format(INFURA_URL, this.InfuraApiKey))
                .Eth
                .GetContractQueryHandler<TokenUriFunction>();

            string? metadataUrl;

            try
            {
                metadataUrl = await contractQueryHandler.QueryAsync<string>(contractAddress, functionMessage: tokenUriFunction);
            }
            catch (RpcResponseException)
            {
                //could be that the function is not called tokenURI so brute forcing the exception
                TokenUriFunctionNonStandard tokenUriFunctionNonStandard = new TokenUriFunctionNonStandard()
                {
                    TokenId = tokenId
                };

                IContractQueryHandler<TokenUriFunctionNonStandard> contractQueryHandlerNonStandard = new Web3(url: string.Format(INFURA_URL, this.InfuraApiKey))
                    .Eth
                    .GetContractQueryHandler<TokenUriFunctionNonStandard>();

                metadataUrl = await contractQueryHandlerNonStandard.QueryAsync<string>(contractAddress, functionMessage: tokenUriFunctionNonStandard);
            }

            return metadataUrl;
        }

        public async Task<string> GetNftMetadataString(string contractAddress, int tokenId)
        {
            string metadataUrl = await GetNftMetadataUrl(contractAddress, tokenId);

            if (IsUrlIpfs(metadataUrl)) return await new HttpClient().GetStringAsync(metadataUrl.Replace("ipfs://", "https://ipfs.io/ipfs/"));
            else return await new HttpClient().GetStringAsync(metadataUrl);
        }

        public async Task<string?> GetNftPictureUrl(string contractAddress, int tokenId)
        {
            string? metadataString = await GetNftMetadataString(contractAddress, tokenId);
            return JsonDocument.Parse(metadataString)
                .RootElement
                .GetProperty("image")
                .GetString();
        }

        public async Task<byte[]> GetNftPicture(string contractAddress, int tokenId)
        {
            string? pictureUrl = await GetNftPictureUrl(contractAddress, tokenId);

            if (pictureUrl is null) throw new NoNullAllowedException(nameof(pictureUrl));

            if (IsUrlIpfs(pictureUrl)) return await new HttpClient().GetByteArrayAsync(pictureUrl.Replace("ipfs://", "https://ipfs.io/ipfs/"));
            else return await new HttpClient().GetByteArrayAsync(pictureUrl);
        }

        private static bool IsUrlIpfs(string url) => url.StartsWith("ipfs://");
    }
}

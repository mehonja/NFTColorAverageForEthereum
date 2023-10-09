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
    /// <summary>
    /// Metadata service for seving NFTs
    /// </summary>
    public class EthereumMetadataService
    {
        private readonly string InfuraApiKey;

        private const string INFURA_URL = "https://mainnet.infura.io/v3/{0}";

        public EthereumMetadataService(string infuraApiKey) 
        {
            this.InfuraApiKey = infuraApiKey;
        }

        /// <summary>
        /// Gets the NFT Metadata URL, can be IPFS.
        /// </summary>
        /// <param name="contractAddress">The address of the NFT contract.</param>
        /// <param name="tokenId">The token ID of the NFT.</param>
        /// <returns>The URL of the NFT metadata.</returns>
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

        /// <summary>
        /// Gets the NFT Metadata as a string.
        /// </summary>
        /// <param name="contractAddress">The address of the NFT contract.</param>
        /// <param name="tokenId">The token ID of the NFT.</param>
        /// <returns>The metadata of the NFT as a string in a JSON format.</returns>
        public async Task<string> GetNftMetadataString(string contractAddress, int tokenId)
        {
            string metadataUrl = await GetNftMetadataUrl(contractAddress, tokenId);

            if (IsUrlIpfs(metadataUrl)) return await new HttpClient().GetStringAsync(metadataUrl.Replace("ipfs://", "https://ipfs.io/ipfs/"));
            else return await new HttpClient().GetStringAsync(metadataUrl);
        }

        /// <summary>
        /// Gets the NFT picture URL.
        /// </summary>
        /// <param name="contractAddress">The address of the NFT contract.</param>
        /// <param name="tokenId">The token ID of the NFT.</param>
        /// <returns>The URL of the NFT picture from the metadata. Can be IPFS URL.</returns>
        public async Task<string?> GetNftPictureUrl(string contractAddress, int tokenId)
        {
            string? metadataString = await GetNftMetadataString(contractAddress, tokenId);
            return JsonDocument.Parse(metadataString)
                .RootElement
                .GetProperty("image")
                .GetString();
        }

        /// <summary>
        /// Gets the picture of the NFT from the URL in the metadata.
        /// </summary>
        /// <param name="contractAddress">The address of the NFT contract.</param>
        /// <param name="tokenId">The token ID of the NFT.</param>
        /// <returns>The picture from the NFT as a byte array.</returns>
        /// <exception cref="NoNullAllowedException">The picture URL cannot be null.</exception>
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

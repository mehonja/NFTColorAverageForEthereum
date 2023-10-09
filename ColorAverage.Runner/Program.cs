// See https://aka.ms/new-console-template for more information
using ColorAverage.MetadataCollection.Ethereum;

EthereumMetadataService ethereumMetadataService = new EthereumMetadataService("d1198a4bad5145abaa47a7a58f91eec6");

await ethereumMetadataService.GetPictureFromNft("0x0c56f29b8d90eea71d57cadeb3216b4ef7494abc", 19637);

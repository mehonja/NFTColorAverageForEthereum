# Ethereum NFT Color Averager (C#)

[![.build](https://github.com/mehonja/NFTColorAverageForEthereum/actions/workflows/dotnet.yml/badge.svg)](https://github.com/mehonja/NFTColorAverageForEthereum/actions/workflows/dotnet.yml) [![codecov](https://codecov.io/gh/mehonja/NFTColorAverageForEthereum/graph/badge.svg?token=K1PM6O1FQ8)](https://codecov.io/gh/mehonja/NFTColorAverageForEthereum)

## Overview

This C# program is designed to retrieve Ethereum-based Non-Fungible Tokens (NFTs) and calculate the average color of the images associated with these NFTs. It leverages the Ethereum blockchain and external image processing libraries to accomplish this task.
The library is using [Image Sharp](https://github.com/SixLabors/ImageSharp) for image analysis and [Nethereum](https://github.com/Nethereum/Nethereum) for Blockchain interaction via [Infura](https://www.infura.io).

IMPORTANT: An Infura API key is required. Infuras free API tier allows for 100,000 free requests a day.

## Features

- Connects to the Ethereum blockchain to fetch NFTs using the Ethereum NFT standard (e.g., ERC-721 or ERC-1155).
- Downloads images associated with each NFT.
- Calculates the average color of the images.
- Provides options for specifying the NFT contract address, token IDs, and output format.

## Requirements

Before running the program, ensure you have the following dependencies and prerequisites:

- [.NET Core](https://dotnet.microsoft.com/download) installed on your system.
- Access to an Ethereum node or a service like Infura to interact with the Ethereum blockchain.
- Any additional libraries or packages specified in the project's `csproj` file.

## Usage

1. Instantiate the EthereumMetadataService

   ```cs
   EthereumMetadataService ethereumMetadataService = new EthereumMetadataService("your infura API key");

2. Get NFT Image

   ```cs
   //the method takes the contract address and the token id as the parameter
   byte[] picture = ethereumMetadataService.GetNftPicture("contract address", 1);

3. Optionally you also have the options to get the NFT picture url, metadata url or metadata

   ```cs
   //the method takes the contract address and the token id as the parameter
   string nftPictureUrl = ethereumMetadataService.GetNftPictureUrl("contract address", 1);

   //the method takes the contract address and the token id as the parameter
   string nftMetadataUrl = ethereumMetadataService.GetNftMetadataUrl("contract address", 1);

   //the method takes the contract address and the token id as the parameter
   string metadata = ethereumMetadataService.GetNftMetadataString("contract address", 1);
   ```
4. Instantiate the PixelAverageService

   ```cs
   PixelAverageService pixelAverageService = new PixelAverageService();

5. Call the GetTopPixelColor method

   ```cs
   //the file is a byte[], the second argument is the amount of colors that is returned, the method returns the RGBA
   string[] topColors = pixelAverageService.GetTopPixelColor(file, 3);

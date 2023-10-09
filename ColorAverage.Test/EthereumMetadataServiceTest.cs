using ColorAverage.MetadataCollection.Ethereum;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ColorAverage.Test
{
    [TestClass]
    public class EthereumMetadataServiceTest
    {
        private readonly EthereumMetadataService EthereumMetadataService;
        private readonly List<TestCase> TestCases = new List<TestCase>();

        public EthereumMetadataServiceTest()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", true)
                .Build();

            this.EthereumMetadataService = new EthereumMetadataService(
                configuration["InfuraKey"] ?? throw new ConfigurationErrorsException("Missing infura key!"));

            //Test case 1
            this.TestCases.Add(new TestCase(
                "0xd29f5f02f5ffcd102faf467f2f236c601830780d",
                254,
                "ipfs://bafybeiczl3yhsy7ob5vgewfsb4nqhdqxwofw5oe5hd2aw6vo4acblbtyqm/254.json",
                "{\r\n  \"name\": \"Deadmigos #254\",\r\n  \"description\": \"Dreamed of moonshots but awoke to a capitulation. 🌑\",\r\n  \"image\": \"ipfs://bafybeiexo7c767mzz7k2oovxzdcem25zfn2gxvxpkegszy2ngsm3zb6ib4/254.png\",\r\n  \"imageHash\": \"0e5ead2fb01d54de8498a8c56a3a5f66473c71d0537c466ec5fe74c40242bd26\",\r\n  \"edition\": 254,\r\n  \"date\": 1695857973159,\r\n  \"attributes\": [\r\n    {\r\n      \"trait_type\": \"Human\",\r\n      \"value\": \"Ambient\"\r\n    },\r\n    {\r\n      \"trait_type\": \"Clothes\",\r\n      \"value\": \"Cult Robe\"\r\n    },\r\n    {\r\n      \"trait_type\": \"Facial Hair\",\r\n      \"value\": \"White Beard\"\r\n    },\r\n    {\r\n      \"trait_type\": \"Hat\",\r\n      \"value\": \"Graveyard Shift\"\r\n    },\r\n    {\r\n      \"trait_type\": \"Accessories\",\r\n      \"value\": \"Moon Wand\"\r\n    }\r\n  ],\r\n  \"compiler\": \"HashLips Art Engine - NFTChef fork\"\r\n}",
                "ipfs://bafybeiexo7c767mzz7k2oovxzdcem25zfn2gxvxpkegszy2ngsm3zb6ib4/254.png"));

            //test case 2
            this.TestCases.Add(new TestCase(
                "0x49cF6f5d44E70224e2E23fDcdd2C053F30aDA28B",
                1031,
                "https://clonex-assets.rtfkt.com/1031",
                "{\"name\":\"CloneX #605\",\"description\":\"\U0001f9ec CLONE X \U0001f9ec\\n\\n20,000 next-gen Avatars, by RTFKT and Takashi Murakami 🌸\\n\\nIf you own a clone without any Murakami trait please read the terms regarding RTFKT - Owned Content here: https://rtfkt.com/legal-2A\\n\\nYou are also entitled to a commercial license, please read the terms to that here: https://rtfkt.com/legal-2C\",\"attributes\":[{\"trait_type\":\"DNA\",\"value\":\"Human\"},{\"trait_type\":\"Eye Color\",\"value\":\"BLCK Surprised\"},{\"trait_type\":\"Hair\",\"value\":\"BLCK Cap\"},{\"trait_type\":\"Clothing\",\"value\":\"PNK HAWAIIAN Shirt\"},{\"trait_type\":\"Mouth\",\"value\":\"ROBO\"}],\"image\":\"https://clonex-assets.rtfkt.com/images/1031.png\"}",
                "https://clonex-assets.rtfkt.com/images/1031.png"));

            //test case 3
            this.TestCases.Add(new TestCase(
                "0x963590fabdc1333d03bc3af42a6b2ab33e21e2ee",
                2511,
                "https://api.skybornegenesis.com/metadata/nft/immortals/1188",
                "{\"name\":\"Emberling Barbarian III #10\",\"external_url\":\"https://skybornegenesis.com\",\"attributes\":[{\"trait_type\":\"Rarity\",\"value\":\"Uncommon\"},{\"trait_type\":\"Species\",\"value\":\"Emberling\"},{\"trait_type\":\"Elemental Affinity\",\"value\":\"Cryonyx\"},{\"trait_type\":\"Elemental Tier\",\"value\":2,\"display_type\":\"number\"},{\"trait_type\":\"Class\",\"value\":\"Barbarian\"},{\"trait_type\":\"Rank\",\"value\":3,\"display_type\":\"number\"},{\"trait_type\":\"Leader\",\"value\":\"False\"},{\"trait_type\":\"Weapon Type\",\"value\":\"Axe\"},{\"trait_type\":\"Weapon Tier\",\"value\":2,\"display_type\":\"number\"},{\"trait_type\":\"Damage Type\",\"value\":\"Slash\"},{\"trait_type\":\"Role\",\"value\":\"Melee DPS\"},{\"trait_type\":\"Companion\",\"value\":\"None\"},{\"trait_type\":\"Expression\",\"value\":\"Neutral\"},{\"trait_type\":\"Eye Color\",\"value\":\"Amber\"},{\"trait_type\":\"Hair Color\",\"value\":\"Obsidian\"},{\"trait_type\":\"Hairstyle\",\"value\":\"Trickster Bun\"},{\"trait_type\":\"Weapon\",\"value\":\"Prospector's Pickaxe\"},{\"trait_type\":\"Outfit\",\"value\":\"Ironclad Battleplate\"},{\"trait_type\":\"Headgear\",\"value\":\"None\"},{\"trait_type\":\"Background\",\"value\":\"Cryostone\"},{\"trait_type\":\"Background Type\",\"value\":\"Nexian Gem\"}],\"image\":\"https://nft-assets.skybornegenesis.com/immortals/1188.png\",\"description\":\"Genesis Immortals are Skyborne Legacy’s first and most elite group of heroes. In the Skyborne Genesis mini-game, your heroes can embark upon quests and will earn rewards that are transferable to Skyborne Legacy’s future titles.\"}",
                "https://nft-assets.skybornegenesis.com/immortals/1188.png"));

            //test case 4
            this.TestCases.Add(new TestCase(
                "0x0581ddf7a136c6837429a46c6cb7b388a3e52971",
                3452,
                "https://nft.blockgames.com/dice/3452",
                "{\"name\":\"BlockGames Dice #3452\",\"description\":\"BlockGames Dice holders get early access to the Player Network. Holders also benefit from full use of $BLOCK, the token that represents the full value of the network.\",\"animation_url\":\"https://nft.blockgames.com/dice/dices_gold.mp4\",\"image\":\"https://nft.blockgames.com/dice/dices_gold.gif\",\"attributes\":[{\"trait_type\":\"Rolled\",\"value\":false}]}",
                "https://nft.blockgames.com/dice/dices_gold.gif"));

            //test case 5
            this.TestCases.Add(new TestCase(
                "0xf661d58cfe893993b11d53d11148c4650590c692",
                11321,
                "https://mnlthassets.rtfkt.com/dunks/0",
                "{\r\n    \"attributes\": [\r\n        {\r\n            \"trait_type\": \"ARTEFACT\", \r\n            \"value\": \"RTFKT x NIKE DUNK GENESIS CRYPTOKICKS\"\r\n        },\r\n        {\r\n            \"trait_type\": \"MOD\", \r\n            \"value\": \"SKIN VIAL\"\r\n        },\r\n        {\r\n            \"trait_type\": \"VIAL\", \r\n            \"value\": \"UNEQUIPPED\"\r\n        }\r\n    ],\r\n    \"description\": \"Introducing the first RTFKT x Nike Sneaker NFT, the RTFKT X Nike Dunk Genesis CRYPTOKICKS Sneaker \U0001f9ea.\\n\\nWhen equipped with a RTFKT Skin Vial NFT, the look of the RTFKT x NIKE DUNK GENESIS CRYPTOKICKS changes according to the traits of the vial. \\n\\nDigital Collectible terms and conditions apply, see: [https://rtfkt.com/legal-2D](https://rtfkt.com/legal-2D)👨‍⚖️\\n\\nDigital Collectible:  Wearable\\n\\nMay have a Third Party Rights Owner if equipped with a Skin Vial having a Third Party Rights Owner (e.g., DNA: Murakami is a Third Party Rights owner). See the DNA metadata traits to determine any equipped RTFKT Skin Vial traits.\",\r\n    \"external_url\": \"https://mnlth.rtfkt.com/\",\r\n    \"image\": \"ipfs://QmUFuKaQLyhGAiugMy81ggqc28Wpn3yrDmyWtRso8t7j1H/base.png\",\r\n    \"animation_url\": \"ipfs://QmUFuKaQLyhGAiugMy81ggqc28Wpn3yrDmyWtRso8t7j1H/base.mp4\",\r\n    \"name\": \"RTFKT x Nike Dunk Genesis CRYPTOKICKS\"\r\n}",
                "ipfs://QmUFuKaQLyhGAiugMy81ggqc28Wpn3yrDmyWtRso8t7j1H/base.png"));

            //test case 6
            this.TestCases.Add(new TestCase(
                "0x76be3b62873462d2142405439777e971754e8e77",
                10844,
                "https://nftdata.parallelnft.com/api/parallel-alpha/ipfs/QmZQ6vA6U5Q9M86UUuKqX77be1vLZ7uRU63mdaceprwayU",
                "{\"name\":\"Pirate Junker\",\"description\":\"“You paid money for this, sir? On purpose?” - Chloe, Renegade Pilot\",\"external_url\":\"https://rarible.com/token/0x76be3b62873462d2142405439777e971754e8e77:10844\",\"token_id\":10844,\"attributes\":[{\"key\":\"Rarity\",\"trait_type\":\"Rarity\",\"value\":\"Common\"},{\"key\":\"Class\",\"trait_type\":\"Class\",\"value\":\"First Edition\"},{\"key\":\"Parallel\",\"trait_type\":\"Parallel\",\"value\":\"Universal\"},{\"key\":\"Artist\",\"trait_type\":\"Artist\",\"value\":\"Oscar Cafaro\"}],\"image\":\"https://nftmedia.parallelnft.com/parallel-alpha/QmPWH3xHNcptz2m7NWcLYyTKpF3TdaoZcY2AwrBHgbLkTf/image.png\"}",
                "https://nftmedia.parallelnft.com/parallel-alpha/QmPWH3xHNcptz2m7NWcLYyTKpF3TdaoZcY2AwrBHgbLkTf/image.png"));

            //test case 7
            this.TestCases.Add(new TestCase(
                "0x5b1085136a811e55b2Bb2CA1eA456bA82126A376",
                50773,
                "https://api.otherside.xyz/vessels/50773",
                "{\"image\":\"https://assets.otherside.xyz/vessels/hunter.webp\",\"attributes\":[{\"value\":\"Hunter\",\"trait_type\":\"Role\"}],\"id\":50773,\"nftLicenseTerms\":\"https://otherside.xyz/license/vessel\"}",
                "https://assets.otherside.xyz/vessels/hunter.webp"));

            //test case 8
            this.TestCases.Add(new TestCase(
                "0xC1ad47aeb274157E24A5f01B5857830aeF962843",
                6096,
                "https://asset.akumudragonz.io/meta/6096",
                "{\"name\":\"Akumu Dragonz #6096\",\"external_url\":\"https://akumudragonz.io\",\"image\":\"https://asset.akumudragonz.io/image/6096.png\",\"description\":\"Akumu Dragonz are an exclusive collection of 10,000 Dragon NFTs on the Ethereum blockchain. The Akumu Dragonz are the sister collection of Bōryoku Dragonz on the Solana blockchain, building cross-chain.\\n\\nDragon holders receive access to a strong multi-chain community and have access to exclusive drops and experiences.\",\"attributes\":[{\"trait_type\":\"Type\",\"value\":\"Ancient\"},{\"trait_type\":\"Skin\",\"value\":\"Purple\"},{\"trait_type\":\"Head\",\"value\":\"Sorty Hat\"},{\"trait_type\":\"Eye\",\"value\":\"Fire Green\"},{\"trait_type\":\"Mouth\",\"value\":\"None\"},{\"trait_type\":\"Neck\",\"value\":\"None\"},{\"trait_type\":\"Clothing\",\"value\":\"None\"},{\"trait_type\":\"Companion\",\"value\":\"None\"},{\"trait_type\":\"Background\",\"value\":\"Solid Color\"}]}",
                "https://asset.akumudragonz.io/image/6096.png"));

            //test case 9
            this.TestCases.Add(new TestCase(
                "0x0c56f29B8D90eea71D57CAdEB3216b4Ef7494abC",
                7398,
                "ipfs://QmSzn68idqk4ATRVjoToXSc6wLcxmkGMDWCyvcHvz1Ue8U/7398",
                "{\"image\":\"ipfs:\\/\\/QmNmrQgPJugCwfrMsnPbb3taEBr2BJjoA44RcRRWsHtoWE\",\"name\":\"CLOAKS #7398\",\"attributes\":[{\"trait_type\":\"Face Mask\",\"value\":\"Rose Gold\"},{\"trait_type\":\"Clan\",\"value\":\"Water\"},{\"trait_type\":\"Power\\/Strength\",\"value\":51,\"max_value\":99},{\"trait_type\":\"Archer Hair\",\"value\":\"Rose\"},{\"trait_type\":\"Speed\\/Agility\",\"value\":70,\"max_value\":99},{\"trait_type\":\"Type\",\"value\":\"Archer\"},{\"trait_type\":\"Wisdom\\/Magic\",\"value\":16,\"max_value\":99},{\"trait_type\":\"Cloak\",\"value\":\"Archer Cloak Blue\"},{\"trait_type\":\"Chest\",\"value\":\"Chestplate Samurai\"}]}",
                "ipfs://QmNmrQgPJugCwfrMsnPbb3taEBr2BJjoA44RcRRWsHtoWE"));

            //test case 10
            this.TestCases.Add(new TestCase(
                "0x60E4d786628Fea6478F785A6d7e704777c86a7c6",
                3594,
                "https://boredapeyachtclub.com/api/mutants/3594",
                "{\"image\":\"ipfs://QmRUwCJWr7yaT8DLx4Sx95BxCGaEHaP6WUiaNfsGtdmbqo\",\"attributes\":[{\"trait_type\":\"Background\",\"value\":\"M1 Gray\"},{\"trait_type\":\"Fur\",\"value\":\"M1 Dark Brown\"},{\"trait_type\":\"Eyes\",\"value\":\"M1 3d\"},{\"trait_type\":\"Clothes\",\"value\":\"M1 Bayc T Red\"},{\"trait_type\":\"Hat\",\"value\":\"M1 Army Hat\"},{\"trait_type\":\"Mouth\",\"value\":\"M1 Bored Unshaven\"}]}",
                "ipfs://QmRUwCJWr7yaT8DLx4Sx95BxCGaEHaP6WUiaNfsGtdmbqo"));
        }

        [TestMethod]
        public async Task TestGetNftMetadataUrl()
        {
            foreach (TestCase testCase in this.TestCases)
            {
                try
                {
                    string metadataUrl = await this.EthereumMetadataService.GetNftMetadataUrl(
                        testCase.ContractAddress,
                        testCase.TokenId);

                    Assert.AreEqual<string>(testCase.MetadataUrl, metadataUrl);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        [TestMethod]
        public async Task TestGetNftMetadataString()
        {
            foreach (TestCase testCase in this.TestCases)
            {
                try
                {
                    string metadataString = await this.EthereumMetadataService.GetNftMetadataString(
                        testCase.ContractAddress,
                        testCase.TokenId);

                    Assert.AreEqual<string>(
                        Regex.Replace(testCase.MetadataString, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1"),
                        Regex.Replace(metadataString, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1"));
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        [TestMethod]
        public async Task TestGetNftPictureUrl()
        {
            foreach (TestCase testCase in this.TestCases)
            {
                try
                {
                    string? nftPictureUrl = await this.EthereumMetadataService.GetNftPictureUrl(
                        testCase.ContractAddress,
                        testCase.TokenId);

                    Assert.IsNotNull(nftPictureUrl);

                    Assert.AreEqual<string>(testCase.PictureUrl, nftPictureUrl);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        [TestMethod]
        public async Task TestGetNftPicture()
        {
            foreach (TestCase testCase in this.TestCases)
            {
                byte[] picture = await this.EthereumMetadataService.GetNftPicture(
                    testCase.ContractAddress,
                    testCase.TokenId);

                Assert.IsTrue(picture.SequenceEqual(File.ReadAllBytes(
                            Path.Combine(
                                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, 
                                "pictures", 
                                testCase.TokenId.ToString()))));
            }
        }
    }

    public class TestCase
    {
        public string ContractAddress { get; }
        public int TokenId { get; }
        public string MetadataUrl { get; }
        public string MetadataString { get; }
        public string PictureUrl { get; }

        public TestCase(
            string contractAddress, 
            int tokenId, 
            string metadaUrl,
            string metadataString,
            string pictureUrl)
        {
            this.ContractAddress = contractAddress;
            this.TokenId = tokenId;
            this.MetadataUrl = metadaUrl;
            this.MetadataString = metadataString;
            this.PictureUrl = pictureUrl;
        }
    }
}

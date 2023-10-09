using ADRaffy.ENSNormalize;

namespace ColorAverage.PixelAverage
{
    /// <summary>
    /// Service for getting the most used pixel colors.
    /// </summary>
    public class PixelAverageService
    {
        public PixelAverageService() { }

        /// <summary>
        /// Gets the top used colors in a picture.
        /// </summary>
        /// <param name="file">The file that needs to be anylised.</param>
        /// <param name="amountOfColors">The amount of colors in the array.</param>
        /// <returns></returns>
        public string[] GetTopPixelColor(byte[] file, int amountOfColors)
        {
            string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            try
            {          
                File.WriteAllBytes(path, file);
                return GetTopColors(path, amountOfColors);
            }
            finally
            {
                if (File.Exists(path)) File.Delete(path);
            }
        }

        private string[] GetTopColors(string filePath, int amount)
        {
            try
            {
                using Image<Rgba32> image = Image.Load<Rgba32>(filePath);
                Dictionary<string, int> pixelList = new Dictionary<string, int>();

                // Iterate through each pixel in the image
                for (int i = 0; i < image.Height; i++)
                {
                    for (int j = 0; j < image.Width; j++)
                    {
                        if (pixelList.ContainsKey(image[i, j].ToHex()))
                            pixelList[image[i, j].ToHex()]++;
                        else pixelList.Add(image[i, j].ToHex(), 1);
                    }
                }

                return pixelList.OrderBy(e => e.Value).Select(e => e.Key).Take(amount).ToArray();
            }
            catch (Exception)
            {
                // Handle exceptions (e.g., file not found, invalid image format, etc.)
                return new string[0];
            }
        }
    }
}

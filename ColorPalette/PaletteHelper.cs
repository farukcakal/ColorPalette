
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ColorPalette
{
    public class PaletteHelper
    {
        public async Task<List<Rgba32>> GetDominantColors(Stream imageStream, int colorCount)
        {
            // Resmi yükle
            using var image = await Image.LoadAsync<Rgba32>(imageStream);

            // Çözünürlüğü küçült (hız için)
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(100, 100),
                Mode = ResizeMode.Max
            }));

            // Tüm pikselleri al
            var pixels = new List<Rgba32>();
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    var pixel = image[x, y];
                    pixels.Add(pixel);
                }
            }

            // Renkleri gruplandır ve en sık kullanılanları seç
            var groupedColors = pixels
                .GroupBy(color => color)
                .OrderByDescending(group => group.Count())
                .Take(colorCount)
                .Select(group => group.Key)
                .ToList();

            return groupedColors;
        }

        public string ToHexColor(Rgba32 color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }
    }
}

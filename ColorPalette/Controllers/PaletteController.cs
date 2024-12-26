using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;

namespace ColorPalette.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaletteController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public PaletteController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("ExtractColors")]
        public async Task<IActionResult> UploadAndExtractColors([FromForm] UploadModel model)
        {
            if (model.File == null || model.File.Length == 0) return BadRequest("Fotoğraf yüklenmedi.");

            try
            {
                PaletteHelper helper = new PaletteHelper();

                // 1. Fotoğrafı kaydet
                string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(model.File.FileName)}";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(fileStream);
                }

                // 2. Renk paletini çıkar
                using var imageStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                var dominantColors = await helper.GetDominantColors(imageStream, model.ColorCount);

                // Renkleri HEX formatında döndür
                var hexColors = dominantColors.Select(color => helper.ToHexColor(color)).ToList();

                return Ok(new
                {
                    Message = "Fotoğraf başarıyla yüklendi ve renkler çıkarıldı.",
                    Colors = hexColors
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}

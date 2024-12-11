using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using EuroCarsUSA.Data.Repositories;
using EuroCarsUSA.Data.Interfaces;

namespace EuroCarsUSA.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly ICarRepository _carRepository;

    public UploadController(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file, [FromForm] Guid carId)
    {
        if (file == null || file.Length == 0)
            return BadRequest(new { success = false, message = "No file uploaded" });

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var filePath = Path.Combine(uploadsFolder, file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var fileUrl = $"/uploads/{file.FileName}";

        var result = await _carRepository.AddImage(carId, fileUrl);
        if (!result)
        {
            return StatusCode(500, new { success = false, message = "Failed to add image to car" });
        }

        return Ok(new { success = true, url = fileUrl });
    }
}

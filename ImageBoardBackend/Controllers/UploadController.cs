// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using ImageBoard.Data;
// using ImageBoard.Models;

// namespace ImageBoard.Controllers;

// [ApiController]
// [Route("api/[controller]")]
// public class UploadController : ControllerBase
// {
//     private static readonly HashSet<string> AllowedMimeTypes = new()
//     {
//         "image/jpeg", "image/png", "image/gif", "image/webp"
//     };

//     private static readonly HashSet<string> AllowedExtensions = new()
//     {
//         ".jpg", ".jpeg", ".png", ".gif", ".webp"
//     };

//     [HttpPost]
//     public async Task<IActionResult> UploadImage(IFormFile file)
//     {
//         if (file == null || file.Length == 0)
//             return BadRequest("Invalid file");

//         if (!AllowedMimeTypes.Contains(file.ContentType))
//             return BadRequest("Invalid file type. Only JPEG, PNG, GIF, and WEBP are allowed.");

//         var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
//         if (!AllowedExtensions.Contains(fileExtension))
//             return BadRequest("Invalid file extension.");

//         var fileName = $"{Guid.NewGuid()}{fileExtension}";
//         var filePath = Path.Combine("wwwroot/uploads", fileName);

//         using (var stream = new FileStream(filePath, FileMode.Create))
//         {
//             await file.CopyToAsync(stream);
//         }

//         return Ok(new { fileUrl = $"/uploads/{fileName}" });
//     }
// }
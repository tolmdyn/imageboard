using System.ComponentModel.DataAnnotations;

namespace ImageBoard.DTOs
{
  public class PostUploadDto
  {
    [Required(ErrorMessage = "Title is required.")]
    public string? Title { get; set; }

    public string? Content { get; set; }

    public string? Category { get; set; }

    public string? Author { get; set; } 

    [Required(ErrorMessage = "An image file is required.")]
    public IFormFile? File { get; set; }
  }
}

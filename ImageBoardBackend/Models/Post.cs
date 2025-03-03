using System.ComponentModel.DataAnnotations;

namespace ImageBoard.Models
{
  public class Post
  {
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = String.Empty;
    public string? Author { get; set; }
    public string Content { get; set; } = String.Empty;
    
    public string Category { get; set; } = String.Empty;
    public string ImageUrl { get; set; } = String.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  }

}

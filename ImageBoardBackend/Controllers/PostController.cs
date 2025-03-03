using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ImageBoard.Data;
using ImageBoard.Models;
using ImageBoard.DTOs;

namespace ImageBoard.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PostController : ControllerBase
  {
    private readonly ApplicationDbContext _context;

    private static readonly HashSet<string> AllowedMimeTypes = new()
    {
        "image/jpeg", "image/png", "image/gif", "image/webp"
    };

    private static readonly HashSet<string> AllowedExtensions = new()
    {
        ".jpg", ".jpeg", ".png", ".gif", ".webp"
    };

    public PostController(ApplicationDbContext context)
    {
      _context = context;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
    {
      return await _context.Posts.ToListAsync();
    }

    [HttpGet("by-id/{id}")]
    public async Task<ActionResult<Post>> GetPost(int id)
    {
      var post = await _context.Posts.FindAsync(id);
      if (post == null)
        return NotFound();

      return post;
    }

    [HttpGet("by-category/{category}")]
    public async Task<ActionResult<Post>> GetPost(string category)
    {
      var posts = await _context.Posts.Where(p => p.Category == category).ToListAsync();

      return Ok(posts);
    }

    // [HttpPost]
    // public async Task<ActionResult<Post>> CreatePost([FromBody] Post post)
    // {
    //   _context.Posts.Add(post);
    //   await _context.SaveChangesAsync();
    //   return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
    // }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromForm] PostUploadDto postDto)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      if (postDto.File == null || postDto.File.Length == 0)
        return BadRequest("Invalid file");

      if (!AllowedMimeTypes.Contains(postDto.File.ContentType))
        return BadRequest("Invalid file type. Only JPEG, PNG, GIF, and WEBP are allowed.");

      var fileExtension = Path.GetExtension(postDto.File.FileName).ToLowerInvariant();
      if (!AllowedExtensions.Contains(fileExtension))
        return BadRequest("Invalid file extension.");

      var fileName = $"{Guid.NewGuid()}{fileExtension}";
      var filePath = Path.Combine("wwwroot/uploads", fileName);

      using (var stream = new FileStream(filePath, FileMode.Create))
      {
        await postDto.File.CopyToAsync(stream);
      }

      var newPost = new Post
    {
        Title = postDto.Title!,
        Content = postDto.Content ?? "", // Allow blank
        Category = postDto.Category!,
        Author = postDto.Author,
        ImageUrl = $"/uploads/{fileName}",
        CreatedAt = DateTime.UtcNow
    };

      _context.Posts.Add(newPost);
      await _context.SaveChangesAsync();

      return Ok(newPost);
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePost(int id)
    {
      var post = await _context.Posts.FindAsync(id);
      if (post == null)
      {
        return NotFound();
      }

      _context.Posts.Remove(post);
      await _context.SaveChangesAsync();
      return NoContent();
    }

  }
}


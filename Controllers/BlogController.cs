using BlogApi.Models;
using Microsoft.AspNetCore.Mvc;


namespace BlogApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogController : ControllerBase
	{
		private readonly BlogDbContext _context;

		public BlogController(BlogDbContext context)
		{
			_context = context;
		}

		[HttpGet("all")]
		public async Task<ActionResult<List<Blog>>> GetBlog()
		{
			return Ok(await _context.Blogs.ToListAsync());
		}

		[HttpGet("id")]
		public async Task<ActionResult<List<Blog>>> GetBlogById(int id)
		{
			var blog = await _context.Blogs.FirstOrDefaultAsync(blog => blog.Id == id);
			if (blog == null)
				return BadRequest("Blog not found.");
			return Ok(blog);
		}

		[HttpDelete]
		public async Task<ActionResult<List<Blog>>> DeleteBlog(int id)
		{
			var blog = await _context.Blogs.FindAsync(id);
			if (blog == null)
				return BadRequest("Blog not found.");
			_context.Blogs.Remove(blog);
			await _context.SaveChangesAsync();

			return Ok(await _context.Categories.ToListAsync());
		}
	}
}

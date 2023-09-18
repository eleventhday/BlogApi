using BlogApi.Models;
using Microsoft.AspNetCore.Mvc;


namespace BlogApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly BlogDbContext _context;

		public CategoryController(BlogDbContext context)
		{
			_context = context;
		}
		[HttpGet("id")]
		public async Task<ActionResult<Category>> GetCategory(int id)
		{
			var category = await _context.Categories.FirstOrDefaultAsync(category => category.Id == id);
			if (category == null)
				return BadRequest("Category not found.");
			return Ok(category);
		}

		[HttpGet("all")]
		public async Task<ActionResult<List<Category>>> GetCategoryAll()
		{
			return Ok(await _context.Categories.ToListAsync());
		}

		[HttpPost]
		public async Task<ActionResult<List<Category>>> AddCategory(Category category)
		{
			_context.Categories.Add(category);
			await _context.SaveChangesAsync();
			return Ok(await _context.Categories.ToListAsync());
		}

		[HttpPut]
		public async Task<ActionResult<List<Category>>> ChangeCategory(Category request)
		{
			var category = await _context.Categories.FindAsync(request.Id);

			if (category == null)
				return BadRequest("Category not found.");

			category.Name = request.Name;

			await _context.SaveChangesAsync();

			return Ok(await _context.Categories.ToListAsync());
		}

		[HttpDelete]
		public async Task<ActionResult<List<Category>>> DeleteCategory(int id)
		{
			var category = await _context.Categories.FindAsync(id);
			if (category == null)
				return BadRequest("Category not found.");
			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();

			return Ok(await _context.Categories.ToListAsync());
		}
	}
}

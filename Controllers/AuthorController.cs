using BlogApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorController : ControllerBase
	{
		private readonly BlogDbContext _context;

		public AuthorController(BlogDbContext context)
		{
			_context = context;
		}

		[HttpGet("all")]
		public async Task<ActionResult<List<Author>>> GetAuthorAll()
		{
			return Ok(await _context.Authors.ToListAsync());
		}

		[HttpGet("id")]
		public async Task<ActionResult<Author>> GetAuthor(int id)
		{
			var author = await _context.Authors.FirstOrDefaultAsync(author => author.Id == id);
			if (author == null)
				return BadRequest("Author not found.");
			return Ok(author);
		}

		[HttpPost]
		public async Task<ActionResult<List<Author>>> AddHero(Author author)
		{
			_context.Authors.Add(author);
			await _context.SaveChangesAsync();

			return Ok(await _context.Authors.ToListAsync());
		}

		[HttpPut]
		public async Task<ActionResult<List<Author>>> ChangeAuthor(Author request)
		{
			var author = await _context.Authors.FindAsync(request.Id);

			if (author == null)
				return BadRequest("Author not found.");

			author.Name = request.Name;
			author.Surname = request.Surname;
			author.Email = request.Email;
			author.Password = request.Password;

			await _context.SaveChangesAsync();

			return Ok(await _context.Authors.ToListAsync());
		}

		[HttpDelete]
		public async Task<ActionResult<List<Author>>> DeleteAuthor(int id)
		{
			var author = await _context.Authors.FindAsync(id);
			if (author == null)
				return BadRequest("Author not found.");
			_context.Authors.Remove(author);
			await _context.SaveChangesAsync();

			return Ok(await _context.Authors.ToListAsync());
		}
	}
}

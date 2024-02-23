using Microsoft.AspNetCore.Mvc;

namespace BlazorFullStackCrud.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SuperHeroController : ControllerBase
	{
		private readonly DataContext _context;
		public SuperHeroController(DataContext context)
		{
			_context = context;


		}

		[HttpGet]
		public async Task<ActionResult<List<SuperHeroModel>>> GetSuperHeroes()
		{
			List<SuperHeroModel> Heroes = await _context.SuperHeroes
				.Include(h => h.Comic)
				.ToListAsync();
			return Ok(Heroes);
		}
		[HttpGet("comics")]
		public async Task<ActionResult<List<ComicModel>>> GetComics()
		{
			var Comics = await _context.Comics.ToListAsync();
			return Ok(Comics);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<List<SuperHeroModel>>> GetSingleHero(int id)
		{
			var hero = await _context.SuperHeroes
				.Include(h => h.Comic)
				.FirstOrDefaultAsync(h => h.Id == id);

			if (hero == null)
			{
				return NotFound("Sorry couldn't find that hero");
			}
			return Ok(hero);
		}

		[HttpPost]
		public async Task<ActionResult<List<SuperHeroModel>>> CreateSuperhero(SuperHeroModel superHero)
		{
			superHero.Comic = null!;

			_context.SuperHeroes.Add(superHero);
			await _context.SaveChangesAsync();

			return Ok(await GetDbHeroes());
		}

		[HttpPut("{id:int}")]
		public async Task<ActionResult<List<SuperHeroModel>>> UpdateSuperHero(SuperHeroModel superHero, int id)
		{
			var dbHero = await _context.SuperHeroes
				.Include(h => h.Comic)
				.FirstOrDefaultAsync(h => h.Id == id);
			if (dbHero == null)
			{
				return NotFound("Sorry but no hero found");
			}
			dbHero.FirstName = superHero.FirstName;
			dbHero.LastName = superHero.LastName;
			dbHero.HeroName = superHero.HeroName;
			dbHero.ComicId = superHero.ComicId;

			await _context.SaveChangesAsync();

			return Ok(await GetDbHeroes());
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult<List<SuperHeroModel>>> DeleteSuperHero(int id)
		{
			var dbHero = await _context.SuperHeroes
				.Include(h => h.Comic)
				.FirstOrDefaultAsync(h => h.Id == id);
			if (dbHero == null)
			{
				return NotFound("Sorry but no hero found");
			}

			_context.SuperHeroes.Remove(dbHero);
			await _context.SaveChangesAsync();

			return Ok(await GetDbHeroes());
		}

		private async Task<List<SuperHeroModel>> GetDbHeroes()
		{
			return await _context.SuperHeroes.Include(h => h.Comic).ToListAsync();
		}
	}
}

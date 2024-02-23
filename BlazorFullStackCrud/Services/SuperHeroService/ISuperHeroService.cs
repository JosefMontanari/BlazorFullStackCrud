using BlazorFullStackCrud.Shared.Models;

namespace BlazorFullStackCrud.Services.SuperHeroService
{
	public interface ISuperHeroService
	{
		List<SuperHeroModel> Heroes { get; set; }
		List<ComicModel> Comics { get; set; }

		public Task<List<ComicModel>> GetComics();
		Task GetSuperHeroes();
		Task<SuperHeroModel> GetSingleHero(int id);
		Task CreateHero(SuperHeroModel model);
		Task Deletehero(int id);
		Task UpdateHero(SuperHeroModel model);
	}
}

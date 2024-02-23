using BlazorFullStackCrud.Shared.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace BlazorFullStackCrud.Services.SuperHeroService
{
	public class SuperHeroService : ISuperHeroService
	{
		private readonly HttpClient Client;
		private readonly NavigationManager NavigationManager;

		public SuperHeroService(HttpClient client, NavigationManager navigationManager)
		{
			Client = new HttpClient()
			{
				BaseAddress = new Uri("https://localhost:7186/api/")
			};

			NavigationManager = navigationManager;
		}
		public List<SuperHeroModel> Heroes { get; set; } = new List<SuperHeroModel>();
		public List<ComicModel> Comics { get; set; } = new List<ComicModel>();

		public async Task CreateHero(SuperHeroModel model)
		{
			var result = await Client.PostAsJsonAsync("superhero", model);
			await SetHeroes(result);
		}

		public async Task Deletehero(int id)
		{
			var result = await Client.DeleteAsync($"superhero/{id}");
			await SetHeroes(result);
		}

		private async Task SetHeroes(HttpResponseMessage result)
		{
			var response = await result.Content.ReadFromJsonAsync<List<SuperHeroModel>>();
			Heroes = response;
			NavigationManager.NavigateTo("/SuperHeroes");
		}

		public async Task<List<ComicModel>> GetComics()
		{
			var result = await Client.GetAsync("SuperHero/comics");
			if (result.IsSuccessStatusCode)
			{
				string comicsJson = await result.Content.ReadAsStringAsync();
				List<ComicModel>? comics = JsonConvert.DeserializeObject<List<ComicModel>>(comicsJson);
				if (comics != null)
				{
					Comics = comics.ToList();
					return Comics;
				}
				throw new JsonException("Couldn't find the json");
			}
			throw new HttpRequestException("Couldnt find that adress");
		}

		public async Task<SuperHeroModel> GetSingleHero(int id)
		{
			var result = await Client.GetAsync($"SuperHero/{id}");
			if (result.IsSuccessStatusCode)
			{
				string superHeroJson = await result.Content.ReadAsStringAsync();
				SuperHeroModel? hero = JsonConvert.DeserializeObject<SuperHeroModel>(superHeroJson);
				if (hero != null)
				{
					return hero;
				}
				throw new JsonException("Couldn't find the json");

			}
			throw new HttpRequestException("Couldnt find that adress");

		}

		public async Task GetSuperHeroes()
		{
			var result = await Client.GetFromJsonAsync<List<SuperHeroModel>>("SuperHero");
			if (result != null)
				Heroes = result;


		}

		public async Task UpdateHero(SuperHeroModel model)
		{
			var result = await Client.PutAsJsonAsync($"superhero/{model.Id}", model);
			await SetHeroes(result);
		}
	}
}

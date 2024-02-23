namespace BlazorFullStackCrud.Shared.Models
{
	public class SuperHeroModel
	{
		public int Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string HeroName { get; set; } = string.Empty;
		public ComicModel? Comic { get; set; }
		public int ComicId { get; set; }
	}
}

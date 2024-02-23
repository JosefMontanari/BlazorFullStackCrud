namespace BlazorFullStackCrud.Server.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ComicModel>().HasData(
				new ComicModel
				{
					Id = 1,
					Name = "Marvel"
				},
				new ComicModel
				{
					Id = 2,
					Name = "DC"
				}
				);

			modelBuilder.Entity<SuperHeroModel>().HasData(
			new SuperHeroModel
			{
				Id = 1,
				FirstName = "Peter",
				LastName = "Parker",
				HeroName = "Spider-Man",
				ComicId = 1,

			},
			new SuperHeroModel
			{
				Id = 2,
				FirstName = "Bruce",
				LastName = "Wayne",
				HeroName = "Batman",
				ComicId = 2,
			}
			);
		}
		public DbSet<SuperHeroModel> SuperHeroes { get; set; }
		public DbSet<ComicModel> Comics { get; set; }

	}

}



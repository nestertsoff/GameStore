namespace GameStore.DAL.Infrastructure.CombineRepositories.Concrete
{
    using global::GameStore.DAL.GameStore.Entities.Concrete;
    using global::GameStore.DAL.GameStore.Repositories.Abstract;
    using global::GameStore.DAL.Infrastructure.CombineRepositories.Abstract;
    using global::GameStore.DAL.Northwind;
    using global::GameStore.DAL.Northwind.Repositories.Abstract;

    public class GenreCombineRepository : GenericCombineRepository<Genre, Category>, IGenreCombineRepository
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IGenreRepository _genreRepository;

        public GenreCombineRepository(IGenreRepository genreRepository, ICategoryRepository categoryRepository)
            : base(genreRepository, categoryRepository)
        {
            _genreRepository = genreRepository;
            _categoryRepository = categoryRepository;
        }
    }
}
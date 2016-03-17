namespace GameStore.BLL.Services.Abstract
{
    using System.Collections.Generic;

    using GameStore.BLL.Dtos.Concrete.InputDtos.FilterInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.GameInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.GenreInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.PlatformTypeInput;
    using GameStore.BLL.Dtos.Concrete.OutputDtos;

    public interface IGameService : IServise
    {
        IEnumerable<GameOutput> Get();

        GameOutput Get(int id);

        GameOutput GetByKey(string key);

        IEnumerable<GameOutput> GetFiltered(GameFilterInput filters);

        IEnumerable<GameOutput> GetFiltered(GameFilterInput filters, int takeCount, int skipCount);

        IEnumerable<GameOutput> GetPaginated(int takeCount, int skipCount);

        IEnumerable<GameOutput> GetByGenres(List<GetDeleteGenreInput> genres);

        IEnumerable<GameOutput> GetByPlatformTypes(List<GetDeletePlatformTypeInput> platformIdList);

        void Create(CreateUpdateGameInput item);

        void Update(CreateUpdateGameInput item);

        void Delete(string key);
    }
}
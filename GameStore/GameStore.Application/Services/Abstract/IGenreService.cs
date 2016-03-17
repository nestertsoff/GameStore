namespace GameStore.BLL.Services.Abstract
{
    using System.Collections.Generic;

    using GameStore.BLL.Dtos.Concrete.OutputDtos;

    public interface IGenreService : IServise
    {
        IEnumerable<GenreOutput> Get();

        GenreOutput Get(int id);
    }
}
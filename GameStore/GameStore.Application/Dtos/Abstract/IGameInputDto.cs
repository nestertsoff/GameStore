namespace GameStore.BLL.Dtos.Abstract
{
    using System.Collections.Generic;

    using GameStore.BLL.Dtos.Concrete.OutputDtos;

    public interface IGameInputDto
    {
        string Key { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        decimal Price { get; set; }

        short UnitsInStock { get; set; }

        int PublisherId { get; set; }

        PublisherOutput Publisher { get; set; }

        IEnumerable<GenreOutput> Genres { get; set; }

        IEnumerable<PlatformTypeOutput> PlatformTypes { get; set; }
    }
}
namespace GameStore.BLL.Dtos.Concrete.OutputDtos
{
    using System.Collections.Generic;

    using GameStore.BLL.Dtos.Abstract;

    public class GenreOutput : IOutputDto
    {
        public string Name { get; set; }

        public string NameRu { get; set; }

        public GenreOutput ParentGenre { get; set; }

        public IEnumerable<GenreOutput> ChildGenres { get; set; }

        public bool IsDeleted { get; set; }

        public int Id { get; set; }
    }
}
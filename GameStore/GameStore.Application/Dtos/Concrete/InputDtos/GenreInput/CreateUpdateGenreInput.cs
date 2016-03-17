namespace GameStore.BLL.Dtos.Concrete.InputDtos.GenreInput
{
    using System.Collections.Generic;

    using GameStore.BLL.Dtos.Abstract;

    public class CreateUpdateGenreInput : IInputDto
    {
        public string Name { get; set; }

        public string NameRu { get; set; }

        public CreateUpdateGenreInput ParentGenre { get; set; }

        public IEnumerable<CreateUpdateGenreInput> ChildGenres { get; set; }

        public int Id { get; set; }
    }
}
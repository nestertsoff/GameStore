namespace GameStore.BLL.Dtos.Concrete.InputDtos.GameInput
{
    using System;
    using System.Collections.Generic;

    using GameStore.BLL.Dtos.Abstract;
    using GameStore.BLL.Dtos.Concrete.InputDtos.GenreInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.PlatformTypeInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.PublisherInput;

    public class CreateUpdateGameInput : IInputDto
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public string NameRu { get; set; }

        public string Description { get; set; }

        public string DescriptionRu { get; set; }

        public decimal Price { get; set; }

        public short UnitsInStock { get; set; }

        public bool Discontinued { get; set; }

        public int PublisherId { get; set; }

        public CreateUpdatePublisherInput Publisher { get; set; }

        public DateTime PublishDate { get; set; }

        public IEnumerable<CreateUpdateGenreInput> Genres { get; set; }

        public IEnumerable<CreateUpdatePlatformTypeInput> PlatformTypes { get; set; }

        public int Id { get; set; }
    }
}
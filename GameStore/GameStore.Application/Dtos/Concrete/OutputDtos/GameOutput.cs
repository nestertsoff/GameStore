namespace GameStore.BLL.Dtos.Concrete.OutputDtos
{
    using System;
    using System.Collections.Generic;

    using GameStore.BLL.Dtos.Abstract;

    public class GameOutput : IOutputDto
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

        public PublisherOutput Publisher { get; set; }

        public DateTime PublishDate { get; set; }

        public IEnumerable<CommentOutput> Comments { get; set; }

        public IEnumerable<GenreOutput> Genres { get; set; }

        public IEnumerable<PlatformTypeOutput> PlatformTypes { get; set; }

        public bool IsDeleted { get; set; }

        public int Id { get; set; }
    }
}
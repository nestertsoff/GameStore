namespace GameStore.BLL.Dtos.Concrete.OutputDtos
{
    using System.Collections.Generic;

    using GameStore.BLL.Dtos.Abstract;

    public class PublisherOutput : IOutputDto
    {
        public string CompanyName { get; set; }

        public string Description { get; set; }

        public string DescriptionRu { get; set; }

        public string HomePage { get; set; }

        public IEnumerable<GameOutput> Games { get; set; }

        public int Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
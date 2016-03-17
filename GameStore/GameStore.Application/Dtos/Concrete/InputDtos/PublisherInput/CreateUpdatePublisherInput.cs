namespace GameStore.BLL.Dtos.Concrete.InputDtos.PublisherInput
{
    using System.Collections.Generic;

    using GameStore.BLL.Dtos.Abstract;
    using GameStore.BLL.Dtos.Concrete.InputDtos.GameInput;

    public class CreateUpdatePublisherInput : IInputDto
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }

        public string DescriptionRu { get; set; }

        public string HomePage { get; set; }

        public IEnumerable<CreateUpdateGameInput> Games { get; set; }
    }
}
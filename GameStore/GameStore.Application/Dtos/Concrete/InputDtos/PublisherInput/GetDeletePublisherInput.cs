namespace GameStore.BLL.Dtos.Concrete.InputDtos.PublisherInput
{
    using GameStore.BLL.Dtos.Abstract;

    public class GetDeletePublisherInput : IInputDto
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }
    }
}
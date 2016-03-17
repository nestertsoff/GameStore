namespace GameStore.BLL.Dtos.Concrete.InputDtos.PlatformTypeInput
{
    using GameStore.BLL.Dtos.Abstract;

    public class GetDeletePlatformTypeInput : IInputDto
    {
        public int Id { get; set; }

        public string Type { get; set; }
    }
}
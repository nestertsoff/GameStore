namespace GameStore.BLL.Dtos.Concrete.InputDtos.PlatformTypeInput
{
    using System.Collections.Generic;

    using GameStore.BLL.Dtos.Abstract;
    using GameStore.BLL.Dtos.Concrete.InputDtos.GameInput;

    public class CreateUpdatePlatformTypeInput : IInputDto
    {
        public string Type { get; set; }

        public string TypeRu { get; set; }

        public IEnumerable<CreateUpdateGameInput> Games { get; set; }

        public int Id { get; set; }
    }
}
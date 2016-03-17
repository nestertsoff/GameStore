namespace GameStore.BLL.Dtos.Concrete.InputDtos.GameInput
{
    using System.Collections.Generic;

    using GameStore.BLL.Dtos.Abstract;
    using GameStore.DAL.GameStore.Entities.Concrete;

    public class GetDeleteGameInput : IInputDto
    {
        public string Key { get; set; }

        public IEnumerable<PlatformType> PlatformTypes { get; set; }

        public int Id { get; set; }
    }
}
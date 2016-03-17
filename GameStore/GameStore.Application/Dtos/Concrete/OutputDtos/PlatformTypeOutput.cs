namespace GameStore.BLL.Dtos.Concrete.OutputDtos
{
    using System.Collections.Generic;

    using GameStore.BLL.Dtos.Abstract;

    public class PlatformTypeOutput : IOutputDto
    {
        public string Type { get; set; }

        public string TypeRu { get; set; }

        public IEnumerable<GameOutput> Games { get; set; }

        public bool IsDeleted { get; set; }

        public int Id { get; set; }
    }
}
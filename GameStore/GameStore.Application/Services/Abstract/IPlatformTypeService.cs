namespace GameStore.BLL.Services.Abstract
{
    using System.Collections.Generic;

    using GameStore.BLL.Dtos.Concrete.OutputDtos;

    public interface IPlatformTypeService
    {
        IEnumerable<PlatformTypeOutput> Get();

        PlatformTypeOutput Get(int id);
    }
}
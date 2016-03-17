namespace GameStore.BLL.Services.Abstract
{
    using System.Collections.Generic;

    using GameStore.BLL.Dtos.Concrete.InputDtos.PublisherInput;
    using GameStore.BLL.Dtos.Concrete.OutputDtos;

    public interface IPublisherService : IServise
    {
        IEnumerable<PublisherOutput> Get();

        PublisherOutput Get(int id);

        PublisherOutput GetByCompanyName(string company);

        void Create(CreateUpdatePublisherInput item);

        void Update(CreateUpdatePublisherInput item);

        void Delete(int id);
    }
}
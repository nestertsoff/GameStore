namespace GameStore.DAL.Infrastructure.CombineRepositories.Concrete
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using global::GameStore.DAL.GameStore.Entities.Concrete;
    using global::GameStore.DAL.GameStore.Repositories.Abstract;
    using global::GameStore.DAL.Infrastructure.CombineRepositories.Abstract;
    using global::GameStore.DAL.Infrastructure.Utils;
    using global::GameStore.DAL.Northwind;
    using global::GameStore.DAL.Northwind.Repositories.Abstract;

    public class PublisherCombineRepository : GenericCombineRepository<Publisher, Supplier>, IPublisherCombineRepository
    {
        private readonly IPublisherRepository _publisherRepository;

        private readonly ISupplierRepository _supplierRepository;

        public PublisherCombineRepository(
            IPublisherRepository publisherRepository,
            ISupplierRepository supplierRepository)
            : base(publisherRepository, supplierRepository)
        {
            _publisherRepository = publisherRepository;
            _supplierRepository = supplierRepository;
        }
    }
}
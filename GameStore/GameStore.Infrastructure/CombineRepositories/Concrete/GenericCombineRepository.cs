namespace GameStore.DAL.Infrastructure.CombineRepositories.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using global::GameStore.DAL.Abstract.Entities.Abstract;
    using global::GameStore.DAL.Abstract.Entities.Concrete;
    using global::GameStore.DAL.Abstract.Repositories.Abstract;
    using global::GameStore.DAL.Infrastructure.CombineRepositories.Abstract;
    using global::GameStore.DAL.Infrastructure.Utils;

    public class GenericCombineRepository<TGameStoreEntity, TNorthwindEntity> : ICombineRepository<TGameStoreEntity>
        where TNorthwindEntity : class, IEntity where TGameStoreEntity : Entity, IEntity
    {
        private readonly IGameStoreRepository<TGameStoreEntity> _gameStoreRepository;

        private readonly INorthwindRepository<TNorthwindEntity> _northwindRepository;

        public GenericCombineRepository(
            IGameStoreRepository<TGameStoreEntity> gameStoreRepository,
            INorthwindRepository<TNorthwindEntity> northwindRepository)
        {
            _gameStoreRepository = gameStoreRepository;
            _northwindRepository = northwindRepository;
        }

        public IEnumerable<TGameStoreEntity> Get()
        {
            var gameStoreEntities = _gameStoreRepository.Get().ToList();
            gameStoreEntities.ForEach(_ => _.Id = DbIdentifier.EncodeId(_.Id, DbType.GameStore));

            var northwindEntities = Mapper.Map<List<TGameStoreEntity>>(_northwindRepository.Get()).ToList();
            //northwindEntities.ForEach(_ => _.Id = DbIdentifier.EncodeId(_.Id, DbType.Northwind));

            northwindEntities.ForEach(
                x =>
                    {
                        if (!gameStoreEntities.Select(z => z.Id).Contains(x.Id))
                        {
                            gameStoreEntities.Add(x);
                        }
                    });

            return gameStoreEntities.Where(_ => _.IsDeleted == false).ToList();
        }

        public TGameStoreEntity Get(int id)
        {
            return Get().SingleOrDefault(_ => _.Id == id);
        }

        public IEnumerable<TGameStoreEntity> Find(Func<TGameStoreEntity, bool> predicate)
        {
            return Get().Where(predicate).ToList();
        }

        public void Create(TGameStoreEntity item)
        {
            item.Id = DbIdentifier.DecodeId(item.Id);
            _gameStoreRepository.Create(item);
        }

        public void Update(TGameStoreEntity item)
        {
            item.Id = DbIdentifier.DecodeId(item.Id);
            _gameStoreRepository.Update(item);
        }

        public void Delete(int id)
        {
            var item = Get(DbIdentifier.DecodeId(id));
            item.IsDeleted = true;
            _gameStoreRepository.CreateOrUpdate(item);
        }
    }
}
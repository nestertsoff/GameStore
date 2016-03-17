namespace GameStore.BLL.Services.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using FluentValidation;

    using GameStore.BLL.Dtos.Concrete.InputDtos.FilterInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.GameInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.GenreInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.PlatformTypeInput;
    using GameStore.BLL.Dtos.Concrete.OutputDtos;
    using GameStore.BLL.Filtration.Concrete;
    using GameStore.BLL.Services.Abstract;
    using GameStore.DAL.GameStore.Entities.Concrete;
    using GameStore.DAL.Infrastructure.UnitOfWork.Abstract;

    using NLog;

    public class GameService : IGameService
    {
        private readonly IValidator<CreateUpdateGameInput> _createUpdateGameValidator;

        private readonly IValidator<GetDeleteGameInput> _getDeleteGameValidator;

        private readonly IValidator<GetDeleteGenreInput> _getDeleteGenreValidator;

        private readonly IValidator<GetDeletePlatformTypeInput> _getDeletePlatformTypeValidator;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        public GameService(IUnitOfWork unitOfWork, IValidatorFactory validatorFactory, ILogger logger)
        {
            _unitOfWork = unitOfWork;

            _logger = logger;
            _createUpdateGameValidator = validatorFactory.GetValidator<CreateUpdateGameInput>();
            _getDeleteGameValidator = validatorFactory.GetValidator<GetDeleteGameInput>();
            _getDeleteGenreValidator = validatorFactory.GetValidator<GetDeleteGenreInput>();
            _getDeletePlatformTypeValidator = validatorFactory.GetValidator<GetDeletePlatformTypeInput>();
        }

        public IEnumerable<GameOutput> Get()
        {
            try
            {
                var query = _unitOfWork.GetGamesFromAllDbs().Get();
                return Mapper.Map<List<GameOutput>>(query);
            }
            catch (Exception exception)
            {
                _logger.Trace(exception.StackTrace);
                throw;
            }
        }

        public GameOutput Get(int id)
        {
            try
            {
                var game = new GetDeleteGameInput { Id = id };
                var validationResults = _getDeleteGameValidator.Validate(game, "Id");

                if (!validationResults.IsValid)
                {
                    throw new ValidationException(validationResults.Errors);
                }

                var query = _unitOfWork.GetGamesFromAllDbs().Get(id);
                return Mapper.Map<GameOutput>(query);
            }
            catch (ValidationException exception)
            {
                _logger.Debug(exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.Trace(exception.StackTrace);
                throw;
            }
        }

        public GameOutput GetByKey(string key)
        {
            try
            {
                var game = new GetDeleteGameInput { Key = key };
                var validationResults = _getDeleteGameValidator.Validate(game, "Key");

                if (!validationResults.IsValid)
                {
                    throw new ValidationException(validationResults.Errors);
                }

                var query = _unitOfWork.GetGamesFromAllDbs().Find(_ => _.Key == key).SingleOrDefault();
                return Mapper.Map<GameOutput>(query);
            }
            catch (ValidationException exception)
            {
                _logger.Debug(exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.Trace(exception.StackTrace);
                throw;
            }
        }

        public IEnumerable<GameOutput> GetByGenres(List<GetDeleteGenreInput> genres)
        {
            try
            {
                genres.ForEach(_ => _getDeleteGenreValidator.ValidateAndThrow(_));

                var query =
                    _unitOfWork.GetGamesFromAllDbs()
                        .Get()
                        .Where(
                            game =>
                            game.Genres.Select(genre => genre.Id).Intersect(genres.Select(genre => genre.Id)).Count()
                            != 0);

                return Mapper.Map<List<GameOutput>>(query);
            }
            catch (ValidationException exception)
            {
                _logger.Warn(exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.Trace(exception.StackTrace);
                throw;
            }
        }

        public IEnumerable<GameOutput> GetByPlatformTypes(List<GetDeletePlatformTypeInput> platformTypes)
        {
            try
            {
                platformTypes.ForEach(_ => _getDeletePlatformTypeValidator.ValidateAndThrow(_));

                var query =
                    _unitOfWork.GetGamesFromAllDbs()
                        .Get()
                        .Where(
                            game =>
                            game.PlatformTypes.Select(platform => platform.Id)
                                .Intersect(platformTypes.Select(platform => platform.Id))
                                .Count() != 0);

                return Mapper.Map<List<GameOutput>>(query);
            }
            catch (ValidationException exception)
            {
                _logger.Warn(exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.Trace(exception.StackTrace);
                throw;
            }
        }

        public IEnumerable<GameOutput> GetFiltered(GameFilterInput filters)
        {
            var gameFilterChain = new GameFilterChain();
            RegisterFilters(gameFilterChain, filters);
            var sortFilter = new SortFilter(filters.SortType);
            var sortPredicate = sortFilter.Execute(x => x);
            var predicate = gameFilterChain.Execute(x => !x.IsDeleted);
            return Mapper.Map<List<GameOutput>>(_unitOfWork.GetGamesFromAllDbs().GetSorted(sortPredicate, predicate));
        }

        public IEnumerable<GameOutput> GetFiltered(GameFilterInput filters, int takeCount, int skipCount)
        {
            var gameFilterChain = new GameFilterChain();
            RegisterFilters(gameFilterChain, filters);
            var sortFilter = new SortFilter(filters.SortType);
            var sortPredicate = sortFilter.Execute(x => x);
            var predicate = gameFilterChain.Execute(x => !x.IsDeleted);
            return Mapper.Map<List<GameOutput>>(_unitOfWork.GetGamesFromAllDbs().GetSorted(sortPredicate, predicate).Skip(skipCount).Take(takeCount));
        }

        public IEnumerable<GameOutput> GetPaginated(int takeCount, int skipCount)
        {
            return Mapper.Map<List<GameOutput>>(_unitOfWork.GetGamesFromAllDbs().Get().Skip(skipCount).Take(takeCount));
        }

        public void Create(CreateUpdateGameInput item)
        {
            try
            {
                var validationResults = _createUpdateGameValidator.Validate(item, ruleSet: "Create");

                if (!validationResults.IsValid)
                {
                    throw new ValidationException(validationResults.Errors);
                }

                _unitOfWork.GetGamesFromAllDbs().Create(Mapper.Map<Game>(item));
                _unitOfWork.Save();
            }
            catch (ValidationException exception)
            {
                _logger.Warn(exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.Trace(exception.StackTrace);
                throw;
            }
        }

        public void Update(CreateUpdateGameInput item)
        {
            try
            {
                var validationResults = _createUpdateGameValidator.Validate(item, ruleSet: "Update");

                if (!validationResults.IsValid)
                {
                    throw new ValidationException(validationResults.Errors);
                }

                _unitOfWork.GetGamesFromAllDbs().Update(Mapper.Map<Game>(item));
                _unitOfWork.Save();
            }
            catch (ValidationException exception)
            {
                _logger.Warn(exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.Trace(exception.StackTrace);
                throw;
            }
        }

        public void Delete(string key)
        {
            try
            {
                var game = new GetDeleteGameInput { Key = key };
                var validationResults = _getDeleteGameValidator.Validate(game, "Key");

                if (!validationResults.IsValid)
                {
                    throw new ValidationException(validationResults.Errors);
                }

                _unitOfWork.GetGamesFromAllDbs().Delete(key);
                _unitOfWork.Save();
            }
            catch (ValidationException exception)
            {
                _logger.Warn(exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.Trace(exception.StackTrace);
                throw;
            }
        }
        
        private static void RegisterFilters(GameFilterChain gameFilterChain, GameFilterInput filters)
        {
            gameFilterChain.Register(new PlatformFilter(filters.PlatformIds))
                .Register(new GenreFilter(filters.GenreIds))
                .Register(new PublisherFilter(filters.PublisherIds))
                .Register(new GameNameFilter(filters.SearchString))
                .Register(new GamePriceFilter(filters.MinPrice, filters.MaxPrice))
                .Register(new GamePublishDateFilter(filters.PeriodType));
        }
    }
}
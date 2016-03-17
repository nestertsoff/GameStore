namespace GameStore.BLL.Services.Concrete
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using FluentValidation;

    using GameStore.BLL.Dtos.Concrete.InputDtos.GenreInput;
    using GameStore.BLL.Dtos.Concrete.OutputDtos;
    using GameStore.BLL.Services.Abstract;
    using GameStore.DAL.Infrastructure.UnitOfWork.Abstract;

    using NLog;

    public class GenreService : IGenreService
    {
        private readonly IValidator<GetDeleteGenreInput> _getDeleteGenreValidator;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        public GenreService(IUnitOfWork unitOfWork, IValidatorFactory validatorFactory, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _getDeleteGenreValidator = validatorFactory.GetValidator<GetDeleteGenreInput>();
        }

        public IEnumerable<GenreOutput> Get()
        {
            try
            {
                var query = _unitOfWork.GetGenresFromAllDbs().Get();
                return Mapper.Map<List<GenreOutput>>(query);
            }
            catch (Exception exception)
            {
                _logger.Trace(exception.StackTrace);
                throw;
            }
        }

        public GenreOutput Get(int id)
        {
            try
            {
                var genre = new GetDeleteGenreInput { Id = id };
                var validationResults = _getDeleteGenreValidator.Validate(genre, "Id");

                if (!validationResults.IsValid)
                {
                    throw new ValidationException(validationResults.Errors);
                }

                var query = _unitOfWork.GetGenresFromAllDbs().Get(id);
                return Mapper.Map<GenreOutput>(query);
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
    }
}
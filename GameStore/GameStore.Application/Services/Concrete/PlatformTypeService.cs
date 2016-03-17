namespace GameStore.BLL.Services.Concrete
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using FluentValidation;

    using GameStore.BLL.Dtos.Concrete.InputDtos.PlatformTypeInput;
    using GameStore.BLL.Dtos.Concrete.OutputDtos;
    using GameStore.BLL.Services.Abstract;
    using GameStore.DAL.Infrastructure.UnitOfWork.Abstract;

    using NLog;

    public class PlatformTypeService : IPlatformTypeService
    {
        private readonly IValidator<GetDeletePlatformTypeInput> _getDeletePlatformTypeValidator;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        public PlatformTypeService(IUnitOfWork unitOfWork, IValidatorFactory validatorFactory, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _getDeletePlatformTypeValidator = validatorFactory.GetValidator<GetDeletePlatformTypeInput>();
        }

        public IEnumerable<PlatformTypeOutput> Get()
        {
            try
            {
                var query = _unitOfWork.GetPlatformTypes().Get();
                return Mapper.Map<List<PlatformTypeOutput>>(query);
            }
            catch (Exception exception)
            {
                _logger.Trace(exception.StackTrace);
                throw;
            }
        }

        public PlatformTypeOutput Get(int id)
        {
            try
            {
                var platformType = new GetDeletePlatformTypeInput { Id = id };
                var validationResults = _getDeletePlatformTypeValidator.Validate(platformType, "Id");

                if (!validationResults.IsValid)
                {
                    throw new ValidationException(validationResults.Errors);
                }

                var query = _unitOfWork.GetPlatformTypes().Get(id);
                return Mapper.Map<PlatformTypeOutput>(query);
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
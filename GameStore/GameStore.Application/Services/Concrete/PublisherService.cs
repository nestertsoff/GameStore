namespace GameStore.BLL.Services.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using FluentValidation;

    using GameStore.BLL.Dtos.Concrete.InputDtos.PublisherInput;
    using GameStore.BLL.Dtos.Concrete.OutputDtos;
    using GameStore.BLL.Services.Abstract;
    using GameStore.DAL.GameStore.Entities.Concrete;
    using GameStore.DAL.Infrastructure.UnitOfWork.Abstract;

    using NLog;

    public class PublisherService : IPublisherService
    {
        private readonly IValidator<CreateUpdatePublisherInput> _createUpdatePublisherValidator;

        private readonly IValidator<GetDeletePublisherInput> _getDeletePublisherValidator;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        public PublisherService(IUnitOfWork unitOfWork, IValidatorFactory validatorFactory, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _createUpdatePublisherValidator = validatorFactory.GetValidator<CreateUpdatePublisherInput>();
            _getDeletePublisherValidator = validatorFactory.GetValidator<GetDeletePublisherInput>();
        }

        public IEnumerable<PublisherOutput> Get()
        {
            try
            {
                var query = _unitOfWork.GetPublishers().Get();
                return Mapper.Map<List<PublisherOutput>>(query);
            }
            catch (Exception exception)
            {
                _logger.Trace(exception.StackTrace);
                throw;
            }
        }

        public PublisherOutput Get(int id)
        {
            try
            {
                var publisher = new GetDeletePublisherInput { Id = id };
                var validationResults = _getDeletePublisherValidator.Validate(publisher, "Id");

                if (!validationResults.IsValid)
                {
                    throw new ValidationException(validationResults.Errors);
                }

                var query = _unitOfWork.GetPublishers().Get(id);
                return Mapper.Map<PublisherOutput>(query);
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

        public PublisherOutput GetByCompanyName(string companyName)
        {
            try
            {
                var publisher = new GetDeletePublisherInput { CompanyName = companyName };
                var validationResults = _getDeletePublisherValidator.Validate(publisher, "CompanyName");

                if (!validationResults.IsValid)
                {
                    throw new ValidationException(validationResults.Errors);
                }

                var query = _unitOfWork.GetPublishers().Find(_ => _.CompanyName == companyName).FirstOrDefault();
                return Mapper.Map<PublisherOutput>(query);
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

        public void Create(CreateUpdatePublisherInput item)
        {
            try
            {
                var validationResults = _createUpdatePublisherValidator.Validate(item, ruleSet: "Create");

                if (!validationResults.IsValid)
                {
                    throw new ValidationException(validationResults.Errors);
                }

                _unitOfWork.GetPublishers().Create(Mapper.Map<Publisher>(item));
                _unitOfWork.Save();
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

        public void Update(CreateUpdatePublisherInput item)
        {
            try
            {
                var validationResults = _createUpdatePublisherValidator.Validate(item, ruleSet: "Update");

                if (!validationResults.IsValid)
                {
                    throw new ValidationException(validationResults.Errors);
                }

                _unitOfWork.GetPublishers().Update(Mapper.Map<Publisher>(item));
                _unitOfWork.Save();
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

        public void Delete(int id)
        {
            try
            {
                var publisher = new GetDeletePublisherInput { Id = id };
                var validationResults = _getDeletePublisherValidator.Validate(publisher, "Id");

                if (!validationResults.IsValid)
                {
                    throw new ValidationException(validationResults.Errors);
                }

                _unitOfWork.GetPublishers().Delete(id);
                _unitOfWork.Save();
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
    }
}
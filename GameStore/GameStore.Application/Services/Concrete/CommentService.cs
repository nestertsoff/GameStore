namespace GameStore.BLL.Services.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using FluentValidation;

    using GameStore.BLL.Dtos.Concrete.InputDtos.CommentInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.GameInput;
    using GameStore.BLL.Dtos.Concrete.OutputDtos;
    using GameStore.BLL.Services.Abstract;
    using GameStore.DAL.GameStore.Entities.Concrete;
    using GameStore.DAL.Infrastructure.UnitOfWork.Abstract;

    using NLog;

    public class CommentService : ICommentService
    {
        private readonly IValidator<CreateUpdateCommentInput> _createUpdateCommentValidator;

        private readonly IValidator<GetDeleteCommentInput> _getDeleteCommentValidator;

        private readonly IValidator<GetDeleteGameInput> _getDeleteGameValidator;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork, IValidatorFactory validatorFactory, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _createUpdateCommentValidator = validatorFactory.GetValidator<CreateUpdateCommentInput>();
            _getDeleteCommentValidator = validatorFactory.GetValidator<GetDeleteCommentInput>();
            _getDeleteGameValidator = validatorFactory.GetValidator<GetDeleteGameInput>();
        }

        public virtual void Create(CreateUpdateCommentInput item)
        {
            try
            {
                var validationResults = _createUpdateCommentValidator.Validate(item, ruleSet: "Create");

                if (!validationResults.IsValid)
                {
                    throw new ValidationException(validationResults.Errors);
                }

                _unitOfWork.GetComments().Create(Mapper.Map<Comment>(item));
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

        public virtual void Delete(int id)
        {
            try
            {
                var comment = new GetDeleteCommentInput { Id = id };
                var results = _getDeleteCommentValidator.Validate(comment, "Id");

                if (!results.IsValid)
                {
                    throw new ValidationException(results.Errors);
                }

                _unitOfWork.GetComments().Delete(id);
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

        public IEnumerable<CommentOutput> Get()
        {
            try
            {
                var query = _unitOfWork.GetComments().Get();
                return Mapper.Map<List<CommentOutput>>(query);
            }
            catch (Exception exception)
            {
                _logger.Trace(exception.StackTrace);
                throw;
            }
        }

        public CommentOutput Get(int id)
        {
            try
            {
                var comment = new GetDeleteCommentInput { Id = id };
                var validationResults = _getDeleteCommentValidator.Validate(comment, "Id");

                if (!validationResults.IsValid)
                {
                    throw new ValidationException(validationResults.Errors);
                }

                var query = _unitOfWork.GetComments().Get(id);
                return Mapper.Map<CommentOutput>(query);
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

        public IEnumerable<CommentOutput> GetByGameKey(string key)
        {
            try
            {
                var game = new GetDeleteGameInput { Key = key };
                var validationResults = _getDeleteGameValidator.Validate(game, "Key");

                if (!validationResults.IsValid)
                {
                    throw new ValidationException(validationResults.Errors);
                }

                var query = _unitOfWork.GetComments().Find(_ => _.Game.Key == key).ToList();
                return Mapper.Map<List<CommentOutput>>(query);
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

        public virtual void Update(CreateUpdateCommentInput item)
        {
            try
            {
                var validationResults = _createUpdateCommentValidator.Validate(item, ruleSet: "Update");

                if (!validationResults.IsValid)
                {
                    throw new ValidationException(validationResults.Errors);
                }

                _unitOfWork.GetComments().Update(Mapper.Map<Comment>(item));
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
    }
}
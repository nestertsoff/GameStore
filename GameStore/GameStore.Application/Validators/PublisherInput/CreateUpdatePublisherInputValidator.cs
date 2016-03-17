namespace GameStore.BLL.Validators.PublisherInput
{
    using System.Linq;

    using FluentValidation;

    using GameStore.BLL.Dtos.Concrete.InputDtos.PublisherInput;
    using GameStore.DAL.Infrastructure.UnitOfWork.Abstract;

    public class CreateUpdatePublisherInputValidator : AbstractValidator<CreateUpdatePublisherInput>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUpdatePublisherInputValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleSet(
                "Create",
                () =>
                    {
                        RuleFor(_ => _.Id).NotEmpty().Must(BeUniqueId);
                        RuleFor(_ => _.CompanyName).NotEmpty().Must(BeUniqueCompanyName);
                    });
            RuleSet(
                "Update",
                () =>
                    {
                        RuleFor(_ => _.Id).NotEmpty().Must(BeExistentId);
                        RuleFor(_ => _.CompanyName).NotEmpty().Must(BeExistentCompanyName);
                    });
        }

        private bool BeExistentId(int id)
        {
            return _unitOfWork.GetPublishers().Get().SingleOrDefault(_ => _.Id == id) != null;
        }

        private bool BeExistentCompanyName(string companyName)
        {
            return _unitOfWork.GetPublishers().Get().SingleOrDefault(_ => _.CompanyName == companyName) != null;
        }

        private bool BeUniqueId(int id)
        {
            return _unitOfWork.GetPublishers().Get().SingleOrDefault(_ => _.Id == id) == null;
        }

        private bool BeUniqueCompanyName(string companyName)
        {
            return _unitOfWork.GetPublishers().Get().SingleOrDefault(_ => _.CompanyName == companyName) == null;
        }
    }
}
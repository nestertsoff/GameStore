namespace GameStore.BLL.Validators.PlatformTypeInput
{
    using System.Linq;

    using FluentValidation;

    using GameStore.BLL.Dtos.Concrete.InputDtos.PlatformTypeInput;
    using GameStore.DAL.Infrastructure.UnitOfWork.Abstract;

    public class CreateUpdatePlatformTypeInputValidator : AbstractValidator<CreateUpdatePlatformTypeInput>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUpdatePlatformTypeInputValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleSet(
                "Create",
                () =>
                    {
                        RuleFor(_ => _.Id).NotEmpty().Must(BeUniqueId);
                        RuleFor(_ => _.Type).NotEmpty().Must(BeUniquePlatformType);
                    });

            RuleSet(
                "Update",
                () =>
                    {
                        RuleFor(_ => _.Id).NotEmpty().Must(BeExistentId);
                        RuleFor(_ => _.Type).NotEmpty().Must(BeExistentPlatformType);
                    });
        }

        private bool BeExistentId(int id)
        {
            return _unitOfWork.GetPlatformTypes().Get().SingleOrDefault(_ => _.Id == id) != null;
        }

        private bool BeExistentPlatformType(string type)
        {
            return _unitOfWork.GetPlatformTypes().Get().SingleOrDefault(_ => _.Type == type) != null;
        }

        private bool BeUniqueId(int id)
        {
            return _unitOfWork.GetPlatformTypes().Get().SingleOrDefault(_ => _.Id == id) == null;
        }

        private bool BeUniquePlatformType(string type)
        {
            return _unitOfWork.GetPlatformTypes().Get().SingleOrDefault(_ => _.Type == type) == null;
        }
    }
}
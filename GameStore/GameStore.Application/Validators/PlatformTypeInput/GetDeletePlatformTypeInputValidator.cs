namespace GameStore.BLL.Validators.PlatformTypeInput
{
    using System.Linq;

    using FluentValidation;

    using GameStore.BLL.Dtos.Concrete.InputDtos.PlatformTypeInput;
    using GameStore.DAL.Infrastructure.UnitOfWork.Abstract;

    public class GetDeletePlatformTypeInputValidator : AbstractValidator<GetDeletePlatformTypeInput>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDeletePlatformTypeInputValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(_ => _.Id).NotEmpty().Must(BeExistentId);
            RuleFor(_ => _.Type).NotEmpty().Must(BeExistentType);
        }

        private bool BeExistentId(int id)
        {
            return _unitOfWork.GetPlatformTypes().Get().SingleOrDefault(_ => _.Id == id) != null;
        }

        private bool BeExistentType(string type)
        {
            return _unitOfWork.GetPlatformTypes().Get().SingleOrDefault(_ => _.Type == type) != null;
        }
    }
}
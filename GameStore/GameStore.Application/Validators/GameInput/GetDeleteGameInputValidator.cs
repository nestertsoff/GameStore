namespace GameStore.BLL.Validators.GameInput
{
    using System.Linq;

    using FluentValidation;

    using GameStore.BLL.Dtos.Concrete.InputDtos.GameInput;
    using GameStore.DAL.Infrastructure.UnitOfWork.Abstract;

    public class GetDeleteGameInputValidator : AbstractValidator<GetDeleteGameInput>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDeleteGameInputValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(_ => _.Id).NotEmpty().Must(BeExistentId);
            RuleFor(_ => _.Key).NotEmpty().Must(BeExistentKey);
        }

        private bool BeExistentId(int id)
        {
            return _unitOfWork.GetGamesFromAllDbs().Get().SingleOrDefault(_ => _.Id == id) != null;
        }

        private bool BeExistentKey(string key)
        {
            return _unitOfWork.GetGamesFromAllDbs().Get().SingleOrDefault(_ => _.Key == key) != null;
        }
    }
}
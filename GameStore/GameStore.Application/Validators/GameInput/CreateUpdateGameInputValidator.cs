namespace GameStore.BLL.Validators.GameInput
{
    using System.Linq;

    using FluentValidation;

    using GameStore.BLL.Dtos.Concrete.InputDtos.GameInput;
    using GameStore.DAL.Infrastructure.UnitOfWork.Abstract;

    using Ninject;

    public class CreateUpdateGameInputValidator : AbstractValidator<CreateUpdateGameInput>
    {
        public CreateUpdateGameInputValidator()
        {
            RuleSet(
                "Default",
                () =>
                    {
                        RuleFor(_ => _.Key).NotEmpty().Length(2, 100);

                        RuleFor(_ => _.Name).NotEmpty().Length(2, 150);

                        RuleFor(_ => _.NameRu).NotEmpty().Length(2, 150);

                        RuleFor(_ => _.Description).NotEmpty().Length(20, 1500);

                        RuleFor(_ => _.DescriptionRu).NotEmpty().Length(20, 1500);

                        RuleFor(_ => _.Price).InclusiveBetween(0, decimal.MaxValue);

                        RuleFor(_ => _.UnitsInStock).InclusiveBetween((short)0, short.MaxValue);

                        RuleFor(_ => _.UnitsInStock).InclusiveBetween((short)0, short.MaxValue);
                    });

            RuleSet("Create", () => { RuleFor(_ => _.Key).NotEmpty().Must(BeUniqueKey); });

            RuleSet("Update", () => { RuleFor(_ => _.Id).NotEmpty().Must(BeExistentId); });

            RuleFor(_ => _.Name).NotEmpty().Length(2, 100);
        }

        [Inject]
        public IUnitOfWork _unitOfWork { get; set; }

        private bool BeUniqueKey(string key)
        {
            return _unitOfWork.GetGamesFromAllDbs().Get().SingleOrDefault(_ => _.Key == key) == null;
        }

        private bool BeExistentId(int id)
        {
            return _unitOfWork.GetGamesFromAllDbs().Get().SingleOrDefault(_ => _.Id == id) != null;
        }
    }
}
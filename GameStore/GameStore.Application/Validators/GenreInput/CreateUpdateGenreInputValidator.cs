namespace GameStore.BLL.Validators.GenreInput
{
    using System.Linq;

    using FluentValidation;

    using GameStore.BLL.Dtos.Concrete.InputDtos.GenreInput;
    using GameStore.DAL.Infrastructure.UnitOfWork.Abstract;

    public class CreateUpdateGenreInputValidator : AbstractValidator<CreateUpdateGenreInput>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUpdateGenreInputValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleSet(
                "Create",
                () =>
                    {
                        RuleFor(_ => _.Id).NotEmpty().Must(BeUniqueId);
                        RuleFor(_ => _.Name).NotEmpty().Must(BeUniqueName);
                    });

            RuleSet("Update", () => { RuleFor(_ => _.Id).NotEmpty().Must(BeExistentId); });

            RuleFor(_ => _.Name).NotEmpty().Length(2, 100).Must(BeUniqueName);
        }

        private bool BeExistentId(int id)
        {
            return _unitOfWork.GetGamesFromAllDbs().Get().SingleOrDefault(_ => _.Id == id) != null;
        }

        private bool BeUniqueId(int id)
        {
            return _unitOfWork.GetGenres().Get().SingleOrDefault(_ => _.Id == id) == null;
        }

        private bool BeUniqueName(string name)
        {
            return _unitOfWork.GetGenres().Get().SingleOrDefault(_ => _.Name == name) == null;
        }
    }
}
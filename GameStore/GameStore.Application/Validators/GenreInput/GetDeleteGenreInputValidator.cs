namespace GameStore.BLL.Validators.GenreInput
{
    using System.Linq;

    using FluentValidation;

    using GameStore.BLL.Dtos.Concrete.InputDtos.GenreInput;
    using GameStore.DAL.Infrastructure.UnitOfWork.Abstract;

    public class GetDeleteGenreInputValidator : AbstractValidator<GetDeleteGenreInput>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDeleteGenreInputValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(_ => _.Id).NotEmpty().Must(BeExistentId);
            RuleFor(_ => _.Name).NotEmpty().Length(2, 100).Must(BeExistentName);
        }

        private bool BeExistentId(int id)
        {
            return _unitOfWork.GetGenres().Get().SingleOrDefault(_ => _.Id == id) != null;
        }

        private bool BeExistentName(string name)
        {
            return _unitOfWork.GetGenres().Get().SingleOrDefault(_ => _.Name == name) != null;
        }
    }
}
namespace GameStore.Web.Areas.Common.Validators
{
    using System.Linq;

    using FluentValidation;

    using GameStore.BLL.Services.Abstract;
    using GameStore.Web.Areas.Common.Models;
    using GameStore.Web.Resouces;

    public class GameViewModelValidator : AbstractValidator<GameViewModel>
    {
        private readonly IGameService _gameService;

        public GameViewModelValidator(IGameService gameService)
        {
            _gameService = gameService;

            RuleFor(_ => _.Key)
                .NotEmpty()
                .WithLocalizedMessage(() => ValidationResource.NotEmpty)
                .Length(2, 100)
                .WithLocalizedMessage(() => ValidationResource.Length);

            RuleFor(_ => _.Name)
                .NotEmpty()
                .WithLocalizedMessage(() => ValidationResource.NotEmpty)
                .Length(2, 150)
                .WithLocalizedMessage(() => ValidationResource.Length);

            RuleFor(_ => _.NameRu)
                .NotEmpty()
                .WithLocalizedMessage(() => ValidationResource.NotEmpty)
                .Length(2, 150)
                .WithLocalizedMessage(() => ValidationResource.Length);

            RuleFor(_ => _.Description)
                .NotEmpty()
                .WithLocalizedMessage(() => ValidationResource.NotEmpty)
                .Length(20, 1500)
                .WithLocalizedMessage(() => ValidationResource.Length);

            RuleFor(_ => _.DescriptionRu)
                .NotEmpty()
                .WithLocalizedMessage(() => ValidationResource.NotEmpty)
                .Length(20, 1500)
                .WithLocalizedMessage(() => ValidationResource.Length);

            RuleFor(_ => _.Price)
                .NotEmpty()
                .WithLocalizedMessage(() => ValidationResource.NotEmpty)
                .InclusiveBetween(0, decimal.MaxValue)
                .WithLocalizedMessage(() => ValidationResource.GreaterThanOrEqualTo);

            RuleFor(_ => _.UnitsInStock)
                .NotEmpty()
                .WithLocalizedMessage(() => ValidationResource.NotEmpty)
                .InclusiveBetween(0, short.MaxValue)
                .WithLocalizedMessage(() => ValidationResource.GreaterThanOrEqualTo);

            RuleFor(_ => _.PublisherId).NotEmpty().WithLocalizedMessage(() => ValidationResource.NotEmpty);

            RuleFor(_ => _.GenreIds).NotEmpty().WithLocalizedMessage(() => ValidationResource.NotEmpty);

            RuleFor(_ => _.PlatformTypeIds).NotEmpty().WithLocalizedMessage(() => ValidationResource.NotEmpty);

            RuleSet(
                "Create",
                () =>
                    {
                        RuleFor(_ => _.Key)
                            .Must(BeUniqueKey)
                            .WithLocalizedMessage(() => ValidationResource.Unique);
                    });

            RuleSet(
                "Update",
                () =>
                    {
                        RuleFor(_ => _.Id)
                            .Must(BeExistentId)
                            .WithLocalizedMessage(() => ValidationResource.Existent);
                        RuleFor(_ => _.Key)
                            .Must(BeExistentKey)
                            .WithLocalizedMessage(() => ValidationResource.Existent);
                    });

            RuleSet(
                "Delete",
                () =>
                    {
                        RuleFor(_ => _.Key)
                            .Must(BeExistentKey)
                            .WithLocalizedMessage(() => ValidationResource.Existent);
                    });
        }

        private bool BeExistentId(int id)
        {
            return _gameService.Get().SingleOrDefault(_ => _.Id == id) != null;
        }

        private bool BeExistentKey(string key)
        {
            return _gameService.Get().SingleOrDefault(_ => _.Key == key) != null;
        }

        private bool BeUniqueKey(string key)
        {
            return _gameService.Get().SingleOrDefault(_ => _.Key == key) == null;
        }
    }
}
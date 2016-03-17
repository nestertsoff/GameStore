namespace GameStore.IoC.Modules
{
    using FluentValidation;

    using GameStore.BLL.Dtos.Concrete.InputDtos.CommentInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.GameInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.GenreInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.PlatformTypeInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.PublisherInput;
    using GameStore.BLL.Infrastructure;
    using GameStore.BLL.Services.Abstract;
    using GameStore.BLL.Services.Concrete;
    using GameStore.BLL.Validators.GameInput;
    using GameStore.BLL.Validators.GenreInput;
    using GameStore.BLL.Validators.GommentInput;
    using GameStore.BLL.Validators.PlatformTypeInput;
    using GameStore.BLL.Validators.PublisherInput;

    using Ninject.Modules;

    public class ApplicationModule : NinjectModule
    {
        public override void Load()
        {
            // binding services
            Bind<IGameService>().To<GameService>();
            Bind<ICommentService>().To<CommentService>();
            Bind<IPublisherService>().To<PublisherService>();
            Bind<IGenreService>().To<GenreService>();
            Bind<IPlatformTypeService>().To<PlatformTypeService>();

            // binding validators
            Bind<IValidatorFactory>().To<NinjectValidatorFactory>();

            Bind<IValidator<CreateUpdateGameInput>>().To<CreateUpdateGameInputValidator>();
            Bind<IValidator<GetDeleteGameInput>>().To<GetDeleteGameInputValidator>();

            Bind<IValidator<CreateUpdateCommentInput>>().To<CreateCommentInputValidator>();
            Bind<IValidator<GetDeleteCommentInput>>().To<GetDeleteCommentInputValidator>();

            Bind<IValidator<CreateUpdateGenreInput>>().To<CreateUpdateGenreInputValidator>();
            Bind<IValidator<GetDeleteGenreInput>>().To<GetDeleteGenreInputValidator>();

            Bind<IValidator<GetDeletePlatformTypeInput>>().To<GetDeletePlatformTypeInputValidator>();

            Bind<IValidator<CreateUpdatePublisherInput>>().To<CreateUpdatePublisherInputValidator>();
            Bind<IValidator<GetDeletePublisherInput>>().To<GetDeletePublisherInputValidator>();
        }
    }
}
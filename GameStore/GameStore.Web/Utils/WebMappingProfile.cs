namespace GameStore.Web.Utils
{
    using System;
    using System.Threading;

    using AutoMapper;

    using GameStore.BLL.Dtos.Concrete.InputDtos.CommentInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.FilterInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.GameInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.GenreInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.PlatformTypeInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.PublisherInput;
    using GameStore.BLL.Dtos.Concrete.OutputDtos;
    using GameStore.Web.Areas.Common.Models;
    using GameStore.Web.Resouces;

    public class WebMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<GameOutput, ShowGameViewModel>()
                .ForMember(_ => _.Name, m => m.ResolveUsing(GetGameName))
                .ForMember(_ => _.Description, m => m.ResolveUsing(GetGameDescription));

            Mapper.CreateMap<GenreOutput, GenreViewModel>().ForMember(_ => _.Name, m => m.ResolveUsing(GetGenreName));

            Mapper.CreateMap<PlatformTypeOutput, PlatformTypeViewModel>()
                .ForMember(_ => _.Type, m => m.ResolveUsing(GetPlatformTypeName));

            Mapper.CreateMap<PublisherOutput, PublisherViewModel>()
                .ForMember(_ => _.Description, m => m.ResolveUsing(GetPublisherDescription));

            Mapper.CreateMap<CommentOutput, CommentViewModel>()
                .ForMember(_ => _.Body, m => m.ResolveUsing(GetCommentBody))
                .ForMember(_ => _.QuoteBody, m => m.ResolveUsing(GetCommentQuoteBody));
            Mapper.CreateMap<GameOutput, GameViewModel>();

            Mapper.CreateMap<GenreOutput, CreateUpdateGenreInput>();
            Mapper.CreateMap<PublisherOutput, CreateUpdatePublisherInput>();
            Mapper.CreateMap<GameOutput, CreateUpdateGameInput>();
            Mapper.CreateMap<CommentOutput, CreateUpdateCommentInput>();

            Mapper.CreateMap<PlatformTypeOutput, CreateUpdatePlatformTypeInput>();

            Mapper.CreateMap<CommentViewModel, CreateUpdateCommentInput>();
            Mapper.CreateMap<CommentViewModel, GetDeleteCommentInput>();

            Mapper.CreateMap<GameViewModel, CreateUpdateGameInput>();

            Mapper.CreateMap<GameViewModel, CreateUpdateCommentInput>();

            Mapper.CreateMap<PublisherViewModel, CreateUpdatePublisherInput>();


            Mapper.CreateMap<GameFilterViewModel, GameFilterInput>();


        }

        private static string GetGameName(GameOutput output)
        {
            switch (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
            {
                case "ru":
                    return output.NameRu ?? output.Name;
                case "en":
                    return output.Name;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static string GetGameDescription(GameOutput output)
        {
            switch (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
            {
                case "ru":
                    return output.DescriptionRu ?? output.Description;
                case "en":
                    return output.Description;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static string GetCommentBody(CommentOutput output)
        {
            return output.IsDeleted ? Resource.DeletedComment : output.Body;
        }

        private static string GetCommentQuoteBody(CommentOutput output)
        {
            return output.HasQuote ? Resource.DeletedComment : output.Body;
        }

        private static string GetGenreName(GenreOutput output)
        {
            switch (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
            {
                case "ru":
                    return output.NameRu ?? output.Name;
                case "en":
                    return output.Name;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static string GetPlatformTypeName(PlatformTypeOutput output)
        {
            switch (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
            {
                case "ru":
                    return output.TypeRu ?? output.Type;
                case "en":
                    return output.Type;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static string GetPublisherDescription(PublisherOutput output)
        {
            switch (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
            {
                case "ru":
                    return output.DescriptionRu ?? output.Description;
                case "en":
                    return output.Description;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
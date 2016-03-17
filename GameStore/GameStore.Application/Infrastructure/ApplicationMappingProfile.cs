namespace GameStore.BLL.Infrastructure
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using GameStore.BLL.Dtos.Concrete.InputDtos.CommentInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.GameInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.GenreInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.PlatformTypeInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.PublisherInput;
    using GameStore.BLL.Dtos.Concrete.OutputDtos;
    using GameStore.DAL.GameStore.Entities.Concrete;
    using GameStore.DAL.Infrastructure.Utils;
    using GameStore.DAL.Northwind;

    public class ApplicationMappingProfile : Profile
    {
        protected override void Configure()
        {
            // input DTO mapping
            Mapper.CreateMap<CreateUpdateGameInput, Game>();

            Mapper.CreateMap<GetDeleteGameInput, Game>();

            Mapper.CreateMap<CreateUpdateCommentInput, Comment>();

            Mapper.CreateMap<GetDeleteCommentInput, Comment>();

            Mapper.CreateMap<CreateUpdateGenreInput, Genre>();

            Mapper.CreateMap<GetDeleteGenreInput, Genre>();

            Mapper.CreateMap<CreateUpdatePlatformTypeInput, PlatformType>();

            Mapper.CreateMap<GetDeletePlatformTypeInput, PlatformType>();

            Mapper.CreateMap<CreateUpdatePublisherInput, Publisher>();

            Mapper.CreateMap<GetDeletePublisherInput, Publisher>();

            Mapper.CreateMap<Product, Game>()
                .ForMember(g => g.Id, m => m.MapFrom(p => DbIdentifier.EncodeId(p.ProductID, DbType.Northwind)))
                .ForMember(g => g.DbId, m => m.UseValue((int)DbType.Northwind))
                .ForMember(g => g.Key, m => m.ResolveUsing(GetProductKey))
                .ForMember(g => g.Name, m => m.MapFrom(p => p.ProductName))
                .ForMember(g => g.Price, m => m.MapFrom(p => p.UnitPrice))
                .ForMember(g => g.UnitsInStock, m => m.MapFrom(p => p.UnitsInStock))
                .ForMember(
                    g => g.PublisherId,
                    m => m.MapFrom(p => DbIdentifier.EncodeId(p.SupplierID.Value, DbType.Northwind)))
                .ForMember(
                    g => g.Genres,
                    m =>
                    m.MapFrom(
                        p =>
                        new List<Genre>
                            {
                                new Genre
                                    {
                                        Id = DbIdentifier.EncodeId(p.CategoryID.Value, DbType.Northwind),
                                        DbId = (int) DbType.Northwind,
                                        Name = p.Category.CategoryName
                                    }
                            }))
                .ForMember(g => g.PlatformTypes, m => m.MapFrom(p => new List<PlatformType>()))
                .ForMember(g => g.Comments, m => m.MapFrom(p => new List<Comment>()))
                .ForMember(g => g.Publisher, m => m.MapFrom(p => Mapper.Map<Supplier, Publisher>(p.Supplier)))
                .ForMember(g => g.PublishDate, m => m.UseValue(DateTime.UtcNow))
                .ForMember(g => g.Description, m => m.UseValue("This game has no description"));

            Mapper.CreateMap<Supplier, Publisher>()
                .ForMember(s => s.CompanyName, m => m.MapFrom(s => s.CompanyName))
                .ForMember(s => s.HomePage, m => m.MapFrom(s => s.HomePage))
                .ForMember(s => s.Id, m => m.MapFrom(s => DbIdentifier.EncodeId(s.SupplierID, DbType.Northwind)))
                .ForMember(s => s.Description, m => m.MapFrom(s => $"Address: {s.Address}"))
                .ForMember(s => s.DbId, m => m.UseValue((int)DbType.Northwind));


            Mapper.CreateMap<Category, Genre>()
                .ForMember(c => c.Name, m => m.MapFrom(s => s.CategoryName))
                .ForMember(c => c.Id, m => m.MapFrom(s => DbIdentifier.EncodeId(s.CategoryID, DbType.Northwind)))
                .ForMember(c => c.DbId, m => m.UseValue((int)DbType.Northwind));

            // output DTO mapping
            Mapper.CreateMap<Comment, CommentOutput>();

            Mapper.CreateMap<Game, GameOutput>();

            Mapper.CreateMap<Genre, GenreOutput>();

            Mapper.CreateMap<PlatformType, PlatformTypeOutput>();

            Mapper.CreateMap<Publisher, PublisherOutput>();
        }

        private static string GetProductKey(Product product)
        {
            return product.ProductName.ToLower().Replace(" ", "_");
        }
    }
}
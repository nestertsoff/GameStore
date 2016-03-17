namespace GameStore.Tests.ApplicationTests
{
    using System;
    using System.Linq;

    using AutoMapper;

    using FluentValidation;

    using GameStore.BLL.Dtos.Concrete.InputDtos.GameInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.GenreInput;
    using GameStore.BLL.Infrastructure;
    using GameStore.BLL.Services.Concrete;
    using GameStore.BLL.Validators.GameInput;
    using GameStore.BLL.Validators.GenreInput;
    using GameStore.DAL.GameStore.Entities.Concrete;
    using GameStore.DAL.GameStore.Repositories.Abstract;
    using GameStore.DAL.Infrastructure.UnitOfWork.Abstract;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using NLog;

    [TestClass]
    public class GenreServiceTests
    {
        private Mock<IGameRepository> _gameRepositoryMock;

        private Mock<ILogger> _loggerMock;

        private Mock<IGenreRepository> _genreRepositoryMock;

        private GenreService _genreService;

        private Mock<IUnitOfWork> _unitOfWorkMock;

        private Mock<IValidatorFactory> _validationFactoryMock;

        private Game[] games;

        private Genre[] genres;

        [ClassInitialize]
        public static void Initializer(TestContext testContext)
        {
            Mapper.Initialize(_ => _.AddProfile(new ApplicationMappingProfile()));
        }

        [TestInitialize]
        public void TestInitializer()
        {
            #region initialization entities

            genres = new[]
                                     {
                                         new Genre { Id = 1, Name = "Name1" }, 
                                         new Genre { Id = 2, Name = "Name2" }, 
                                         new Genre { Id = 3, Name = "Name3" }, 
                                         new Genre { Id = 4, Name = "Name4" }
                                     };

            games = new[]
                             {
                                 new Game
                                     {
                                         Id = 1, 
                                         Name = "Game1", 
                                         Description = "Description1", 
                                         Key = "game_1", 
                                         Genres = new[] { genres[0], genres[1] }
                                     }, 
                                 new Game
                                     {
                                         Id = 2, 
                                         Name = "Game2", 
                                         Description = "Description2", 
                                         Key = "game_2", 
                                         Genres = new[] { genres[2] }
                                     }, 
                                 new Game
                                     {
                                         Id = 3, 
                                         Name = "Game3", 
                                         Description = "Description3", 
                                         Key = "game_3", 
                                         Genres = new[] { genres[3] }
                                     }
                             };

            #endregion

            #region initialization repositories, unit of work, logger, validation factory and game service

            _gameRepositoryMock = new Mock<IGameRepository>();
            _gameRepositoryMock.Setup(_ => _.Get()).Returns(games);
            _gameRepositoryMock.Setup(_ => _.Get(It.IsAny<int>()))
                .Returns((int id) => games.FirstOrDefault(_ => _.Id == id));
            _gameRepositoryMock.Setup(_ => _.Find(It.IsAny<Func<Game, bool>>()))
                .Returns((Func<Game, bool> predicate) => games.Where(predicate));

            _genreRepositoryMock = new Mock<IGenreRepository>();
            _genreRepositoryMock.Setup(_ => _.Get()).Returns(genres);
            _genreRepositoryMock.Setup(_ => _.Get(It.IsAny<int>()))
                .Returns((int id) => genres.FirstOrDefault(_ => _.Id == id));
            _genreRepositoryMock.Setup(_ => _.Find(It.IsAny<Func<Genre, bool>>()))
                .Returns((Func<Genre, bool> predicate) => genres.Where(predicate));

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(_ => _.GetGames()).Returns(_gameRepositoryMock.Object);
            _unitOfWorkMock.Setup(_ => _.GetGenres()).Returns(_genreRepositoryMock.Object);

            _validationFactoryMock = new Mock<IValidatorFactory>();

            _validationFactoryMock.Setup(_ => _.GetValidator<CreateUpdateGameInput>())
                .Returns(new CreateUpdateGameInputValidator());

            _validationFactoryMock.Setup(_ => _.GetValidator<GetDeleteGameInput>())
                .Returns(new GetDeleteGameInputValidator(_unitOfWorkMock.Object));

            _validationFactoryMock.Setup(_ => _.GetValidator<CreateUpdateGenreInput>())
                .Returns(new CreateUpdateGenreInputValidator(_unitOfWorkMock.Object));

            _validationFactoryMock.Setup(_ => _.GetValidator<GetDeleteGenreInput>())
                .Returns(new GetDeleteGenreInputValidator(_unitOfWorkMock.Object));

            _loggerMock = new Mock<ILogger>();

            _genreService = new GenreService(
                _unitOfWorkMock.Object,
                _validationFactoryMock.Object,
                _loggerMock.Object);

            #endregion
        }

        [TestMethod]
        public void Get_Genres()
        {
            // Act
            var testGenres = _genreService.Get().ToList();

            // Assert
            Assert.AreEqual(4, testGenres.Count);
            Assert.AreEqual("Name1", testGenres.First().Name);
        }

        [TestMethod]
        public void Get_Genre_With_Right_Id()
        {
            // Arrange
            var id = genres[1].Id;

            // Act
            var testGenre = _genreService.Get(id);

            // Assert
            Assert.AreEqual("Name2", testGenre.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Get_Genre_With_Wrong_Id()
        {
            // Arrange
            const int Id = 56;

            // Act
            _genreService.Get(Id);
        }
    }
}
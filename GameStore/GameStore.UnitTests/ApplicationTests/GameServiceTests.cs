namespace GameStore.Tests.ApplicationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using AutoMapper;

    using FluentValidation;

    using GameStore.BLL.Dtos.Concrete.InputDtos.GameInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.GenreInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.PlatformTypeInput;
    using GameStore.BLL.Infrastructure;
    using GameStore.BLL.Services.Concrete;
    using GameStore.BLL.Validators.GameInput;
    using GameStore.BLL.Validators.GenreInput;
    using GameStore.BLL.Validators.PlatformTypeInput;
    using GameStore.DAL.GameStore.Entities.Concrete;
    using GameStore.DAL.GameStore.Repositories.Abstract;
    using GameStore.DAL.Infrastructure.UnitOfWork.Abstract;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using NLog;

    using ValidationException = FluentValidation.ValidationException;

    [TestClass]
    public class GameServiceTests
    {
        private Mock<IGameRepository> _gameRepositoryMock;

        private GameService _gameService;

        private Mock<IGenreRepository> _genreRepositoryMock;

        private Mock<ILogger> _loggerMock;

        private Mock<IPlatformTypeRepository> _platformTypeRepositoryMock;

        private List<GetDeleteGenreInput> _rightGenreInputs;

        private List<GetDeletePlatformTypeInput> _rightPlatformTypeInputs;

        private Mock<IUnitOfWork> _unitOfWorkMock;

        private Mock<IValidatorFactory> _validationFactoryMock;

        private List<GetDeleteGenreInput> _wrongGenreInputs;

        private List<GetDeletePlatformTypeInput> _wrongPlatformTypeInputs;

        private Game[] games;

        private Genre[] genres;

        private PlatformType[] platformTypes;

        private Publisher[] publishers;

        [ClassInitialize]
        public static void Initializer(TestContext testContext)
        {
            Mapper.Initialize(_ => _.AddProfile(new ApplicationMappingProfile()));
        }

        [TestInitialize]
        public void TestInitializer()
        {
            #region initialization entities

            const int GenreId1 = 1;
            const int GenreId2 = 2;
            const int GenreId3 = 3;
            const int GenreId4 = 4;

            genres = new[]
                              {
                                  new Genre { Id = GenreId1, Name = "Genre1" }, 
                                  new Genre { Id = GenreId2, Name = "Genre2" }, 
                                  new Genre { Id = GenreId3, Name = "SubGenre1", ParentGenreId = GenreId2 }, 
                                  new Genre { Id = GenreId4, Name = "SubSubGenre1", ParentGenreId = GenreId3 }
                              };

            platformTypes = new[]
                                     {
                                         new PlatformType { Id = 1, Type = "Platform1" }, 
                                         new PlatformType { Id = 2, Type = "Platform2" }, 
                                         new PlatformType { Id = 3, Type = "Platform3" }, 
                                         new PlatformType { Id = 4, Type = "Platform4" }
                                     };

            publishers = new[]
                                  {
                                      new Publisher
                                          {
                                              Id = 1, 
                                              CompanyName = "Company1", 
                                              Description = "Description1", 
                                              HomePage = "page.com"
                                          }
                                  };

            games = new[]
                             {
                                 new Game
                                     {
                                         Id = 1, 
                                         Name = "Game1", 
                                         Description = "Description1", 
                                         Key = "game_1", 
                                         Genres = new[] { genres[0], genres[2] }, 
                                         PlatformTypes = new[] { platformTypes[0], platformTypes[2] }, 
                                         Publisher = publishers[0]
                                     }, 
                                 new Game
                                     {
                                         Id = 2, 
                                         Name = "Game2", 
                                         Description = "Description2", 
                                         Key = "game_2", 
                                         Genres = new[] { genres[2] }, 
                                         PlatformTypes = new[] { platformTypes[1] }, 
                                         Publisher = publishers[0]
                                     }, 
                                 new Game
                                     {
                                         Id = 3, 
                                         Name = "Game3", 
                                         Description = "Description3", 
                                         Key = "game_3", 
                                         Genres = new[] { genres[0], genres[1] }, 
                                         PlatformTypes = new[] { platformTypes[0], platformTypes[2] }, 
                                         Publisher = publishers[0]
                                     }
                             };

            _rightPlatformTypeInputs = new List<GetDeletePlatformTypeInput>
                                                {
                                                    new GetDeletePlatformTypeInput
                                                        {
                                                            Id = platformTypes[0].Id, 
                                                            Type = platformTypes[0].Type
                                                        }, 
                                                    new GetDeletePlatformTypeInput
                                                        {
                                                            Id = platformTypes[2].Id, 
                                                            Type = platformTypes[2].Type
                                                        }
                                                };

            _wrongPlatformTypeInputs = new List<GetDeletePlatformTypeInput>
                                                {
                                                    new GetDeletePlatformTypeInput
                                                        {
                                                            Id = 301, 
                                                            Type = "Wrong1"
                                                        }, 
                                                    new GetDeletePlatformTypeInput
                                                        {
                                                            Id = 302, 
                                                            Type = "Wrong2"
                                                        }
                                                };
            _rightGenreInputs = new List<GetDeleteGenreInput>
                                         {
                                             new GetDeleteGenreInput
                                                 {
                                                     Id = genres[0].Id, 
                                                     Name =
                                                         genres[0].Name
                                                 }, 
                                             new GetDeleteGenreInput
                                                 {
                                                     Id = genres[1].Id, 
                                                     Name =
                                                         genres[1].Name
                                                 }
                                         };

            _wrongGenreInputs = new List<GetDeleteGenreInput>
                                         {
                                             new GetDeleteGenreInput
                                                 {
                                                     Id = 501, 
                                                     Name = "Wrong1"
                                                 }, 
                                             new GetDeleteGenreInput
                                                 {
                                                     Id = 502, 
                                                     Name = "Wrong2"
                                                 }, 
                                             new GetDeleteGenreInput
                                                 {
                                                     Id = 503, 
                                                     Name = "Wrong3"
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
            _genreRepositoryMock.Setup(_ => _.Get()).Returns(genres.ToList());
            _genreRepositoryMock.Setup(_ => _.Find(It.IsAny<Func<Genre, bool>>()))
                .Returns((Func<Genre, bool> predicate) => genres.ToList().Where(predicate));

            _platformTypeRepositoryMock = new Mock<IPlatformTypeRepository>();
            _platformTypeRepositoryMock.Setup(_ => _.Get()).Returns(platformTypes);
            _platformTypeRepositoryMock.Setup(_ => _.Find(It.IsAny<Func<PlatformType, bool>>()))
                .Returns(
                    (Expression<Func<PlatformType, bool>> predicate) => platformTypes.Where(predicate.Compile()));

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(_ => _.GetGames()).Returns(_gameRepositoryMock.Object);
            _unitOfWorkMock.Setup(_ => _.GetGenres()).Returns(_genreRepositoryMock.Object);
            _unitOfWorkMock.Setup(_ => _.GetPlatformTypes()).Returns(_platformTypeRepositoryMock.Object);

            _validationFactoryMock = new Mock<IValidatorFactory>();
            _validationFactoryMock.Setup(_ => _.GetValidator<CreateUpdateGameInput>())
                .Returns(new CreateUpdateGameInputValidator());

            // .Returns(new CreateGameInputValidator(this._unitOfWorkMock.Object));
            _validationFactoryMock.Setup(_ => _.GetValidator<GetDeleteGameInput>())
                .Returns(new GetDeleteGameInputValidator(_unitOfWorkMock.Object));

            _validationFactoryMock.Setup(_ => _.GetValidator<CreateUpdateGenreInput>())
                .Returns(new CreateUpdateGenreInputValidator(_unitOfWorkMock.Object));

            _validationFactoryMock.Setup(_ => _.GetValidator<GetDeleteGenreInput>())
                .Returns(new GetDeleteGenreInputValidator(_unitOfWorkMock.Object));

            _validationFactoryMock.Setup(_ => _.GetValidator<GetDeletePlatformTypeInput>())
                .Returns(new GetDeletePlatformTypeInputValidator(_unitOfWorkMock.Object));

            _loggerMock = new Mock<ILogger>();

            _gameService = new GameService(
                _unitOfWorkMock.Object,
                _validationFactoryMock.Object,
                _loggerMock.Object);

            #endregion
        }

        [TestMethod]
        public void Get_Game_With_Right_Id()
        {
            // Arrange
            var id = games[1].Id;

            // Act
            var testGame = _gameService.Get(id);

            // Assert
            Assert.AreEqual("Game2", testGame.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Get_Game_With_Wrong_Id()
        {
            // Arrange
            const int Id = 67;

            // Act
            _gameService.Get(Id);
        }

        [TestMethod]
        public void Get_Game_With_Right_Key()
        {
            // Arrange
            const string Key = "game_2";

            // Act
            var testGame = _gameService.GetByKey(Key);

            // Assert
            Assert.AreEqual(games[1].Id, testGame.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Get_Game_With_Wrong_Key()
        {
            // Arrange
            const string Key = "Wrong_key";

            // Act
            _gameService.GetByKey(Key);
        }

        [TestMethod]
        public void Get_Games_With_Right_Genres()
        {
            // Arrange
            var testGenres = _rightGenreInputs;

            // Act
            var testGames = _gameService.GetByGenres(testGenres);

            // Assert
            Assert.AreEqual(2, testGames.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Get_Games_With_Wrong_Genres()
        {
            // Arrange
            var testGenres = _wrongGenreInputs;

            // Act
            _gameService.GetByGenres(testGenres);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Get_Games_With_Null_Genres()
        {
            // Act
            _gameService.GetByGenres(null);
        }

        [TestMethod]
        public void Get_Games_With_Right_PlatformTypes()
        {
            // Arrange
            var platformList = _rightPlatformTypeInputs;

            // Act
            var testGames = _gameService.GetByPlatformTypes(platformList);

            // Assert
            Assert.AreEqual(2, testGames.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Get_Games_With_Wrong_PlatformTypes()
        {
            // Arrange
            var platformList = _wrongPlatformTypeInputs;

            // Act
            _gameService.GetByPlatformTypes(platformList);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Get_Games_With_Null_PlatformTypes()
        {
            // Act
            _gameService.GetByPlatformTypes(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Delete_Game_With_Wrong_Key()
        {
            // Arrange
            const string WrongKey = "wrong_key";

            // Act
            _gameService.Delete(WrongKey);
        }

        [TestMethod]
        public void Delete_Game_With_Right_Key()
        {
            // Arrange
            var rightKey = games[1].Key;

            // Act
            _gameService.Delete(rightKey);

            // Assert
            _gameRepositoryMock.Verify(_ => _.Delete(It.IsAny<string>()), Times.Once);
        }
    }
}
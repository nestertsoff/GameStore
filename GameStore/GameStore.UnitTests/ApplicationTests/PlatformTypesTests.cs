namespace GameStore.Tests.ApplicationTests
{
    using System;
    using System.Linq;

    using AutoMapper;

    using FluentValidation;

    using GameStore.BLL.Dtos.Concrete.InputDtos.GameInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.PlatformTypeInput;
    using GameStore.BLL.Infrastructure;
    using GameStore.BLL.Services.Concrete;
    using GameStore.BLL.Validators.GameInput;
    using GameStore.BLL.Validators.PlatformTypeInput;
    using GameStore.DAL.GameStore.Entities.Concrete;
    using GameStore.DAL.GameStore.Repositories.Abstract;
    using GameStore.DAL.Infrastructure.UnitOfWork.Abstract;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using NLog;

    [TestClass]
    public class PlatformTypesTests
    {
        private Mock<IGameRepository> _gameRepositoryMock;

        private Mock<ILogger> _loggerMock;

        private Mock<IPlatformTypeRepository> _platformTypeRepositoryMock;

        private PlatformTypeService _platformTypeService;

        private Mock<IUnitOfWork> _unitOfWorkMock;

        private Mock<IValidatorFactory> _validationFactoryMock;

        private Game[] games;

        private PlatformType[] platformTypes;

        [ClassInitialize]
        public static void Initializer(TestContext testContext)
        {
            Mapper.Initialize(_ => _.AddProfile(new ApplicationMappingProfile()));
        }

        [TestInitialize]
        public void TestInitializer()
        {
            #region initialization entities

            platformTypes = new[]
                                     {
                                         new PlatformType { Id = 1, Type = "Type1" },
                                         new PlatformType { Id = 2, Type = "Type2" },
                                         new PlatformType { Id = 3, Type = "Type3" },
                                         new PlatformType { Id = 4, Type = "Type4" }
                                     };

            games = new[]
                             {
                                 new Game
                                     {
                                         Id = 1,
                                         Name = "Game1",
                                         Description = "Description1",
                                         Key = "game_1",
                                         PlatformTypes = new[] { platformTypes[0], platformTypes[1] }
                                     },
                                 new Game
                                     {
                                         Id = 2,
                                         Name = "Game2",
                                         Description = "Description2",
                                         Key = "game_2",
                                         PlatformTypes = new[] { platformTypes[2] }
                                     },
                                 new Game
                                     {
                                         Id = 3,
                                         Name = "Game3",
                                         Description = "Description3",
                                         Key = "game_3",
                                         PlatformTypes = new[] { platformTypes[3] }
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

            _platformTypeRepositoryMock = new Mock<IPlatformTypeRepository>();
            _platformTypeRepositoryMock.Setup(_ => _.Get()).Returns(platformTypes);
            _platformTypeRepositoryMock.Setup(_ => _.Get(It.IsAny<int>()))
                .Returns((int id) => platformTypes.FirstOrDefault(_ => _.Id == id));
            _platformTypeRepositoryMock.Setup(_ => _.Find(It.IsAny<Func<PlatformType, bool>>()))
                .Returns((Func<PlatformType, bool> predicate) => platformTypes.Where(predicate));

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(_ => _.GetGames()).Returns(_gameRepositoryMock.Object);
            _unitOfWorkMock.Setup(_ => _.GetPlatformTypes()).Returns(_platformTypeRepositoryMock.Object);

            _validationFactoryMock = new Mock<IValidatorFactory>();

            _validationFactoryMock.Setup(_ => _.GetValidator<CreateUpdateGameInput>())
                .Returns(new CreateUpdateGameInputValidator());

            _validationFactoryMock.Setup(_ => _.GetValidator<GetDeleteGameInput>())
                .Returns(new GetDeleteGameInputValidator(_unitOfWorkMock.Object));

            _validationFactoryMock.Setup(_ => _.GetValidator<CreateUpdatePlatformTypeInput>())
                .Returns(new CreateUpdatePlatformTypeInputValidator(_unitOfWorkMock.Object));

            _validationFactoryMock.Setup(_ => _.GetValidator<GetDeletePlatformTypeInput>())
                .Returns(new GetDeletePlatformTypeInputValidator(_unitOfWorkMock.Object));

            _loggerMock = new Mock<ILogger>();

            _platformTypeService = new PlatformTypeService(
                _unitOfWorkMock.Object,
                _validationFactoryMock.Object,
                _loggerMock.Object);

            #endregion
        }

        [TestMethod]
        public void Get_PlatformTypes()
        {
            // Act
            var testPlatformTypes = _platformTypeService.Get().ToList();

            // Assert
            Assert.AreEqual(4, testPlatformTypes.Count);
            Assert.AreEqual("Type1", testPlatformTypes.First().Type);
        }

        [TestMethod]
        public void Get_PlatformType_With_Right_Id()
        {
            // Arrange
            var id = platformTypes[1].Id;

            // Act
            var testPlatformType = _platformTypeService.Get(id);

            // Assert
            Assert.AreEqual("Type2", testPlatformType.Type);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Get_PlatformType_With_Wrong_Id()
        {
            // Arrange
            const int Id = 221;

            // Act
            _platformTypeService.Get(Id);
        }
    }
}
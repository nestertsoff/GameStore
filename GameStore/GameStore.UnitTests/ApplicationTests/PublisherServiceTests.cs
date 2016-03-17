namespace GameStore.Tests.ApplicationTests
{
    using System;
    using System.Linq;

    using AutoMapper;

    using FluentValidation;

    using GameStore.BLL.Dtos.Concrete.InputDtos.GameInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.PublisherInput;
    using GameStore.BLL.Infrastructure;
    using GameStore.BLL.Services.Concrete;
    using GameStore.BLL.Validators.GameInput;
    using GameStore.BLL.Validators.PublisherInput;
    using GameStore.DAL.GameStore.Entities.Concrete;
    using GameStore.DAL.GameStore.Repositories.Abstract;
    using GameStore.DAL.Infrastructure.UnitOfWork.Abstract;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using NLog;

    [TestClass]
    public class PublisherServiceTests
    {
        private Mock<IGameRepository> _gameRepositoryMock;

        private Mock<ILogger> _loggerMock;

        private Mock<IPublisherRepository> _publisherRepositoryMock;

        private PublisherService _publisherService;

        private Mock<IUnitOfWork> _unitOfWorkMock;

        private Mock<IValidatorFactory> _validationFactoryMock;

        private Game[] games;

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

            publishers = new[]
                                  {
                                      new Publisher { Id = 1, CompanyName = "CompanyName1" },
                                      new Publisher { Id = 2, CompanyName = "CompanyName2" },
                                      new Publisher { Id = 3, CompanyName = "CompanyName3" },
                                      new Publisher { Id = 4, CompanyName = "CompanyName4" }
                                  };

            games = new[]
                             {
                                 new Game
                                     {
                                         Id = 1,
                                         Name = "Game1",
                                         Description = "Description1",
                                         Key = "game_1",
                                         Publisher = publishers[1]
                                     },
                                 new Game
                                     {
                                         Id = 2,
                                         Name = "Game2",
                                         Description = "Description2",
                                         Key = "game_2",
                                         Publisher = publishers[2]
                                     },
                                 new Game
                                     {
                                         Id = 3,
                                         Name = "Game3",
                                         Description = "Description3",
                                         Key = "game_3",
                                         Publisher = publishers[3]
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

            _publisherRepositoryMock = new Mock<IPublisherRepository>();
            _publisherRepositoryMock.Setup(_ => _.Get()).Returns(publishers);
            _publisherRepositoryMock.Setup(_ => _.Get(It.IsAny<int>()))
                .Returns((int id) => publishers.FirstOrDefault(_ => _.Id == id));
            _publisherRepositoryMock.Setup(_ => _.Find(It.IsAny<Func<Publisher, bool>>()))
                .Returns((Func<Publisher, bool> predicate) => publishers.Where(predicate));

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(_ => _.GetGames()).Returns(_gameRepositoryMock.Object);
            _unitOfWorkMock.Setup(_ => _.GetPublishers()).Returns(_publisherRepositoryMock.Object);

            _validationFactoryMock = new Mock<IValidatorFactory>();

            _validationFactoryMock.Setup(_ => _.GetValidator<CreateUpdateGameInput>())
                .Returns(new CreateUpdateGameInputValidator());

            // .Returns(new CreateGameInputValidator(this._unitOfWorkMock.Object));
            _validationFactoryMock.Setup(_ => _.GetValidator<GetDeleteGameInput>())
                .Returns(new GetDeleteGameInputValidator(_unitOfWorkMock.Object));

            _validationFactoryMock.Setup(_ => _.GetValidator<CreateUpdatePublisherInput>())
                .Returns(new CreateUpdatePublisherInputValidator(_unitOfWorkMock.Object));

            _validationFactoryMock.Setup(_ => _.GetValidator<GetDeletePublisherInput>())
                .Returns(new GetDeletePublisherInputValidator(_unitOfWorkMock.Object));

            _loggerMock = new Mock<ILogger>();

            _publisherService = new PublisherService(
                _unitOfWorkMock.Object,
                _validationFactoryMock.Object,
                _loggerMock.Object);

            #endregion
        }

        [TestMethod]
        public void Get_Publishers()
        {
            // Act
            var testPublishers = _publisherService.Get().ToList();

            // Assert
            Assert.AreEqual(4, testPublishers.Count);
            Assert.AreEqual("CompanyName1", testPublishers.First().CompanyName);
        }

        [TestMethod]
        public void Get_Publisher_With_Right_Id()
        {
            // Arrange
            var id = publishers[1].Id;

            // Act
            var testPublisher = _publisherService.Get(id);

            // Assert
            Assert.AreEqual("CompanyName2", testPublisher.CompanyName);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Get_Publisher_With_Wrong_Id()
        {
            // Arrange
            const int Id = 78;

            // Act
            _publisherService.Get(Id);
        }

        [TestMethod]
        public void Get_Publisher_With_Right_CompanyName()
        {
            // Arrange
            const string CompanyName = "CompanyName2";

            // Act
            var testPublisher = _publisherService.GetByCompanyName(CompanyName);

            // Assert
            Assert.AreEqual("CompanyName2", testPublisher.CompanyName);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Get_Publisher_With_Wrong_CompanyName()
        {
            // Arrange
            const string CompanyName = "Wrong_companyName";

            // Act
            _publisherService.GetByCompanyName(CompanyName);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Get_Publishers_With_Null_CompanyName()
        {
            // Act
            _publisherService.GetByCompanyName(null);
        }

        [TestMethod]
        public void Create_Publisher_With_Right_Data()
        {
            // Arrange
            var testPublisher = new CreateUpdatePublisherInput
                                    {
                                        Id = 67,
                                        CompanyName = "FirstCompany",
                                        Description =
                                            "This is test description for FirstCompany. FirstCompany is a publisher of  many games",
                                        HomePage = "firstcompany.com"
                                    };

            // Act
            _publisherService.Create(testPublisher);

            // Assert
            _publisherRepositoryMock.Verify(_ => _.Create(It.IsAny<Publisher>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Create_Publisher_With_Wrong_Id()
        {
            // Arrange
            var testPublisher = new CreateUpdatePublisherInput { Id = 56, CompanyName = publishers[1].CompanyName };

            // Act
            _publisherService.Create(testPublisher);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Update_Publisher_With_Wrong_Id()
        {
            // Arrange
            var testPublisher = new CreateUpdatePublisherInput { Id = 45, CompanyName = publishers[1].CompanyName };

            // Act
            _publisherService.Update(testPublisher);
        }

        [TestMethod]
        public void Update_Publisher_With_Right_Data()
        {
            // Arrange
            var testPublisher = new CreateUpdatePublisherInput
                                    {
                                        Id = publishers[1].Id,
                                        CompanyName = publishers[1].CompanyName,
                                        Description = publishers[1].Description,
                                        DescriptionRu = publishers[1].DescriptionRu,
                                        HomePage = publishers[1].HomePage
                                    };

            // Act
            _publisherService.Update(testPublisher);

            // Assert
            _publisherRepositoryMock.Verify(_ => _.Update(It.IsAny<Publisher>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Delete_Publisher_With_Wrong_Id()
        {
            // Arrange
            const int WrongId = 89;

            // Act
            _publisherService.Delete(WrongId);
        }

        [TestMethod]
        public void Delete_Publisher_With_Right_Id()
        {
            // Arrange
            var rightId = publishers[1].Id;

            // Act
            _publisherService.Delete(rightId);

            // Assert
            _publisherRepositoryMock.Verify(_ => _.Delete(It.IsAny<int>()), Times.Once);
        }
    }
}
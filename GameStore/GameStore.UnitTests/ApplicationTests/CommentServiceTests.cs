namespace GameStore.Tests.ApplicationTests
{
    using System;
    using System.Linq;

    using AutoMapper;

    using FluentValidation;

    using GameStore.BLL.Dtos.Concrete.InputDtos.CommentInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.GameInput;
    using GameStore.BLL.Infrastructure;
    using GameStore.BLL.Services.Concrete;
    using GameStore.BLL.Validators.GameInput;
    using GameStore.BLL.Validators.GommentInput;
    using GameStore.DAL.GameStore.Entities.Concrete;
    using GameStore.DAL.GameStore.Repositories.Abstract;
    using GameStore.DAL.Infrastructure.UnitOfWork.Abstract;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using NLog;

    [TestClass]
    public class CommentServiceTests
    {
        private Mock<ICommentRepository> _commentRepositoryMock;

        private CommentService _commentService;

        private Mock<IGameRepository> _gameRepositoryMock;

        private Mock<ILogger> _loggerMock;

        private Mock<IUnitOfWork> _unitOfWorkMock;

        private Mock<IValidatorFactory> _validationFactoryMock;

        private Comment[] comments;

        private Game[] games;

        [ClassInitialize]
        public static void Initializer(TestContext testContext)
        {
            Mapper.Initialize(_ => _.AddProfile(new ApplicationMappingProfile()));
        }

        [TestInitialize]
        public void TestInitializer()
        {
            #region initialization entities

            var testGame = new Game { Id = 77, Name = "TestGame", Description = "TestDescription", Key = "test_game" };

            comments = new[]
                                {
                                    new Comment { Id = 1, Body = "Body1", Game = testGame },
                                    new Comment { Id = 2, Body = "Body2", Game = testGame },
                                    new Comment { Id = 3, Body = "Body3", Game = testGame },
                                    new Comment { Id = 4, Body = "Body4", Game = testGame }
                                };

            games = new[]
                             {
                                 new Game
                                     {
                                         Id = 1,
                                         Name = "Game1",
                                         Description = "Description1",
                                         Key = "game_1",
                                         Comments = new[] { comments[0], comments[1] }
                                     },
                                 new Game
                                     {
                                         Id = 2,
                                         Name = "Game2",
                                         Description = "Description2",
                                         Key = "game_2",
                                         Comments = new[] { comments[2] }
                                     },
                                 new Game
                                     {
                                         Id = 3,
                                         Name = "Game3",
                                         Description = "Description3",
                                         Key = "game_3",
                                         Comments = new[] { comments[3] }
                                     },
                                 testGame
                             };

            #endregion

            #region initialization repositories, unit of work, logger, validation factory and game service

            _gameRepositoryMock = new Mock<IGameRepository>();
            _gameRepositoryMock.Setup(_ => _.Get()).Returns(games);
            _gameRepositoryMock.Setup(_ => _.Get(It.IsAny<int>()))
                .Returns((int id) => games.FirstOrDefault(_ => _.Id == id));
            _gameRepositoryMock.Setup(_ => _.Find(It.IsAny<Func<Game, bool>>()))
                .Returns((Func<Game, bool> predicate) => games.Where(predicate));

            _commentRepositoryMock = new Mock<ICommentRepository>();
            _commentRepositoryMock.Setup(_ => _.Get()).Returns(comments);
            _commentRepositoryMock.Setup(_ => _.Get(It.IsAny<int>()))
                .Returns((int id) => comments.FirstOrDefault(_ => _.Id == id));
            _commentRepositoryMock.Setup(_ => _.Find(It.IsAny<Func<Comment, bool>>()))
                .Returns((Func<Comment, bool> predicate) => comments.Where(predicate));

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(_ => _.GetGames()).Returns(_gameRepositoryMock.Object);
            _unitOfWorkMock.Setup(_ => _.GetComments()).Returns(_commentRepositoryMock.Object);

            _validationFactoryMock = new Mock<IValidatorFactory>();

            _validationFactoryMock.Setup(_ => _.GetValidator<CreateUpdateGameInput>())
                .Returns(new CreateUpdateGameInputValidator());

            // .Returns(new CreateGameInputValidator(this._unitOfWorkMock.Object));
            _validationFactoryMock.Setup(_ => _.GetValidator<GetDeleteGameInput>())
                .Returns(new GetDeleteGameInputValidator(_unitOfWorkMock.Object));

            _validationFactoryMock.Setup(_ => _.GetValidator<CreateUpdateCommentInput>())
                .Returns(new CreateCommentInputValidator(_unitOfWorkMock.Object));

            _validationFactoryMock.Setup(_ => _.GetValidator<GetDeleteCommentInput>())
                .Returns(new GetDeleteCommentInputValidator(_unitOfWorkMock.Object));

            _loggerMock = new Mock<ILogger>();

            _commentService = new CommentService(
                _unitOfWorkMock.Object,
                _validationFactoryMock.Object,
                _loggerMock.Object);

            #endregion
        }

        [TestMethod]
        public void Get_Comments()
        {
            // Act
            var testComments = _commentService.Get().ToList();

            // Assert
            Assert.AreEqual(4, testComments.Count);
            Assert.AreEqual("Body1", testComments.First().Body);
        }

        [TestMethod]
        public void Get_Comment_With_Right_Id()
        {
            // Arrange
            var id = comments[1].Id;

            // Act
            var testComment = _commentService.Get(id);

            // Assert
            Assert.AreEqual("Body2", testComment.Body);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Get_Comment_With_Wrong_Id()
        {
            // Arrange
            const int Id = 223;

            // Act
            _commentService.Get(Id);
        }

        [TestMethod]
        public void Get_Comments_With_Right_GameKey()
        {
            // Arrange
            const string Key = "test_game";

            // Act
            var testComments = _commentService.GetByGameKey(Key).ToList();

            // Assert
            Assert.AreEqual(4, testComments.Count);
            Assert.AreEqual("Body1", testComments.First().Body);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Get_Comments_With_Wrong_GameKey()
        {
            // Arrange
            const string Key = "Wrong_key";

            // Act
            _commentService.GetByGameKey(Key);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Get_Comments_With_Null_GameKey()
        {
            // Act
            _commentService.GetByGameKey(null);
        }

        [TestMethod]
        public void Create_Comment_With_Right_Data()
        {
            // Arrange
            var testComment = new CreateUpdateCommentInput
                                  {
                                      Id = 17,
                                      Name = "This is test name",
                                      Body = "This is test body for comment"
                                  };

            // Act
            _commentService.Create(testComment);

            // Assert
            _commentRepositoryMock.Verify(_ => _.Create(It.IsAny<Comment>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Create_Comment_With_Wrong_Id()
        {
            // Arrange
            var testComment = new CreateUpdateCommentInput
                                  {
                                      Id = 245,
                                      Name = comments[1].Name,
                                      Body = comments[1].Body
                                  };

            // Act
            _commentService.Create(testComment);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Update_Comment_With_Wrong_Id()
        {
            // Arrange
            var testPublisher = new CreateUpdateCommentInput
                                    {
                                        Id = 245,
                                        Body = comments[1].Body,
                                        Name = comments[1].Name,
                                        Date = comments[1].Date
                                    };

            // Act
            _commentService.Update(testPublisher);
        }

        [TestMethod]
        public void Update_Comment_With_Right_Data()
        {
            // Arrange
            var testComment = new CreateUpdateCommentInput
                                  {
                                      Id = comments[1].Id,
                                      Body = comments[1].Body,
                                      Name = "NewName",
                                      Date = comments[1].Date
                                  };

            // Act
            _commentService.Update(testComment);

            // Assert
            _commentRepositoryMock.Verify(_ => _.Update(It.IsAny<Comment>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Delete_Comment_With_Wrong_Id()
        {
            // Arrange
            const int WrongId = 234;

            // Act
            _commentService.Delete(WrongId);
        }

        [TestMethod]
        public void Delete_Comment_With_Right_Id()
        {
            // Arrange
            var rightId = comments[1].Id;

            // Act
            _commentService.Delete(rightId);

            // Assert
            _commentRepositoryMock.Verify(_ => _.Delete(It.IsAny<int>()), Times.Once);
        }
    }
}
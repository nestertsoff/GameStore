namespace GameStore.DAL.GameStore.Context.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using global::GameStore.DAL.GameStore.Entities.Concrete;

    public class ContextInitializer : DropCreateDatabaseAlways<GameStoreContext>
    {
        private GameStoreContext _db;

        protected override void Seed(GameStoreContext context)
        {
            _db = context;

            CreateIndex("Key", typeof(Game));
            CreateIndex("Name", typeof(Genre));
            CreateIndex("Type", typeof(PlatformType));
            CreateIndex("CompanyName", typeof(Publisher));

            var subgenges = new List<Genre>
                                {
                                    new Genre { DbId = 1, Name = "RTS", NameRu = "Реального времени" },
                                    new Genre { DbId = 1, Name = "TBS", NameRu = "Пошаговые" },
                                    new Genre { DbId = 1, Name = "Rally", NameRu = "Ралли" },
                                    new Genre { DbId = 1, Name = "Arcade", NameRu = "Аркады" },
                                    new Genre { DbId = 1, Name = "Formula", NameRu = "Формула" },
                                    new Genre { DbId = 1, Name = "Off-road", NameRu = "Внедорожники" },
                                    new Genre { DbId = 1, Name = "FPS", NameRu = "От первого лица" },
                                    new Genre { DbId = 1, Name = "TPS", NameRu = "От третьего лица" },
                                    new Genre
                                        {
                                            DbId = 1,
                                            Name = "Misc",
                                            NameRu = "С минимальным набором команд"
                                        }
                                };

            var genres = new List<Genre>
                             {
                                 new Genre
                                     {
                                         DbId = 1,
                                         Name = "Strategy",
                                         NameRu = "Стратегии",
                                         ChildGenres = new[] { subgenges[0], subgenges[1] }
                                     },
                                 new Genre
                                     {
                                         DbId = 1,
                                         Name = "Races",
                                         NameRu = "Гонки",
                                         ChildGenres =
                                             new[]
                                                 {
                                                     subgenges[2], subgenges[3], subgenges[4],
                                                     subgenges[5]
                                                 }
                                     },
                                 new Genre
                                     {
                                         DbId = 1,
                                         Name = "Action",
                                         NameRu = "Шутер",
                                         ChildGenres =
                                             new[] { subgenges[6], subgenges[7], subgenges[8] }
                                     },
                                 new Genre { DbId = 1, Name = "RPG", NameRu = "Ролевые" },
                                 new Genre { DbId = 1, Name = "Sports", NameRu = "Спортивные" },
                                 new Genre { DbId = 1, Name = "Adventure", NameRu = "Приключения" },
                                 new Genre { DbId = 1, Name = "Puzzle&Skill", NameRu = "Головоломки" }
                             };

            genres.ForEach(_ => context.Genres.Add(_));

            var platforms = new List<PlatformType>
                                {
                                    new PlatformType
                                        {
                                            DbId = 1,
                                            Type = "Mobile",
                                            TypeRu = "Мобильные"
                                        },
                                    new PlatformType
                                        {
                                            DbId = 1,
                                            Type = "Browser",
                                            TypeRu = "Браузерные"
                                        },
                                    new PlatformType
                                        {
                                            DbId = 1,
                                            Type = "Desktop",
                                            TypeRu = "Для ПК"
                                        },
                                    new PlatformType
                                        {
                                            DbId = 1,
                                            Type = "Console",
                                            TypeRu = "Консольные"
                                        }
                                };

            platforms.ForEach(_ => context.PlatformTypes.Add(_));

            var publishers = new List<Publisher>
                                 {
                                     new Publisher
                                         {
                                             DbId = 1,
                                             CompanyName = "First Company",
                                             Description =
                                                 "This is description for First Company. First Company is a publisher of many games.",
                                             DescriptionRu =
                                                 "Это описание для \"Первой Компании\". \"Первая Компания\" - издатель многих игр.",
                                             HomePage = "firstcompany.com"
                                         },
                                     new Publisher
                                         {
                                             DbId = 1,
                                             CompanyName = "Second Company",
                                             Description =
                                                 "This is description for Second Company. Second Company is a publisher of many games.",
                                             DescriptionRu =
                                                 "Это описание для \"Второй Компании\". \"Вторая Компания\" - издатель многих игр.",
                                             HomePage = "secondcompany.com"
                                         },
                                     new Publisher
                                         {
                                             DbId = 1,
                                             CompanyName = "Third Company",
                                             Description =
                                                 "This is description for Third Company. Third Company is a publisher of many games.",
                                             DescriptionRu =
                                                 "Это описание для \"Первой Компании\". \"Первая Компания\" - издатель многих игр.",
                                             HomePage = "thirdcompany.com"
                                         }
                                 };

            publishers.ForEach(_ => context.Publishers.Add(_));

            var random = new Random();

            for (var i = 0; i < 100; i++)
            {
                var randomGenres = new List<Genre>();
                var randomPlatforms = new List<PlatformType>();
                var allGenres = genres.Concat(subgenges).ToList();
                var leftoverGenres = new List<Genre>(allGenres);
                var leftoverPlatforms = new List<PlatformType>(platforms);
                var randomGenresCount = random.Next(1, genres.Count);
                var randomPlatformsCount = random.Next(1, platforms.Count);

                var randomPublisherIndex = random.Next(0, publishers.Count - 1);
                var randomPublisher = publishers[randomPublisherIndex];

                for (var j = 1; j <= randomGenresCount; j++)
                {
                    var randomGenreIndex = random.Next(0, leftoverGenres.Count - 1);
                    var genre = leftoverGenres[randomGenreIndex];
                    randomGenres.Add(genre);
                    leftoverGenres.Remove(genre);
                }

                for (var k = 1; k <= randomPlatformsCount; k++)
                {
                    var randomPlatformIndex = random.Next(0, leftoverPlatforms.Count - 1);
                    var platform = leftoverPlatforms[randomPlatformIndex];
                    randomPlatforms.Add(platform);
                    leftoverPlatforms.Remove(platform);
                }

                context.Games.Add(
                    new Game
                        {
                            Name = $"Game #{i + 1}",
                            NameRu = $"Игра #{i + 1}",
                            Description = $"This is game #{i + 1} of the game store",
                            DescriptionRu = $"Это игра #{i + 1} игрового магазина",
                            Genres = randomGenres,
                            Key = $"game_{i + 1}",
                            PlatformTypes = randomPlatforms,
                            Price = random.Next(0, 10000),
                            UnitsInStock = (short)random.Next(0, 10000),
                            Discontinued = random.NextDouble() > 0.8,
                            Publisher = randomPublisher,
                            PublishDate = DateTime.UtcNow.AddDays(-i)
                        });
            }
        }

        private void CreateIndex(string field, Type table)
        {
            var command = string.Format("CREATE UNIQUE INDEX IX_{0} ON [{1}s]([{0}])", field, table.Name);
            _db.Database.ExecuteSqlCommand(command);
        }
    }
}
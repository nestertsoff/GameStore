namespace GameStore.Web.Areas.Common.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using FluentValidation.Mvc;

    using GameStore.BLL.Dtos.Concrete.InputDtos.FilterInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.GameInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.GenreInput;
    using GameStore.BLL.Dtos.Concrete.InputDtos.PlatformTypeInput;
    using GameStore.BLL.Enums;
    using GameStore.BLL.Pagination;
    using GameStore.BLL.Services.Abstract;
    using GameStore.Web.Areas.Common.Models;

    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        private readonly IGenreService _genreService;

        private readonly IPlatformTypeService _platformTypeService;

        private readonly IPublisherService _publisherService;

        public GameController(
            IGameService gameAppService, 
            IPublisherService publisherAppService, 
            IGenreService genreService, 
            IPlatformTypeService platformTypeService)
        {
            _gameService = gameAppService;
            _publisherService = publisherAppService;
            _genreService = genreService;
            _platformTypeService = platformTypeService;
        }

        [HttpGet]
        public ActionResult Games()
        {
            var paginInfo = new PagingInfo
                                {
                                    PaginationType = PaginationType.TenPerPage, 
                                    TotalItems = _gameService.Get().Count(), 
                                    CurrentPage = 1
                                };


            var model = new GameFilterViewModel
                            {
                                Games =
                                    Mapper.Map<IEnumerable<ShowGameViewModel>>(
                                        _gameService.GetPaginated(
                                            (int)paginInfo.PaginationType,
                                            (paginInfo.CurrentPage - 1) * (int)paginInfo.PaginationType)),
                                Genres =
                                    Mapper.Map<List<GenreViewModel>>(
                                        _genreService.Get().Where(_ => _.ParentGenre == null)), 
                                Platforms =
                                    Mapper.Map<List<PlatformTypeViewModel>>(
                                        _platformTypeService.Get()), 
                                Publishers =
                                    Mapper.Map<List<PublisherViewModel>>(
                                        _publisherService.Get()), 
                                PagingInfo = paginInfo
                            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Games(GameFilterViewModel filters, int page = 1)
        {
            var model = new GameFilterViewModel();

            if (filters.PagingInfo == null)
            {
                model.PagingInfo = filters.PagingInfo = new PagingInfo
                                         {
                                             PaginationType = PaginationType.TenPerPage,
                                             TotalItems = _gameService.Get().Count()
                                         };
            }

            filters.PagingInfo.CurrentPage = page;

            if (filters.PagingInfo.PaginationType == PaginationType.All)
            {
                model.Games =
                    Mapper.Map<List<ShowGameViewModel>>(
                        _gameService.GetFiltered(Mapper.Map<GameFilterInput>(filters)));
            }
            else
            {
                filters.PagingInfo.TotalItems = _gameService.Get().Count();
                model.PagingInfo = filters.PagingInfo;

                model.Games =
                    Mapper.Map<List<ShowGameViewModel>>(
                        _gameService.GetFiltered(
                            Mapper.Map<GameFilterInput>(filters), 
                            (int)filters.PagingInfo.PaginationType, 
                            (filters.PagingInfo.CurrentPage - 1) * (int)filters.PagingInfo.PaginationType));
            }

            return PartialView("_GameList", model);
        }

        public ActionResult Details(string gameKey)
        {
            return View(Mapper.Map<ShowGameViewModel>(_gameService.GetByKey(gameKey)));
        }

        public ActionResult Create()
        {
            var model = new GameViewModel
                            {
                                PublisherList =
                                    new SelectList(_publisherService.Get(), "Id", "CompanyName"), 
                                GenreList = new SelectList(_genreService.Get(), "Id", "Name"), 
                                PlatformTypeList =
                                    new SelectList(_platformTypeService.Get(), "Id", "Type")
                            };

            return View(model);
        }

        [HttpPost]
        [RuleSetForClientSideMessages("Create")]
        public ActionResult Create(GameViewModel model)
        {
            if (ModelState.IsValid)
            {
                var game = Mapper.Map<CreateUpdateGameInput>(model);

                game.Genres =
                    Mapper.Map<List<CreateUpdateGenreInput>>(
                        model.GenreIds.Select(_ => _genreService.Get(_)).ToList());

                game.PlatformTypes =
                    Mapper.Map<List<CreateUpdatePlatformTypeInput>>(
                        model.PlatformTypeIds.Select(_ => _platformTypeService.Get(_)).ToList());

                _gameService.Create(game);
                return RedirectToAction("Games");
            }

            model.PublisherList = new SelectList(_publisherService.Get(), "Id", "CompanyName");
            model.PlatformTypeList = new SelectList(_platformTypeService.Get(), "Id", "Type");
            model.GenreList = new SelectList(_genreService.Get(), "Id", "Name");
            return View(model);
        }

        public ActionResult Edit(string gameKey)
        {
            var model = Mapper.Map<GameViewModel>(_gameService.GetByKey(gameKey));
            model.PublisherList = new SelectList(_publisherService.Get(), "Id", "CompanyName");
            model.GenreList = new SelectList(_genreService.Get(), "Id", "Name");
            model.PlatformTypeList = new SelectList(_platformTypeService.Get(), "Id", "Type");
            return View(model);
        }

        [HttpPost]
        [RuleSetForClientSideMessages("Update")]
        public ActionResult Edit(GameViewModel model)
        {
            if (ModelState.IsValid)
            {
                var game = Mapper.Map<CreateUpdateGameInput>(model);

                game.Genres =
                    Mapper.Map<List<CreateUpdateGenreInput>>(
                        model.GenreIds.Select(_ => _genreService.Get(_)).ToList());

                game.PlatformTypes =
                    Mapper.Map<List<CreateUpdatePlatformTypeInput>>(
                        model.PlatformTypeIds.Select(_ => _platformTypeService.Get(_)).ToList());

                _gameService.Update(game);
                return RedirectToAction("Games");
            }

            model.PublisherList = new SelectList(_publisherService.Get(), "Id", "CompanyName");
            model.PlatformTypeList = new SelectList(_platformTypeService.Get(), "Id", "Type");
            model.GenreList = new SelectList(_genreService.Get(), "Id", "Name");
            return View(model);
        }

        [HttpGet]
        [RuleSetForClientSideMessages("Delete")]
        public ActionResult Delete(string gameKey)
        {
            return View(Mapper.Map<ShowGameViewModel>(_gameService.GetByKey(gameKey)));
        }

        [HttpPost]
        public ActionResult Delete(ShowGameViewModel game)
        {
            if (ModelState.IsValid)
            {
                _gameService.Delete(game.Key);
                return RedirectToAction("Games");
            }

            return View(game);
        }

        [HttpGet]
        public ActionResult Download(string gameKey)
        {
            return new FileContentResult(new byte[0], "application/pdf");
        }

        [OutputCache(Duration = 60)]
        public ActionResult GetGameCount()
        {
            return View("_GameCount", _gameService.Get().Count());
        }
    }
}
namespace GameStore.Web.Areas.Common.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using AutoMapper;

    using GameStore.BLL.Dtos.Concrete.InputDtos.CommentInput;
    using GameStore.BLL.Services.Abstract;
    using GameStore.Web.Areas.Common.Models;

    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        private readonly IGameService _gameService;

        public CommentController(ICommentService commentService, IGameService gameService)
        {
            _commentService = commentService;
            _gameService = gameService;
        }

        [HttpGet]
        public ActionResult Comments(string gameKey)
        {
            var game = _gameService.GetByKey(gameKey);
            var model = new CommentsViewModel
                            {
                                NewComment = new CommentViewModel { GameId = game.Id },
                                Comments = Mapper.Map<IEnumerable<CommentViewModel>>(game.Comments)
                            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Comments(CommentsViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.NewComment.Date = DateTime.UtcNow;
                _commentService.Create(Mapper.Map<CreateUpdateCommentInput>(model.NewComment));
                return RedirectToAction("Comments");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _commentService.Delete(id);
            return RedirectToAction("Comments");
        }
    }
}
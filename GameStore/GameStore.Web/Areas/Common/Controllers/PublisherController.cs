namespace GameStore.Web.Areas.Common.Controllers
{
    using System.Web.Mvc;

    using AutoMapper;

    using GameStore.BLL.Dtos.Concrete.InputDtos.PublisherInput;
    using GameStore.BLL.Services.Abstract;
    using GameStore.Web.Areas.Common.Models;

    public class PublisherController : Controller
    {
        private readonly IPublisherService gameService;

        public PublisherController(IPublisherService gameService)
        {
            this.gameService = gameService;
        }

        public ActionResult Details(string companyName)
        {
            return View(Mapper.Map<PublisherViewModel>(gameService.GetByCompanyName(companyName)));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PublisherViewModel model)
        {
            if (ModelState.IsValid)
            {
                var publisher = Mapper.Map<CreateUpdatePublisherInput>(model);
                gameService.Create(publisher);
                TempData["ResultMsg"] = "The publisher was added successfully!";
                return RedirectToAction("Details", "Publisher", new { companyName = model.CompanyName });
            }

            TempData["ResultMsg"] = "Some fields of game is not valid!";
            return View(model);
        }
    }
}
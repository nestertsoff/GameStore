namespace GameStore.Web.Areas.Common.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using FluentValidation.Attributes;

    using GameStore.Web.Areas.Common.Validators;
    using GameStore.Web.Resouces;

    [Validator(typeof(GameViewModelValidator))]
    public class GameViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Key", ResourceType = typeof(Resource))]
        public string Key { get; set; }

        [Display(Name = "NameEn", ResourceType = typeof(Resource))]
        public string Name { get; set; }

        [Display(Name = "NameRu", ResourceType = typeof(Resource))]
        public string NameRu { get; set; }

        [Display(Name = "DescriptionEn", ResourceType = typeof(Resource))]
        public string Description { get; set; }

        [Display(Name = "DescriptionRu", ResourceType = typeof(Resource))]
        public string DescriptionRu { get; set; }

        [Display(Name = "Price", ResourceType = typeof(Resource))]
        public decimal Price { get; set; }

        [Display(Name = "InStock", ResourceType = typeof(Resource))]
        public int UnitsInStock { get; set; }

        [Display(Name = "Discontinued", ResourceType = typeof(Resource))]
        public bool Discontinued { get; set; }

        public int PublisherId { get; set; }

        [Display(Name = "Publisher", ResourceType = typeof(Resource))]
        public PublisherViewModel Publisher { get; set; }

        public SelectList PublisherList { get; set; }

        [Display(Name = "Comments", ResourceType = typeof(Resource))]
        public IEnumerable<CommentViewModel> Comments { get; set; }

        public IEnumerable<int> GenreIds { get; set; }

        [Display(Name = "Genres", ResourceType = typeof(Resource))]
        public IEnumerable<GenreViewModel> Genres { get; set; }

        public SelectList GenreList { get; set; }

        public IEnumerable<int> PlatformTypeIds { get; set; }

        [Display(Name = "Platforms", ResourceType = typeof(Resource))]
        public IEnumerable<PlatformTypeViewModel> PlatformTypes { get; set; }

        public SelectList PlatformTypeList { get; set; }
    }
}
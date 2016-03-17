namespace GameStore.Web.Areas.Common.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using GameStore.Web.Resouces;

    public class ShowGameViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Key", ResourceType = typeof(Resource))]
        public string Key { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resource))]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resource))]
        public string Description { get; set; }

        [Display(Name = "Price", ResourceType = typeof(Resource))]
        public decimal Price { get; set; }

        [Display(Name = "InStock", ResourceType = typeof(Resource))]
        public int UnitsInStock { get; set; }

        [Display(Name = "Discontinued", ResourceType = typeof(Resource))]
        public bool Discontinued { get; set; }

        [Display(Name = "Publisher", ResourceType = typeof(Resource))]
        public PublisherViewModel Publisher { get; set; }

        [Display(Name = "Genres", ResourceType = typeof(Resource))]
        public IEnumerable<GenreViewModel> Genres { get; set; }

        [Display(Name = "Platforms", ResourceType = typeof(Resource))]
        public IEnumerable<PlatformTypeViewModel> PlatformTypes { get; set; }
    }
}
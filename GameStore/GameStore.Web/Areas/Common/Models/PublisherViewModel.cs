namespace GameStore.Web.Areas.Common.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using GameStore.Web.Resouces;

    public class PublisherViewModel
    {
        [Display(Name = "CompanyName", ResourceType = typeof(Resource))]
        public string CompanyName { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resource))]
        public string Description { get; set; }

        [Display(Name = "HomePage", ResourceType = typeof(Resource))]
        public string HomePage { get; set; }

        public IEnumerable<GameViewModel> Games { get; set; }

        public int Id { get; set; }
    }
}
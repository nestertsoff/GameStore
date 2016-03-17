namespace GameStore.Web.Areas.Common.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using GameStore.BLL.Enums;
    using GameStore.BLL.Pagination;
    using GameStore.Web.Resouces;

    public class GameFilterViewModel
    {
        public IEnumerable<ShowGameViewModel> Games { get; set; }

        public IEnumerable<GenreViewModel> Genres { get; set; }

        public IEnumerable<PublisherViewModel> Publishers { get; set; }

        public IEnumerable<PlatformTypeViewModel> Platforms { get; set; }

        public IEnumerable<int> GenreIds { get; set; }

        public IEnumerable<int> PublisherIds { get; set; }

        public IEnumerable<int> PlatformIds { get; set; }

        [MinLength(3)]
        public string SearchString { get; set; }

        [Display(Name = "Sorting", ResourceType = typeof(Resource))]
        public SortType SortType { get; set; }

        [Display(Name = "Period", ResourceType = typeof(Resource))]
        public PeriodType PeriodType { get; set; }

        [Display(Name = "From", ResourceType = typeof(Resource))]
        public decimal? MinPrice { get; set; }

        [Display(Name = "To", ResourceType = typeof(Resource))]
        public decimal? MaxPrice { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
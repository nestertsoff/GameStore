namespace GameStore.Web.Areas.Common.Models
{
    using System.Collections.Generic;

    using GameStore.BLL.Enums;

    public class GamesViewModel
    {
        public ShowGameViewModel Game { get; set; }

        public IEnumerable<ShowGameViewModel> Games { get; set; }

        public IEnumerable<GenreViewModel> AllGenres { get; set; }

        public IEnumerable<PlatformTypeViewModel> AllPlatformTypes { get; set; }

        public IEnumerable<PublisherViewModel> AllPublishers { get; set; }

        public SortType SortTypes { get; set; }
    }
}
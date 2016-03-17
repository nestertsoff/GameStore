namespace GameStore.BLL.Dtos.Concrete.InputDtos.FilterInput
{
    using System.Collections.Generic;

    using GameStore.BLL.Dtos.Abstract;
    using GameStore.BLL.Enums;
    using GameStore.BLL.Pagination;

    public class GameFilterInput : IInputDto
    {
        public string SearchString { get; set; }

        public SortType SortType { get; set; }

        public PeriodType PeriodType { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public IEnumerable<int> GenreIds { get; set; }

        public IEnumerable<int> PublisherIds { get; set; }

        public IEnumerable<int> PlatformIds { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
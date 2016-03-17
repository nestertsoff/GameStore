namespace GameStore.BLL.Pagination
{
    using System;

    using GameStore.BLL.Enums;

    public class PagingInfo
    {
        public PaginationType PaginationType { get; set; }

        public int TotalItems { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems / (int)PaginationType);
            }
        }
    }
}
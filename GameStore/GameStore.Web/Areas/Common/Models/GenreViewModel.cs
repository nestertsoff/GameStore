namespace GameStore.Web.Areas.Common.Models
{
    using System.Collections.Generic;

    public class GenreViewModel
    {
        public string Name { get; set; }

        public GenreViewModel ParentGenre { get; set; }

        public IEnumerable<GenreViewModel> ChildGenres { get; set; }

        public int Id { get; set; }
    }
}
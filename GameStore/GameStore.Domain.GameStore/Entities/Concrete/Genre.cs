namespace GameStore.DAL.GameStore.Entities.Concrete
{
    using System.Collections.Generic;

    using global::GameStore.DAL.Abstract.Entities.Concrete;

    public class Genre : Entity
    {
        public string Name { get; set; }

        public string NameRu { get; set; }

        public int? ParentGenreId { get; set; }

        public virtual Genre ParentGenre { get; set; }

        public virtual ICollection<Genre> ChildGenres { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
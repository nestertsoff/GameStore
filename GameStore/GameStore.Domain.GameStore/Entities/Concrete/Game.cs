namespace GameStore.DAL.GameStore.Entities.Concrete
{
    using System;
    using System.Collections.Generic;

    using global::GameStore.DAL.Abstract.Entities.Concrete;

    public class Game : Entity
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public string NameRu { get; set; }

        public string Description { get; set; }

        public string DescriptionRu { get; set; }

        public decimal Price { get; set; }

        public short UnitsInStock { get; set; }

        public bool Discontinued { get; set; }

        public DateTime PublishDate { get; set; }

        public int? PublisherId { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

        public virtual ICollection<PlatformType> PlatformTypes { get; set; }

        public int ViewsCount { get; set; }
    }
}
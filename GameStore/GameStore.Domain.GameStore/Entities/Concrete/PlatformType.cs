namespace GameStore.DAL.GameStore.Entities.Concrete
{
    using System.Collections.Generic;

    using global::GameStore.DAL.Abstract.Entities.Concrete;

    public class PlatformType : Entity
    {
        public string Type { get; set; }

        public string TypeRu { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
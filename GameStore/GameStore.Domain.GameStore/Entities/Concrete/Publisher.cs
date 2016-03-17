namespace GameStore.DAL.GameStore.Entities.Concrete
{
    using System.Collections.Generic;

    using global::GameStore.DAL.Abstract.Entities.Concrete;

    public class Publisher : Entity
    {
        public string CompanyName { get; set; }

        public string Description { get; set; }

        public string DescriptionRu { get; set; }

        public string HomePage { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
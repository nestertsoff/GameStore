namespace GameStore.DAL.Abstract.Entities.Concrete
{
    using GameStore.DAL.Abstract.Entities.Abstract;
    public class Entity : IEntity
    {
        public int Id { get; set; }

        public short DbId { get; set; }

        public bool IsDeleted { get; set; }
    }
}

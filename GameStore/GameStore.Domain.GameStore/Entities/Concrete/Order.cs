namespace GameStore.DAL.GameStore.Entities.Concrete
{
    using System;
    using System.Collections.Generic;

    using global::GameStore.DAL.Abstract.Entities.Concrete;

    public class Order : Entity
    {
        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public virtual ICollection<OrderDetails> OrderDetailsList { get; set; }
    }
}
namespace GameStore.DAL.GameStore.Entities.Concrete
{
    using System;
    using System.Collections.Generic;

    using global::GameStore.DAL.Abstract.Entities.Concrete;

    public class Comment : Entity
    {
        public string Name { get; set; }

        public string Body { get; set; }

        public DateTime Date { get; set; }

        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        public int? ParentCommentId { get; set; }

        public virtual Comment ParentComment { get; set; }

        public virtual ICollection<Comment> ChildComments { get; set; }

        public bool HasQuote { get; set; }
    }
}
namespace GameStore.BLL.Dtos.Concrete.InputDtos.CommentInput
{
    using System;
    using System.Collections.Generic;

    using GameStore.BLL.Dtos.Abstract;
    using GameStore.DAL.GameStore.Entities.Concrete;

    public class CreateUpdateCommentInput : IInputDto
    {
        public string Name { get; set; }

        public string Body { get; set; }

        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        public DateTime Date { get; set; }

        public int? ParentCommentId { get; set; }

        public Comment ParentComment { get; set; }

        public ICollection<Comment> ChildComments { get; set; }

        public bool HasQuote { get; set; }

        public int Id { get; set; }
    }
}
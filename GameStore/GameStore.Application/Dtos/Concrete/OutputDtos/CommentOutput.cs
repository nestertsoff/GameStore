namespace GameStore.BLL.Dtos.Concrete.OutputDtos
{
    using System;
    using System.Collections.Generic;

    using GameStore.BLL.Dtos.Abstract;

    public class CommentOutput : IOutputDto
    {
        public string Name { get; set; }

        public string Body { get; set; }

        public int GameId { get; set; }

        public GameOutput Game { get; set; }

        public DateTime Date { get; set; }

        public int? ParentCommentId { get; set; }

        public CommentOutput ParentComment { get; set; }

        public IEnumerable<CommentOutput> ChildComments { get; set; }

        public bool IsDeleted { get; set; }

        public bool HasQuote { get; set; }

        public int Id { get; set; }
    }
}
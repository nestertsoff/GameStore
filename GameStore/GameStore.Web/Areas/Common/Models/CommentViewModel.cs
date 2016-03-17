namespace GameStore.Web.Areas.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FluentValidation.Attributes;

    using GameStore.Web.Resouces;

    [Validator(typeof(CommentViewModel))]
    public class CommentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resource))]
        public string Name { get; set; }

        [Display(Name = "Body", ResourceType = typeof(Resource))]
        public string Body { get; set; }

        public string QuoteBody { get; set; }

        public bool HasQuote { get; set; }

        public DateTime Date { get; set; }

        public int GameId { get; set; }

        public GameViewModel Game { get; set; }

        public int? ParentCommentId { get; set; }

        public CommentViewModel ParentComment { get; set; }

        public IEnumerable<CommentViewModel> ChildComments { get; set; }

        public bool IsDeleted { get; set; }
    }
}
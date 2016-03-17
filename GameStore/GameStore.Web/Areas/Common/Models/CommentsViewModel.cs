namespace GameStore.Web.Areas.Common.Models
{
    using System.Collections.Generic;

    public class CommentsViewModel
    {
        public CommentViewModel NewComment { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
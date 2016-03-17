namespace GameStore.Web.Areas.Common.Validators
{
    using System.Linq;

    using FluentValidation;

    using GameStore.BLL.Services.Abstract;
    using GameStore.Web.Areas.Common.Models;
    using GameStore.Web.Resouces;

    public class CommentViewModelValidator : AbstractValidator<CommentViewModel>
    {
        private readonly ICommentService _commentService;

        public CommentViewModelValidator(ICommentService commentService)
        {
            _commentService = commentService;

            RuleFor(_ => _.Name)
                .NotEmpty()
                .WithLocalizedMessage(() => ValidationResource.NotEmpty)
                .Length(2, 100)
                .WithLocalizedMessage(() => ValidationResource.Length);

            RuleFor(_ => _.Body)
                .NotEmpty()
                .WithLocalizedMessage(() => ValidationResource.NotEmpty)
                .Length(0, 250)
                .WithLocalizedMessage(() => ValidationResource.Length);

            RuleSet(
                "Update",
                () =>
                    {
                        RuleFor(_ => _.Id)
                            .Must(BeExistentId)
                            .WithLocalizedMessage(() => ValidationResource.Existent);
                    });
        }

        private bool BeExistentId(int id)
        {
            return _commentService.Get().SingleOrDefault(_ => _.Id == id) != null;
        }
    }
}
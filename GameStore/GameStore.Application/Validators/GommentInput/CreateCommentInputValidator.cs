namespace GameStore.BLL.Validators.GommentInput
{
    using System.Linq;

    using FluentValidation;

    using GameStore.BLL.Dtos.Concrete.InputDtos.CommentInput;
    using GameStore.DAL.Infrastructure.UnitOfWork.Abstract;

    public class CreateCommentInputValidator : AbstractValidator<CreateUpdateCommentInput>
    {

        private readonly IUnitOfWork _unitOfWork;


        public CreateCommentInputValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleSet("Create", () => { RuleFor(_ => _.Id).NotEmpty().Must(BeUniqueId); });

            RuleSet("Update", () => { RuleFor(_ => _.Id).NotEmpty().Must(BeExistentId); });

            RuleFor(_ => _.Name).NotEmpty().Length(2, 100);
            RuleFor(_ => _.Body).NotEmpty().Length(0, 250);
        }

        private bool BeUniqueId(int id)
        {
            return _unitOfWork.GetComments().Get().SingleOrDefault(_ => _.Id == id) == null;
        }

        private bool BeExistentId(int id)
        {
            return _unitOfWork.GetComments().Get().SingleOrDefault(_ => _.Id == id) != null;
        }
    }
}
namespace GameStore.BLL.Validators.GommentInput
{
    using System.Linq;

    using FluentValidation;

    using GameStore.BLL.Dtos.Concrete.InputDtos.CommentInput;
    using GameStore.DAL.Infrastructure.UnitOfWork.Abstract;

    public class GetDeleteCommentInputValidator : AbstractValidator<GetDeleteCommentInput>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDeleteCommentInputValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(_ => _.Id).NotEmpty().Must(BeExistentId);
        }

        private bool BeExistentId(int id)
        {
            return _unitOfWork.GetComments().Get().SingleOrDefault(_ => _.Id == id) != null;
        }
    }
}
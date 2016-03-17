namespace GameStore.BLL.Services.Abstract
{
    using System.Collections.Generic;

    using GameStore.BLL.Dtos.Concrete.InputDtos.CommentInput;
    using GameStore.BLL.Dtos.Concrete.OutputDtos;

    public interface ICommentService : IServise
    {
        IEnumerable<CommentOutput> Get();

        CommentOutput Get(int id);

        IEnumerable<CommentOutput> GetByGameKey(string key);

        void Create(CreateUpdateCommentInput item);

        void Update(CreateUpdateCommentInput item);

        void Delete(int id);
    }
}
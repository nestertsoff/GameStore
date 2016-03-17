namespace GameStore.DAL.GameStore.Repositories.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using global::GameStore.DAL.Abstract.Context.Abstract;
    using global::GameStore.DAL.Abstract.Repositories.Concrete;
    using global::GameStore.DAL.GameStore.Entities.Concrete;
    using global::GameStore.DAL.GameStore.Repositories.Abstract;

    public class CommentRepository : GameStoreRepository<Comment>, ICommentRepository
    {
        private readonly IGameStoreContext _db;

        public CommentRepository(IGameStoreContext db)
            : base(db)
        {
            _db = db;
        }

        public override IEnumerable<Comment> Find(Func<Comment, bool> predicate)
        {
            return _db.Set<Comment>().Include(_ => _.Game).Where(predicate).ToList();
        }
    }
}
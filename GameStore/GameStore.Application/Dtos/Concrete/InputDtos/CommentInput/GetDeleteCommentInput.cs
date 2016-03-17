namespace GameStore.BLL.Dtos.Concrete.InputDtos.CommentInput
{
    using GameStore.BLL.Dtos.Abstract;

    public class GetDeleteCommentInput : IInputDto
    {
        public string Name { get; set; }

        public string Body { get; set; }

        public int Id { get; set; }
    }
}
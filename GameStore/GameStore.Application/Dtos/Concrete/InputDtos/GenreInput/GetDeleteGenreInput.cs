namespace GameStore.BLL.Dtos.Concrete.InputDtos.GenreInput
{
    using GameStore.BLL.Dtos.Abstract;

    public class GetDeleteGenreInput : IInputDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
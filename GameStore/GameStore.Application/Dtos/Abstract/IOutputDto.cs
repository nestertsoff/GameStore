namespace GameStore.BLL.Dtos.Abstract
{
    public interface IOutputDto : IDto
    {
        bool IsDeleted { get; set; }
    }
}
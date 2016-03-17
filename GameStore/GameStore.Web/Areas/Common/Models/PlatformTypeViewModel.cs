namespace GameStore.Web.Areas.Common.Models
{
    using System.Collections.Generic;

    using GameStore.BLL.Dtos.Concrete.OutputDtos;

    public class PlatformTypeViewModel
    {
        public string Type { get; set; }

        public IEnumerable<GameOutput> Games { get; set; }

        public int Id { get; set; }
    }
}
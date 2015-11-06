namespace BillableHoursWebApp.DataTransferModels
{
    using Common.Mapping;
    using Data.Models;

    public class CategoryResponseModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

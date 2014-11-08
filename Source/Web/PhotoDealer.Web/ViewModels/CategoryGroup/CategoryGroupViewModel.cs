namespace PhotoDealer.Web.ViewModels.CategoryGroup
{
    using PhotoDealer.Data.Models;
    using PhotoDealer.Web.Infrastructure.Mapping;

    public class CategoryGroupViewModel : IMapFrom<CategoryGroup>
    {
        public int CategoryGroupId { get; set; }

        public string GroupName { get; set; }
    }
}
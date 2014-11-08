namespace PhotoDealer.Web.ViewModels.Category
{
    using PhotoDealer.Data.Models;
    using PhotoDealer.Web.Infrastructure.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }
    }
}
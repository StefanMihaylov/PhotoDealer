namespace PhotoDealer.Web.ViewModels
{
    using PhotoDealer.Data.Models;
    using PhotoDealer.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class CategoryGroupViewModel : IMapFrom<CategoryGroup>
    {
        public int CategoryGroupId { get; set; }

        [Required]
        [MaxLength(30)]
        [UIHint("KendoString")]
        public string GroupName { get; set; }
    }
}
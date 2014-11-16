namespace PhotoDealer.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using PhotoDealer.Data.Models;
    using PhotoDealer.Web.Infrastructure.Mapping;

    public class CategoryGroupViewModel : IMapFrom<CategoryGroup>
    {
        public int CategoryGroupId { get; set; }

        [Required]
        [MaxLength(30)]
        [UIHint("KendoString")]
        public string GroupName { get; set; }
    }
}
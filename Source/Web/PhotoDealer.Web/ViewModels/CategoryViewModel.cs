namespace PhotoDealer.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using PhotoDealer.Data.Models;
    using PhotoDealer.Web.Infrastructure.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(30)]
        [UIHint("KendoString")]
        public string Name { get; set; }

        public int CategoryGroupId { get; set; }

        // public virtual CategoryGroup CategoryGroup { get; set; }
    }
}
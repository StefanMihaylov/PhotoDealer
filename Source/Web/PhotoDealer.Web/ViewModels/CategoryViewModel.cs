namespace PhotoDealer.Web.ViewModels
{
    using PhotoDealer.Data.Models;
    using PhotoDealer.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public int CategoryGroupId { get; set; }

       // public virtual CategoryGroup CategoryGroup { get; set; }
    }
}
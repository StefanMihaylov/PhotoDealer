namespace PhotoDealer.Web.ViewModels
{
    using PhotoDealer.Data.Models;
    using PhotoDealer.Web.Infrastructure.Mapping;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class PictureViewModel : IMapFrom<Picture>
    {
        public string Title { get; set; }

        [UIHint("CategoryGroup")]
        public int? CategoryGroupId { get; set; }

        [DisplayName("Category Group")]
        public CategoryGroup CategoryGroup { get; set; }

        [UIHint("Category")]
        public int? CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
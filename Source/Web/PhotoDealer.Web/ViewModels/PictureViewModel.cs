namespace PhotoDealer.Web.ViewModels
{
    using PhotoDealer.Data.Models;
    using PhotoDealer.Web.Infrastructure.Mapping;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class PictureViewModel : IMapFrom<Picture>
    {
        [Required]
        [DisplayName("Picture Title")]
        public string Title { get; set; }

        [Required]
        [UIHint("CategoryGroup")]
        public int? CategoryGroupId { get; set; }

        [DisplayName("Category Group")]
        public CategoryGroup CategoryGroup { get; set; }


        [UIHint("Category")]
        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        public string TagsString { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }
    }
}
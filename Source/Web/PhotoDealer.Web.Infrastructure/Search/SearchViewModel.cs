namespace PhotoDealer.Web.Infrastructure.Search
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class SearchViewModel
    {
        public string Title { get; set; }

        public string Author { get; set; }

        [DisplayName("Price From")]
        public decimal PriceFrom { get; set; }

        [DisplayName("Price To")]
        public decimal PriceTo { get; set; }

        public string Tag { get; set; }

        [DisplayName("Category Group")]
        [UIHint("CategoryGroup")]
        public int CategoryGroup { get; set; }

        [UIHint("Category")]
        public int Category { get; set; }

        public OrderByEnum OrderBy { get; set; }

        public OrderTypeEnum OrderType { get; set; }

        public int Page { get; set; }
    }
}
namespace PhotoDealer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Tag
    {
        private ICollection<Picture> pictures;

        public Tag()
        {
            this.pictures = new HashSet<Picture>();
        }

        [Key]
        public int TagId { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(20, MinimumLength = 3)]
        public string Content { get; set; }

        public virtual ICollection<Picture> Pictures
        {
            get { return this.pictures; }
            set { this.pictures = value; }
        }
    }
}

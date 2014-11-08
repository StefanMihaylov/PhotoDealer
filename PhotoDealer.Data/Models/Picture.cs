using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoDealer.Data.Models
{
    public class Picture
    {
        private ICollection<string> tags;

        public Picture()
        {
            this.PictureId = Guid.NewGuid();
            this.tags = new HashSet<string>();
        }

        [Key]
        public Guid PictureId { get; set; }

        [Required]
        public byte[] Content { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string ContentType { get; set; }

        [Required]
        public int WidthPixels { get; set; }

        [Required]
        public int HeightPixels { get; set; }

        public decimal Price { get; set; }


        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }


        public int? CategoryGroupId { get; set; }

        public CategoryGroup CategoryGroup { get; set; }

        public int? SubCategoryId { get; set; }

        public Category SubCategory { get; set; }


        public virtual ICollection<string> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }
    }
}

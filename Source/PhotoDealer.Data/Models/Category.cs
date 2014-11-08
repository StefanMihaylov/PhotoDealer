using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace PhotoDealer.Data.Models
{
    public class Category
    {
        private ICollection<Picture> pictures;

        public Category()
        {
            this.pictures = new HashSet<Picture>();
        }

        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(30)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public int CategoryGroupId { get; set; }

        public virtual CategoryGroup CategoryGroup { get; set; }

        public virtual ICollection<Picture> Pictures
        {
            get { return this.pictures; }
            set { this.pictures = value; }
        }
    }
}

using PhotoDealer.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoDealer.Data.Models
{
    public class CategoryGroup : AuditInfo, IDeletableEntity
    {
        private ICollection<Category> categories;
        private ICollection<Picture> pictures;

        public CategoryGroup()
        {
            this.categories = new HashSet<Category>();
            this.pictures = new HashSet<Picture>();
        }

        [Key]
        public int CategoryGroupId { get; set; }

        [Required]
        [MaxLength(30)]
        [Index(IsUnique = true)]
        public string GroupName { get; set; }

        public virtual ICollection<Category> Categories
        {
            get { return this.categories; }
            set { this.categories = value; }
        }

        public virtual ICollection<Picture> Pictures
        {
            get { return this.pictures; }
            set { this.pictures = value; }
        }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}

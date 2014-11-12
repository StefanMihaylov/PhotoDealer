namespace PhotoDealer.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using PhotoDealer.Data.Common.Models;

    public class Category : AuditInfo, IDeletableEntity
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

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}

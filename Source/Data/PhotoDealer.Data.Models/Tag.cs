namespace PhotoDealer.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using PhotoDealer.Data.Common.Models;

    public class Tag : AuditInfo, IDeletableEntity
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

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}

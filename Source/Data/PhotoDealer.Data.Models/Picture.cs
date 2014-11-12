﻿namespace PhotoDealer.Data.Models
{    
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using PhotoDealer.Data.Common.Models;

    public class Picture : AuditInfo, IDeletableEntity
    {
        private ICollection<Tag> tags;

        public Picture()
        {
            this.PictureId = Guid.NewGuid();
            this.tags = new HashSet<Tag>();
        }

        [Key]
        public Guid PictureId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public byte[] FileContent { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FileContentType { get; set; }

        [Required]
        public int WidthPixels { get; set; }

        [Required]
        public int HeightPixels { get; set; }

        public decimal Price { get; set; }

        public int Downloads { get; set; }

        public bool IsVisible { get; set; }

        public bool IsPrivate { get; set; }

        [ForeignKey("Author")]
        public string AuthorId { get; set; }
        
        public virtual User Author { get; set; }

        [ForeignKey("Owner")]
        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }

        [Required]
        public int CategoryGroupId { get; set; }

        public CategoryGroup CategoryGroup { get; set; }

        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}

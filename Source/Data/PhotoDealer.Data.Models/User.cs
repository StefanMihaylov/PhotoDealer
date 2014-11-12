﻿namespace PhotoDealer.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using PhotoDealer.Data.Common.Models;

    public class User : IdentityUser, IAuditInfo, IDeletableEntity
    {
        private ICollection<Picture> ownerPictures;
        private ICollection<Picture> authorPictures;

        public User()
        {
            // This will prevent UserManager.CreateAsync from causing exception
            this.CreatedOn = DateTime.Now;
            this.ownerPictures = new HashSet<Picture>();
            this.authorPictures = new HashSet<Picture>();
        }

        public decimal Credits { get; set; }

        [InverseProperty("Author")]
        public virtual ICollection<Picture> OwnerPictures
        {
            get { return this.ownerPictures; }
            set { this.ownerPictures = value; }
        }

        [InverseProperty("Owner")]
        public virtual ICollection<Picture> AuthorPictures
        {
            get { return this.authorPictures; }
            set { this.authorPictures = value; }
        }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Specifies whether or not the CreatedOn property should be automatically set.
        /// </summary>
        [NotMapped]
        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}

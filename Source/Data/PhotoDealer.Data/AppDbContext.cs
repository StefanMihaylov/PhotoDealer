namespace PhotoDealer.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using System.Linq;

    using PhotoDealer.Data.Models;
    using PhotoDealer.Data.Migrations;
    using PhotoDealer.Data.Common.Models;
    using System;

    public class AppDbContext : IdentityDbContext<User>, IAppDbContext
    {
        public AppDbContext()
            : base("PhotoDealerDb", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Configuration>());
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        public IDbSet<Picture> Pictures { get; set; }

        public IDbSet<CategoryGroup> CategoryGroups { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Transaction> Transactions { get; set; }

        public IDbSet<Tag> Tags { get; set; }

        // additional code
        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletableEntityRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            var entities = this.ChangeTracker.Entries()
                    .Where(e => e.Entity is IAuditInfo &&
                        ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));
            foreach (var entry in entities)
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void ApplyDeletableEntityRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            var entities = this.ChangeTracker.Entries()
                        .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Deleted));
            foreach (var entry in entities)
            {
                var entity = (IDeletableEntity)entry.Entity;
                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}

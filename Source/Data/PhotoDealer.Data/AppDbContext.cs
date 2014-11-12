namespace PhotoDealer.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;

    using PhotoDealer.Data.Common.Models;
    using PhotoDealer.Data.Migrations;
    using PhotoDealer.Data.Models;

    public class AppDbContext : IdentityDbContext<User>, IAppDbContext
    {
        public AppDbContext()
            : this("PhotoDealerDb")
        {
        }

        public AppDbContext(string connectionString)
            : base(connectionString, throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Configuration>());
        }

        public virtual IDbSet<Picture> Pictures { get; set; }

        public virtual IDbSet<CategoryGroup> CategoryGroups { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<CreditTransaction> CreditTransactions { get; set; }

        public virtual IDbSet<Tag> Tags { get; set; }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        // additional code
        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
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
    }
}

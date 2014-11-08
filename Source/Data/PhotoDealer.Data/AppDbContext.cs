namespace PhotoDealer.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;

    using PhotoDealer.Data.Models;
    using PhotoDealer.Data.Migrations;

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

        public virtual IDbSet<Picture> Pictures { get; set; }

        public virtual IDbSet<CategoryGroup> CategoryGroups { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Transaction> Transactions { get; set; }
    }
}

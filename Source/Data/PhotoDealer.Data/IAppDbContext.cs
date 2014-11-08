namespace PhotoDealer.Data
{
    using PhotoDealer.Data.Models;
    using System.Data.Entity;

    public interface IAppDbContext
    {
        IDbSet<Picture> Pictures { get; set; }

        IDbSet<CategoryGroup> CategoryGroups { get; set; }

        IDbSet<Category> Categories { get; set; }

        IDbSet<Transaction> Transactions { get; set; }

        int SaveChanges();
    }
}

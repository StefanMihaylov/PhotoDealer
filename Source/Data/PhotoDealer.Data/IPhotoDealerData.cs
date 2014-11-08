namespace PhotoDealer.Data
{
    using PhotoDealer.Data.Common.Repository;
    using PhotoDealer.Data.Models;

    public interface IPhotoDealerData
    {
        IRepository<User> Users { get; }

        IRepository<Picture> Pictures { get; }

        IRepository<CategoryGroup> CategoryGroups { get; }

        IRepository<Category> Categories { get; }

        IRepository<Transaction> Transactions { get; }

        int SaveChanges();
    }
}

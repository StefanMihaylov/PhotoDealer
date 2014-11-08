namespace PhotoDealer.Data
{
    using PhotoDealer.Data.Models;
    using PhotoDealer.Data.Repositories;

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

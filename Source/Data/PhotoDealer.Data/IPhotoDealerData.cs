namespace PhotoDealer.Data
{
    using PhotoDealer.Data.Common.Repository;
    using PhotoDealer.Data.Models;

    public interface IPhotoDealerData
    {
        IDeletableEntityRepository<User> Users { get; }

        IDeletableEntityRepository<Picture> Pictures { get; }

        IDeletableEntityRepository<CategoryGroup> CategoryGroups { get; }

        IDeletableEntityRepository<Category> Categories { get; }

        IDeletableEntityRepository<Transaction> Transactions { get; }

        IDeletableEntityRepository<Tag> Tags { get; }

        int SaveChanges();
    }
}

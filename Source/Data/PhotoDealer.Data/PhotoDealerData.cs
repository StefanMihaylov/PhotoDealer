namespace PhotoDealer.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using PhotoDealer.Data.Models;
    using PhotoDealer.Data.Common.Repository;
    using PhotoDealer.Data.Common.Models;

    public class PhotoDealerData : IPhotoDealerData
    {
        private IAppDbContext context;

        private readonly IDictionary<Type, object> repositories;

        public PhotoDealerData(IAppDbContext context)
        {
            this.Context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IAppDbContext Context
        {
            get { return this.context; }
            private set { this.context = value; }
        }

        public IDeletableEntityRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IDeletableEntityRepository<Picture> Pictures
        {
            get { return this.GetRepository<Picture>(); }
        }

        public IDeletableEntityRepository<CategoryGroup> CategoryGroups
        {
            get { return this.GetRepository<CategoryGroup>(); }
        }

        public IDeletableEntityRepository<Category> Categories
        {
            get { return this.GetRepository<Category>(); }
        }

        public IDeletableEntityRepository<CreditTransaction> CreditTransactions
        {
            get { return this.GetRepository<CreditTransaction>(); }
        }

        public IDeletableEntityRepository<Tag> Tags
        {
            get { return this.GetRepository<Tag>(); }
        }

        // common parts
        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IDeletableEntityRepository<T> GetRepository<T>() where T : class, IDeletableEntity
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(DeletableEntityRepository<T>), context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeOfRepository];
        }
    }
}

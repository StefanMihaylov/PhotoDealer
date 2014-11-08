namespace PhotoDealer.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using PhotoDealer.Data.Models;
    using PhotoDealer.Data.Common.Repository;

    public class PhotoDealerData : IPhotoDealerData
    {
        private IAppDbContext context;

        private IDictionary<Type, object> repositories;

        public PhotoDealerData(IAppDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IRepository<Picture> Pictures
        {
            get { return this.GetRepository<Picture>(); }
        }

        public IRepository<CategoryGroup> CategoryGroups
        {
            get { return this.GetRepository<CategoryGroup>(); }
        }

        public IRepository<Category> Categories
        {
            get { return this.GetRepository<Category>(); }
        }

        public IRepository<Transaction> Transactions
        {
            get { return this.GetRepository<Transaction>(); }
        }


        // common parts
        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(EfRepository<T>), context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}

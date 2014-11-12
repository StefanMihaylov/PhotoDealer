﻿namespace PhotoDealer.Data
{
    using System.Data.Entity;
    using PhotoDealer.Data.Models;

    public interface IAppDbContext
    {
        IDbSet<Picture> Pictures { get; set; }

        IDbSet<CategoryGroup> CategoryGroups { get; set; }

        IDbSet<Category> Categories { get; set; }

        IDbSet<CreditTransaction> CreditTransactions { get; set; }

        IDbSet<Tag> Tags { get; set; }

        int SaveChanges();
    }
}

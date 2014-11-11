namespace PhotoDealer.Data.Common.Repository
{
    using System.Data.Entity;
    using System.Linq;

    using PhotoDealer.Data.Common.Models;
    using System;

    public class DeletableEntityRepository<T> : EfRepository<T>, IDeletableEntityRepository<T>
        where T : class, IDeletableEntity
    {
        public DeletableEntityRepository(DbContext context)
            : base(context)
        {
        }

        public override IQueryable<T> All()
        {
            return base.All().Where(x => !x.IsDeleted);
        }

        public override void Delete(T entity)
        {
            entity.DeletedOn = DateTime.Now;
            entity.IsDeleted = true;

            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public IQueryable<T> AllWithDeleted()
        {
            return base.All();
        }

        public void ActualDelete(T entry)
        {
            base.Delete(entry);
        }
    }
}

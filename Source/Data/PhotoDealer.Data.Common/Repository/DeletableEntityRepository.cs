namespace PhotoDealer.Data.Common.Repository
{
    using System.Data.Entity;
    using System.Linq;

    using PhotoDealer.Data.Common.Models;

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

        public IQueryable<T> AllWithDeleted()
        {
            return base.All();
        }
    }
}

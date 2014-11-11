namespace PhotoDealer.Data.Common.Repository
{
    using System.Linq;

    using PhotoDealer.Data.Common.Models;

    public interface IDeletableEntityRepository<T> : IRepository<T> where T : class, IDeletableEntity
    {
        IQueryable<T> AllWithDeleted();

        void ActualDelete(T entry);
    }
}

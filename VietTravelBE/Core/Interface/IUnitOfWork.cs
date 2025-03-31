using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Core.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}

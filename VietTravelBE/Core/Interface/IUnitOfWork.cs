using Microsoft.EntityFrameworkCore.Storage;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Core.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
        Task RollbackAsync();
    }
}

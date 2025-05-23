﻿using System.Linq.Expressions;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Core.Interface
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? include = null);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
    }
}

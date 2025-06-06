﻿using System.Linq.Expressions;

namespace VietTravelBE.Core.Interface
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        List<string> IncludeStrings { get; } // For ThenInclude
        bool IsDistinct { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
        IQueryable<T> ApplyCriteria(IQueryable<T> query);
    }
    public interface ISpecification<T, TResult> : ISpecification<T>
    {
        Expression<Func<T, TResult>> Select { get; }
    }
}

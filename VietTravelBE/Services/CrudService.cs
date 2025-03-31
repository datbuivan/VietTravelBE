using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using VietTravelBE.Core.Interface;
using VietTravelBE.Core.Specifications;
using VietTravelBE.Dtos;
using VietTravelBE.Infrastructure;
using VietTravelBE.Infrastructure.Data.Entities;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Services
{
    public class CrudService<T> where T : BaseEntity
    {
        private readonly IGenericRepository<T> _repo;
        private readonly IUnitOfWork _unit;
        public CrudService(IGenericRepository<T> repo, IUnitOfWork unit)
        {
            _repo = repo;
            _unit = unit;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            
            return await _repo.ListAllAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            _repo.Add(entity);
            await _unit.Complete();
            return entity;
        }

        public async Task<T> UpdateAsync(int id, T entity)
        {
            var existingEntity = await _repo.GetByIdAsync(id);
            if (existingEntity == null) return null;

            _repo.Update(entity);
            await _unit.Complete();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            _repo.Delete(entity);
            await _unit.Complete();
            return true;
        }
    }
}

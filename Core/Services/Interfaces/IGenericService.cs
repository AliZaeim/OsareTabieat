﻿using System.Linq.Expressions;

namespace Core.Services.Interfaces
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        public void Create(TEntity entity);
        public Task CreateAsync(TEntity entity);
        public void CreateRange(List<TEntity> entities);
        public void Delete(TEntity entity);
        public void Delete(int id);
        public void Edit(TEntity entity);
        public IQueryable<TEntity> GetAll();
        public Task<IEnumerable<TEntity>> GetAllAsync();
        public TEntity? GetById(int id);
        public Task<TEntity?> GetByIdAsync(int id);
        public bool ExistEntity(int id);
        public IEnumerable<TEntity> Filter();
        public Task<IEnumerable<TEntity>> FilterAsync();
        public IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);
        public Task<IEnumerable<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> predicate);
        public void SaveChanges();
        public Task SaveChangesAsync();
        public bool DetachEntity(TEntity entity);
        
    }
}

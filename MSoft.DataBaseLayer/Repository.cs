using DataBaseLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DataBaseLayer
{
    /// <summary>
    /// Определяет методы обработки сущности заданного типа.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public class Repository<T> : IRepository<T> where T:class, IEntity
    {
        #region Protected

        protected IQueryable<T> IncludeProperties(IQueryable<T> query, Expression<Func<T, object>>[] includeProperties)
        {
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        protected IQueryable<T> GetQueryWithProperties(params Expression<Func<T, object>>[] includeProperties)
        {
            return IncludeProperties(context.Set<T>().AsQueryable<T>(), includeProperties);
        }

        protected IDbContext context;  

        #endregion

        #region Public

        /// <summary>
        /// Инициализирует экземпляр типа <see cref="Repository"/>
        /// </summary>
        /// <param name="context">Контекст подключения к базе данных.</param>
        public Repository(IDbContext context)
        {
            this.context = context;
        }

        #region Реализация IRepository<T>

        public bool Any()
        {
            return context.Set<T>().Any();
        }

        public bool Any(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            return GetQueryWithProperties(includeProperties).Any(where);
        }

        public T Get(long id, params Expression<Func<T, object>>[] includeProperties)
        {
            return GetQueryWithProperties(includeProperties).FirstOrDefault(p => p.Id == id);
        }

        public void Delete(T entity)
        {
            var forRemove = context.Set<T>().FirstOrDefault(p => p.Id == entity.Id);

            if(forRemove != null)
            {
                context.Set<T>().Remove(forRemove);
            }
        }        
                
        public T FirstOrDefault(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {            
            return GetQueryWithProperties(includeProperties).FirstOrDefault(where);
        }

        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            return GetQueryWithProperties(includeProperties).ToList();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            return GetQueryWithProperties(includeProperties).Where(where);
        }

        public T Insert(T entity)
        {
            return context.Set<T>().Add(entity);
        }

        public T Single(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            return GetQueryWithProperties(includeProperties).Single(where);
        }
                
        public void Update(T entity)
        {
            var dbEntity = context.Entry<T>(entity);
            
            if(dbEntity.State == EntityState.Detached)
            {
                context.Set<T>().Attach(entity);
            }

            dbEntity.State = EntityState.Modified;
        }

        #endregion         

        #endregion   
    }
}

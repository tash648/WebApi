using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataBaseLayer.Interfaces
{
    /// <summary>
    /// Определяет интерфейс репозитория.
    /// </summary>
    /// <typeparam name="T">Тип сущности репозитория.</typeparam>
    public interface IRepository<T> where T:IEntity
    {
        /// <summary>
        /// Возвращает сущность по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Сущность по её идентификатору.</returns>
        T Get(long id, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Возвращает признак, есть ли хотябы один элемент в коллекции.
        /// </summary>
        /// <returns>Признак, есть ли хотябы один элемент в коллекции.</returns>
        bool Any();

        /// <summary>
        /// Возвращает признак, есть ли хотябы один элемент в коллекции по заданному условию.
        /// </summary>
        /// <param name="where">Условие.</param>
        /// <param name="includeProperties"></param>
        /// <returns>Признак, есть ли хотябы один элемент в коллекции по заданному условию.</returns>
        bool Any(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Выполняет добавление сущности в репозиторий.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        T Insert(T entity);

        /// <summary>
        /// Выполняет удаление сущности в репозиторий.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        void Delete(T entity);

        /// <summary>
        /// Выполняет обновление сущности в репозиторий.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        void Update(T entity);

        /// <summary>
        /// Возвращает сущность, которая удовлетворяет условию.
        /// </summary>
        /// <param name="where">Условие.</param>
        /// <returns>Сущность, которая удовлетворяет условию.</returns>
        T FirstOrDefault(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Возвращает строго один объект сущности, которая удовлетворяет условию.
        /// </summary>
        /// <param name="where">Условие.</param>
        /// <returns>Строго один объект сущности, которая удовлетворяет условию.</returns>
        T Single(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Возвращает все сущности репозитория.
        /// </summary>
        /// <returns>Все сущности.</returns>
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Возвращает сущности, которые удовлетворяют условию.
        /// </summary>
        /// <param name="where">Условие.</param>
        /// <returns>сущности, которые удовлетворяют условию.</returns>
        IEnumerable<T> GetAll(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);        
    }
}

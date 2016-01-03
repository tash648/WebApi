using System;
using System.Data;
using System.Threading.Tasks;

namespace DataBaseLayer.Interfaces
{
    /// <summary>
    /// Определяет интерфейс единицы работы.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Возвращает экземпляр типа <see cref="IDbContext"/> предоставляющий методы доступа к базе данных.
        /// </summary>
        IDbContext Context { get; }

        /// <summary>
        /// Выполняет фиксацию изменений.
        /// </summary>
        void Commit();

        /// <summary>
        /// Выполняет откат изменений.
        /// </summary>
        void RollBack();

        /// <summary>
        /// Выполняет заданное действие транзакционно и асинхронно.
        /// </summary>
        /// <param name="action">Действие.</param>        
        Task Transaction(Func<Task> action, IsolationLevel level = IsolationLevel.RepeatableRead);

        /// <summary>
        /// Выполняет заданное действие транзакционно.
        /// </summary>
        /// <param name="action">Действие.</param>     
        void Transaction(Action action, IsolationLevel level = IsolationLevel.RepeatableRead);

        /// <summary>
        /// Выполняет транзакцию с заданным действием и возвращает результат.
        /// </summary>
        /// <typeparam name="TResult">Тип результата.</typeparam>
        /// <param name="action">Дейтсвие.</param>
        /// <returns>Результат действия.</returns>
        TResult Transaction<TResult>(Func<TResult> action, IsolationLevel level = IsolationLevel.RepeatableRead);

        /// <summary>
        /// Выполняет транзакцию асинхронно с заданным действием и возвращает результат.
        /// </summary>
        /// <typeparam name="TResult">Тип результата.</typeparam>
        /// <param name="action">Дейтсвие.</param>
        /// <returns>Результат действия.</returns>
        Task<TResult> Transaction<TResult>(Func<Task<TResult>> action, IsolationLevel level = IsolationLevel.RepeatableRead);

        /// <summary>
        /// Выполняет фиксацию после заданного действия.
        /// </summary>
        /// <param name="action">Действие.</param>
        void CommitOperation(Action action);

        /// <summary>
        /// Выполняет фиксацию асинхронно после заданного аиснхронного действия.
        /// </summary>
        /// <param name="action">Действие.</param>
        Task CommitOperation(Func<Task> action);

        /// <summary>
        /// Возвращает репозиторий заданного типа.
        /// </summary>
        /// <typeparam name="T">Тип репозитория.</typeparam>
        /// <returns>Репозиторий заданного типа.</returns>
        IRepository<T> GetRepository<T>() where T : class, IEntity;
    }
}

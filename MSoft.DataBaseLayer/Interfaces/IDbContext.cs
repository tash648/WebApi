using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;

namespace DataBaseLayer.Interfaces
{
    /// <summary>
    /// Определяет интерфейс контекста подключения к базе данных.
    /// </summary>
    public interface IDbContext : IDisposable
    {
        /// <summary>
        ///  Возвращает объект, используемый для обращения к функциям, осуществляющим отслеживание  изменений.
        /// </summary>
        DbChangeTracker ChangeTracker { get; }

        /// <summary>
        /// Возвращает Объект, используемый для доступа к параметрам конфигурации.
        /// </summary>
        DbContextConfiguration Configuration { get; }

        /// <summary>
        /// Создает экземпляр базы данных для этого контекста, который позволяет выполнять
        /// создание, удаление или проверку существования основной базы данных.
        /// </summary>
        Database Database { get; }

        /// <summary>
        /// Возвращает для заданной сущности объект System.Data.Entity.Infrastructure.DbEntityEntry,
        /// предоставляющий доступ к сведениям о сущности и о возможности выполнения
        /// действий над ней.
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns>Запись для сущности</returns>
        DbEntityEntry Entry(object entity);

        /// <summary>
        /// Возвращает для заданной сущности объект System.Data.Entity.Infrastructure.DbEntityEntry,
        /// предоставляющий доступ к сведениям о сущности и о возможности выполнения
        /// действий над ней.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="entity">Сущность.</param>
        /// <returns>Запись для сущности.</returns>
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Проверяет отслеживаемые сущности и возвращает коллекцию System.Data.Entity.Validation.DbEntityValidationResult,
        /// содержащую результаты проверки.
        /// </summary>
        /// <returns>Коллекция результатов проверки для недопустимых сущностей.Эта коллекция никогда не равна значению NULL и не должна содержать значения null или результаты для допустимых сущностей.</returns>
        IEnumerable<DbEntityValidationResult> GetValidationErrors();

        /// <summary>
        /// Сохраняет все изменения основной базы данных, произведенные в контексте.
        /// </summary>
        /// <returns>Количество объектов, записанных в основную базу данных</returns>
        int SaveChanges();

        /// <summary>
        ///  Асинхронно сохраняет все изменения основной базы данных, произведенные в
        ///  контексте.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию сохранения.Результат задач содержит
        /// количество объектов, записанных в основную базу данных.</returns>
        Task<int> SaveChangesAsync();
       
        /// <summary>
        /// Асинхронно сохраняет все изменения основной базы данных, произведенные в
        /// контексте.
        /// </summary>
        /// <param name="cancellationToken">Токен System.Threading.CancellationToken, который нужно отслеживать во время
        /// ожидания выполнения задачи.</param>
        /// <returns>Задача, представляющая асинхронную операцию сохранения.Результат задач содержит
        /// количество объектов, записанных в основную базу данных.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// System.Data.Entity.IDbSet представляет коллекцию всех сущностей
        /// указанного типа, которые содержатся в контексте или могут быть запрошены
        /// из базы данных.
        /// </summary>
        /// <typeparam name="TEntity">Сущность-тип, для которой возвращается набор.</typeparam>
        /// <returns>Набор для заданного типа сущности.</returns>
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}

using DataBaseLayer.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    /// <summary>
    /// Описывает абстрактный класс единицы работы с заданным контекстом.
    /// </summary>
    /// <typeparam name="T">Тип контекста.</typeparam>
    public abstract class UnitOfWork<T> : IUnitOfWork, IDisposable
        where T: IDbContext, new()
    {
        #region Protected
		
        private bool isDisposed;

        protected IDbContext context;

        /// <summary>
        /// Выполняет инициализацию типа <see cref="UnitOfWork"/>
        /// </summary>
        protected UnitOfWork()
        {
            context = new T();
        }
 
	    #endregion

        #region Public

        /// <summary>
        /// Возвращает репозиторий заданного типа.
        /// </summary>
        /// <typeparam name="TRepository">Тип репозитория.</typeparam>
        /// <returns>Репозиторий заданного типа.</returns>
        public IRepository<TRepository> GetRepository<TRepository>() where TRepository : class, IEntity
        {
            return new Repository<TRepository>(context);
        }

        #region Реализация IUnitOfWork

        public IDbContext Context
        {
            get
            {
                return context;
            }
        }

        /// <summary>
        /// Выполняет фиксацию изменений.
        /// </summary>
        public void Commit()
        {
            if (context.ChangeTracker.HasChanges())
            {
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Выполняет откат изменений.
        /// </summary>
        public void RollBack()
        {
            context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }

        public async Task Transaction(Func<Task> action, IsolationLevel level = IsolationLevel.Serializable)
        {
            using (var transaction = context.Database.BeginTransaction(level))
            {
                await action();

                await context.SaveChangesAsync();

                transaction.Commit();
            }
        }

        public async Task<TResult> Transaction<TResult>(Func<Task<TResult>> action, IsolationLevel level = IsolationLevel.Serializable)
        {
            using (var transaction = context.Database.BeginTransaction(level))
            {
                var result = await action();

                await context.SaveChangesAsync();

                transaction.Commit();

                return result;
            }
        }

        public TResult Transaction<TResult>(Func<TResult> action, IsolationLevel level = IsolationLevel.Serializable)
        {
            using (var transaction = context.Database.BeginTransaction(level))
            {
                var result = action();

                context.SaveChanges();

                transaction.Commit();

                return result;
            }
        }

        public void Transaction(Action action, IsolationLevel level = IsolationLevel.Serializable)
        {
            using (var transaction = context.Database.BeginTransaction(level))
            {
                action();

                context.SaveChanges();

                transaction.Commit();
            }
        }

        public void CommitOperation(Action action)
        {
            try
            {
                action();
                context.SaveChanges();
            }
            finally
            {
                RollBack();
            }
        }

        public async Task CommitOperation(Func<Task> action)
        {
            try
            {
                await action();

                await context.SaveChangesAsync();
            }
            finally
            {
                RollBack();
            }
        }

        public virtual void Dispose()
        {
            if (!isDisposed)
            {
                context.Dispose();
                isDisposed = true;
            }
        }

        #endregion 

        #endregion
    }
}

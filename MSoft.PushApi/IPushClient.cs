using MSoft.PushApi.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MSoft.Interfaces
{
    /// <summary>
    /// Определяет базовый интерфейс работы клиента с хабом уведомлений.
    /// </summary>
    public interface IPushClientBase : IDisposable
    {
        /// <summary>
        /// Выполняет заданный метод.
        /// </summary>
        /// <param name="expression">Выражение, содержащее имя метода.</param>
        /// <param name="args">Аргументы метода.</param>
        Task ExecuteOperation(Expression<Action<IPushHub>> expression, params object[] args);

        /// <summary>
        /// Выполняет заданный метод.
        /// </summary>
        /// <param name="expression">Выражение, содержащее имя метода.</param>
        /// <param name="args">Аргументы метода.</param>
        Task<TResult> ExecuteOperation<TResult>(Expression<Action<IPushHub>> expression, params object[] args) where TResult : class;

        /// <summary>
        /// Выполняет привязку к заданному событию.
        /// </summary>
        /// <param name="expression">Выражение, содержащее имя метода.</param>
        /// <param name="onAction">Действие по событию.</param>
        void On<TType>(Expression<Action<IPushHub>> expression, Action<TType> onAction);

        /// <summary>
        /// Выполняет привязку к заданному событию.
        /// </summary>
        /// <param name="expression">Выражение, содержащее имя метода.</param>
        /// <param name="onAction">Действие по событию.</param>
        void On(Expression<Action<IPushHub>> expression, Action onAction);
    }
}

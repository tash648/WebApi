using Microsoft.AspNet.SignalR.Client;
using MSoft.PushApi.Interfaces;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using MSoft.Interfaces;

namespace MSoft.PushApi
{
    /// <summary>
    /// Определяет базовый класс клиента уведомлений.
    /// </summary>
    /// <typeparam name="T">Тип источника уведомлений.</typeparam>
    public class PushClientBase<T> : IPushClientBase
        where T : IPushHub
    {
        private HubConnection hubConnection;
        private bool isDisposed;
        private IHubProxy hubProxy;
        
        /// <summary>
        /// Инициализирует экземпляр типа <see cref="PushClientBase"/>
        /// </summary>
        /// <param name="baseUrl">URL-путь к корню сервера.</param>
        public PushClientBase(string baseUrl) 
        {
            this.hubConnection = new HubConnection(string.Format("{0}", baseUrl));
            this.hubProxy = hubConnection.CreateHubProxy(typeof(T).Name);
            this.hubConnection.Start().Wait();                                
        }

        /// <summary>
        /// Асинронно выполняет заданную клиентом операцию.
        /// </summary>
        /// <param name="expression">Выражение, которое содержит имя метода для выполнения.</param>
        /// <param name="args">Аргументы метода.</param>        
        public async Task ExecuteOperation(Expression<Action<IPushHub>> expression, params object[] args)
        {
            var methodName = this.GetMethodName(expression);

            await hubProxy.Invoke(methodName, args);            
        }

        public async Task<TResult> ExecuteOperation<TResult>(Expression<Action<IPushHub>> expression, params object[] args)
            where TResult:class
        {
            return await (ExecuteOperation(expression, args) as Task<TResult>);
        }

        /// <summary>
        /// Осуществляет подписку на заданное событие.
        /// </summary>
        /// <typeparam name="TType">Тип результата события.</typeparam>
        /// <param name="expression">Выражение, содержащее имя события.</param>
        /// <param name="onAction">Действие, выполняемое по событию.</param>
        public void On<TType>(Expression<Action<IPushHub>> expression, Action<TType> onAction)
        {
            var eventName = this.GetMethodName(expression);            

            hubProxy.On<TType>(eventName, onAction);
        }

        /// <summary>
        /// Осуществляет подписку на заданное событие.
        /// </summary>
        /// <param name="expression">Выражение, содержащее имя события.</param>
        /// <param name="onAction">Действие, выполняемое по событию.</param>
        public void On(Expression<Action<IPushHub>> expression, Action onAction)
        {
            var eventName = this.GetMethodName(expression);

            hubProxy.On(eventName, onAction);
        }

        /// <summary>
        /// Выполняет освобождение ресурсов, занятых экземляром <see cref="PushClientBase"/>
        /// </summary>
        public virtual void Dispose()
        {
            if(!isDisposed)
            {
                hubConnection.Dispose();
                isDisposed = true;
            }
        }        
    }
}

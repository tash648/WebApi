using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;

namespace MSoft.PushApi.Interfaces
{    
    /// <summary>
    /// Определяет интерфейс хаба, рассылающего и принимающего уведомления.
    /// </summary>
    public interface IPushHub : IHub, IUntrackedDisposable, IDisposable
    {
        #region Заглушки

        /// <summary>
        /// Метод-заглушка, для выражений привязки к событию NewMessageReceived.
        /// </summary>
        /// <returns></returns>
        void NewMessageReceived();

        /// <summary>
        /// Метод-заглушка, для выражений привязки к событию Connect.
        /// </summary>      
        void Connect();

        /// <summary>
        /// Метод-заглушка, для выражений привязки к событию UpdateOnlineUsers.
        /// </summary>
        void UpdateOnlineUsers();

        /// <summary>
        /// Метод-заглушка, для выражений привязки к событию Disconnect.
        /// </summary>
        /// <returns></returns>       
        void Disconnect();

        /// <summary>
        /// Метод-заглушка, для выражений вызова BroadcastMessageToAll.
        /// </summary>
        void BroadcastMessageToAll();

        /// <summary>
        /// Метод-заглушка, для выражений вызова BroadcastMessageToUser.
        /// </summary>
        void BroadcastMessageToUser();

        /// <summary>
        /// Метод-заглушка, для выражений вызова BroadcastToGroup.
        /// </summary> 
        void BroadcastToGroup();

        /// <summary>
        /// Метод-заглушка, для выражений вызова JoinAGroup.
        /// </summary>
        void JoinAGroup();

        /// <summary>
        /// Метод-заглушка, для выражений вызова RemoveFromAGroup.
        /// </summary>
        void RemoveFromAGroup();

        /// <summary>
        /// Метод-заглушка, для выражений привязки к событию TaskAdded.
        /// </summary>
        void TaskAdded();

        /// <summary>
        /// Метод-заглушка, для выражений привязки к событию IterationAdded.
        /// </summary>
        void IterationAdded();

        /// <summary>
        /// Метод-заглушка, для выражений привязки к событию DeveloperAdded.
        /// </summary>
        void DeveloperAdded();

        /// <summary>
        /// Метод-заглушка, для выражений привязки к событию GroupAdded.
        /// </summary>
        void GroupAdded();

        /// <summary>
        /// Метод-заглушка, для выражений привязки к событию ReportAdded.
        /// </summary>
        void ReportAdded();

        #endregion 

        /// <summary>
        /// Выполняет уведомление о подключении пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>        
        void Connect(string userName);

        /// <summary>
        /// Рассылает уведомление об отключении пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>        
        void Disconnect(string userName);

        /// <summary>
        /// Выполняет доставку сообщения всем.
        /// </summary>
        /// <param name="message">Сообщение.</param>        
        void BroadcastMessageToAll(string message, string sender);

        /// <summary>
        /// Выполняет доставку сообщения пользователю.
        /// </summary>
        /// <param name="message">Сообщение.</param>     
        void BroadcastMessageToUser(string message, string username, string sender);

        /// <summary>
        /// Доносить сообщение до группы.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="group">Группа.</param>        
        void BroadcastToGroup(string message, string group, string sender);

        /// <summary>
        /// Сливает пользователя с заданной группой.
        /// </summary>
        /// <param name="group">Группа.</param>        
        void JoinAGroup(string group);

        /// <summary>
        /// Удаляет пользователя из группы.
        /// </summary>
        /// <param name="group">Группа.</param>        
        void RemoveFromAGroup(string group);        
    }
}

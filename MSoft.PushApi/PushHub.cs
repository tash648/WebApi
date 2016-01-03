using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using MSoft.PushApi.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace MSoft.PushApi
{
    [PartCreationPolicy(CreationPolicy.Shared)]
    /// <summary>
    /// Определяет методы хаба, получающего и рассылающего уведомления.
    /// </summary>
    public class PushHub : Hub, IPushHub
    {
        public static IHubContext Current
        {
            get
            {
                return current ?? (current = GlobalHost.ConnectionManager.GetHubContext<PushHub>());
            }
        }
        private static IHubContext current;

        #region Реализация IPushHub

        private static Dictionary<string, List<string>> onlineUsers = new Dictionary<string, List<string>>();

        #region Заглушки

        public void NewMessageReceived() { }

        public void Connect() { }

        public void Disconnect() { }

        public void UpdateOnlineUsers() { }

        public void BroadcastMessageToAll() { }

        public void BroadcastMessageToUser() { }

        public void BroadcastToGroup() { }

        public void JoinAGroup() { }

        public void RemoveFromAGroup() { }

        public void TaskAdded() { }

        public void IterationAdded() { }

        public void DeveloperAdded() { }

        public void GroupAdded() { }

        public void ReportAdded() { }

        #endregion

        [HubMethodName("Connect")]
        public void Connect(string userName)
        {
            if(!onlineUsers.ContainsKey(userName))
            {
                onlineUsers.Add(userName, new List<string>() { Context.ConnectionId });
            }
            else
            {
                onlineUsers[userName].Add(Context.ConnectionId);
            }

            Groups.Add(Context.ConnectionId, userName);

            Clients.All.UpdateOnlineUsers(onlineUsers.Select(p => p.Key));
        }

        [HubMethodName("Disconnect")]
        public void Disconnect(string userName)
        {
            if(onlineUsers.ContainsKey(userName))
            {
                var user = onlineUsers[userName];

                user.Remove(Context.ConnectionId);

                if(user.Count == 0)
                {   
                    onlineUsers.Remove(userName);                    
                }                
            }

            Groups.Remove(Context.ConnectionId, userName);

            Clients.All.UpdateOnlineUsers(onlineUsers.Select(p => p.Key));
        }

        [HubMethodName("BroadcastMessageToUser")]
        public void BroadcastMessageToUser(string message, string username, string sender)
        {
            Clients.Group(username).NewMessageReceived(new Message() { MessageText = message, Sender = sender });
        }

        [HubMethodName("BroadcastMessageToAll")]
        public void BroadcastMessageToAll(string message, string sender)
        {
            Clients.All.NewMessageReceived(new Message() { MessageText = message, Sender = sender });
        }

        [HubMethodName("JoinAGroup")]
        public void JoinAGroup(string group)
        {
            Groups.Add(Context.ConnectionId, group);
        }

        [HubMethodName("RemoveFromAGroup")]
        public void RemoveFromAGroup(string group)
        {
            Groups.Remove(Context.ConnectionId, group);
        }

        [HubMethodName("BroadcastToGroup")]
        public void BroadcastToGroup(string message, string group, string sender)
        {
            Clients.Group(group).NewMessageReceived(new Message() { MessageText = message, Sender = sender });
        }         

        #endregion
    }
}

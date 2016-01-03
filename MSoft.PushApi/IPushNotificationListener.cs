
using System;
namespace MSoft.PushApi
{
    interface IPushNotificationListener
    {
        IPushNotificationListener On(Action<IPushClient> action);
    }
}

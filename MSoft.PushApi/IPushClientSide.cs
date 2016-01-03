using MSoft.Interfaces;

namespace MSoft.PushApi
{
    /// <summary>
    /// Определяет интерфейс работы клиента с хабом уведомлений.
    /// </summary>
    public interface IPushClient : IPushClientBase
    {
        string UserName { get; set; }
    }
}

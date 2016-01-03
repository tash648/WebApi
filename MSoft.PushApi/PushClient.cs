using System.Threading.Tasks;

namespace MSoft.PushApi
{
    /// <summary>
    /// Определяет класс клиента пуш-уведомления.
    /// </summary>
    public class PushClient : PushClientBase<PushHub>, IPushClient
    {
        /// <summary>
        /// Возвращает имя пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Инициализирует экземпляр типа <see cref="PushClient"/>
        /// </summary>
        /// <param name="baseUrl">Путь к корню хоста.</param>
        public PushClient(string baseUrl, string userName) : base(baseUrl) 
        {
            this.UserName = userName;            
        } 
    }
}

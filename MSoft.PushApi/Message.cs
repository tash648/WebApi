
namespace MSoft.PushApi
{
    /// <summary>
    /// Определяет сущность сообщения.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Возвращает или задает текст сообщения.
        /// </summary>
        public string MessageText { get; set; }

        /// <summary>
        /// Возвращает или задает источник сообщения.
        /// </summary>
        public string Sender { get; set; }
    }
}

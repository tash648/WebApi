using System.ComponentModel.DataAnnotations;

namespace DataBaseLayer.Interfaces
{
    /// <summary>
    /// Определяет интерфейс сущности.
    /// </summary>
    public interface IEntity
    {
        [Key]
        /// <summary>
        /// Возвращает идентификатор сущности.
        /// </summary>
        int Id { get; }
    }
}

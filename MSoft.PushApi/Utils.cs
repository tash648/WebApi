using System.Linq.Expressions;

namespace System
{
    public static class Utils
    {
        /// <summary>
        /// Возвращает имя метода.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="obj">Объект.</param>
        /// <param name="method">Выражение с методом.</param>
        /// <returns>Имя метода.</returns>
        public static string GetMethodName<T>(this object obj, Expression<Action<T>> method)
        {
            var call = (MethodCallExpression)method.Body;

            return call.Method.Name;
        }
    }
}

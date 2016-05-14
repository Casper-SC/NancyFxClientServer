using System.Linq;

namespace Utils
{
    public static class Url
    {
        /// <summary>
        /// Составить путь из сегментов разделяя каждый сегмент слэшем. В конце слэш не ставится.
        /// </summary>
        /// <param name="segments">Части/сегменты пути.</param>
        /// <returns>Возвращает строковое представление пути.</returns>
        public static string Combine(params string[] segments)
        {
            if (segments == null || segments.Length == 0) return "";
            string urlResult = segments.Aggregate("",
                (current, segment) =>
                {
                    return current + (current != "" ? "/" : "") + segment.Trim('/');
                });

            return urlResult;
        }
    }
}
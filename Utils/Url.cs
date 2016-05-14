using System.Linq;

namespace Utils
{
    public static class Url
    {
        /// <summary>
        /// ��������� ���� �� ��������� �������� ������ ������� ������. � ����� ���� �� ��������.
        /// </summary>
        /// <param name="segments">�����/�������� ����.</param>
        /// <returns>���������� ��������� ������������� ����.</returns>
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
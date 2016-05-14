using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Utils.UnitTests
{
    [TestClass]
    public class UrlHelperUnitTest
    {
        [TestMethod]
        public void UrlHelperCombineTest()
        {
            const string pattern = "http://127.0.0.1/part1/part2/part3.txt";

            string result1 = Url.Combine("http://127.0.0.1", "//part1///", "//part2///", "part3.txt");
            Assert.AreEqual(pattern, result1); //сверяем результат выполнения метода с ожидаемым шаблоном.

            string result2 = Url.Combine("http://127.0.0.1", "part1", "part2", "part3.txt");
            Assert.AreEqual(pattern, result2);

            string result3 = Url.Combine("http://127.0.0.1", "/part1/", "/part2/", "/part3.txt/");
            Assert.AreEqual(pattern, result3);

            string result4 = Url.Combine("http://127.0.0.1//", "///part1/", "part2", "////part3.txt//");
            Assert.AreEqual(pattern, result4);
        }
    }
}
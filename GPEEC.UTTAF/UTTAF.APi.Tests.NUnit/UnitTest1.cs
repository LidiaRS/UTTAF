using NUnit.Framework;

namespace UTTAF.APi.Tests.NUnit
{
    public class Tests
    {
        private string str = "world";

        [Test, Order(1)]
        public void Test1()
        {
            Assert.IsTrue(str.Contains('w'));
        }

        [Test, Order(0)]
        public void Test2()
        {
            str = "";
            Assert.IsTrue(str.Contains('w'));
        }
    }
}
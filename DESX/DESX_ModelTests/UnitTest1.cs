using System;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DESX_Model;

namespace DESX_ModelTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var input = new InputHandler("cos tam sobie napisałem", Encoding.UTF8);
            Console.Out.WriteLine(input.PlainTextInBytes.Length);
            foreach (var inputPlainTextInByte in input.PlainTextInBytes)
            {
                Console.Out.WriteLine(inputPlainTextInByte);
            }

            Console.Out.WriteLine(input.PlainTextInBytes.Last());
        }
    }
}

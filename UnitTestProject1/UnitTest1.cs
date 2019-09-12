using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Huffman_code;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SaveTest()
        {
            var filePath = Path.Combine("c:\\temp", "text1.txt");
            var text = new HufCode("Идея, положенная в основу кодировании Хаффмана, " +
                                   "основана на частоте появления символа в последовательности.");
            text.Save(filePath);
        }
    }
}

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
            var filePath = Path.Combine(Path.GetTempPath(), "text1.txt");
            var text = new HufCode("Идея, положенная в основу кодировании Хаффмана, " +
                                   "основана на частоте появления символа в последовательности.");
            text.Save(filePath);
        }
        [TestMethod]
        public void CountTest()
        {
            var filePath = Path.Combine(Path.GetTempPath(), "text.txt");
            var text = new HufCode("aba");
            text.Save(filePath);
            text.CountDictionary(filePath);
        }
    }
}

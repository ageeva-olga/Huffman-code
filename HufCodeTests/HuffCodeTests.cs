using System;
using System.IO;
using Huffman_code;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HufCodeTests
{
    [TestClass]
    public class HuffCodeTests
    {
        [TestMethod]
        public void SaveTest()
        {
            var text = "Идея, положенная в основу кодировании Хаффмана, " +
                                   "основана на частоте появления символа в последовательности.";
            var filePath = Path.Combine(Path.GetTempPath(), "text1.txt");

            var hufCode = new HufCode(text);
            var byteArray = hufCode.CompressText();
            hufCode.Save(filePath, byteArray);
            hufCode.Text = "";

            var textResult = hufCode.Load(filePath);
            Assert.AreEqual(text, textResult);
        }
        [TestMethod]
        public void RestoreTextTest()
        {
            var bits = "1011101100010010111";
            var huffCode = new HufCode();
            var tree = new Node()
            {
                LeftNode = new Node() { Value = 'b' },
                RightNode = new Node()
                {
                    LeftNode = new Node()
                    {
                        LeftNode = new Node() { Value = 'd' },
                        RightNode = new Node() { Value = 'c' }
                    },
                    RightNode = new Node() { Value = 'a' }
                }
            };

            var text = huffCode.RestoreText(tree, bits);
            Assert.AreEqual("cababbbdca", text);
        }
    }
}

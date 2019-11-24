using System;
using Huffman_code;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryHelperTests
{
    [TestClass]
    public class BynaryHelperTests
    {
        [TestMethod]
        public void CompressTest()
        {
            var text = "cababbbdca";
            var huffCode = new HufCode(text);
            var huffTable = new FrequencyTable(huffCode.Text);
            var table = huffTable.MakeFrequencyTable();
            var huffTree = new HuffmanTree();

            huffTree.MakeHuffmanTree(table);
            var treeCode = huffTree.GetHuffmanTable(huffTree.Node);
            int tail = 0;

            var binaryHelper = new BinaryHelper();

            var byteArray = binaryHelper.GetBynary(huffCode.Text, treeCode, out tail);
            Assert.AreEqual(187, byteArray[0]);
            Assert.AreEqual(18, byteArray[1]);
            Assert.AreEqual(224, byteArray[2]);
            Assert.AreEqual(5, tail);
        }
        [TestMethod]
        public void RestoreBytesTest()
        {
            var huffCode = new HufCode();
            var bytes = new byte[3] { 187, 18, 224 };

            var binaryHelper = new BinaryHelper();

            var bit = binaryHelper.RestorBynaryString(bytes, 5);
            Assert.AreEqual("1011101100010010111", bit);
        }
    }
}

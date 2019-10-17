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
            var text = "Идея, положенная в основу кодировании Хаффмана, " +
                                   "основана на частоте появления символа в последовательности.";
            var filePath = Path.Combine(Path.GetTempPath(), "text1.txt");
            var hufCode = new HufCode(text);
            hufCode.Save(filePath);
            hufCode.Text = "";

            hufCode.Load(filePath);
            Assert.AreEqual(text, hufCode.Text);
        }
        [TestMethod]
        public void CountTest()
        {
            var text = new HufCode("cababbbdca");
            var res = text.MakeHuffmanTable();
            Assert.AreEqual('b', res[3].Key);
            Assert.AreEqual(4, res[3].Value);

            Assert.AreEqual('a', res[2].Key);
            Assert.AreEqual(3, res[2].Value);

            Assert.AreEqual('c', res[1].Key);
            Assert.AreEqual(2, res[1].Value);

            Assert.AreEqual('d', res[0].Key);
            Assert.AreEqual(1, res[0].Value);
        }
        [TestMethod]
        public void TreeTest()
        {
            var huffCode = new HufCode("cababbbdca");
            var table = huffCode.MakeHuffmanTable();
            var tree = huffCode.MakeHuffmanTree(table);
            var res1 = tree.LeftNode.Value;
            var res2 = tree.RightNode.RightNode.Value;
            var res3 = tree.RightNode.LeftNode.LeftNode.Value;
            var res4 = tree.RightNode.LeftNode.RightNode.Value;
            Assert.AreEqual('b', res1);
            Assert.AreEqual('a', res2);
            Assert.AreEqual('d', res3);
            Assert.AreEqual('c', res4);
        }
        [TestMethod]
        public void CodeTest()
        {
            var huffCode = new HufCode("cababbbdca");
            var table = huffCode.MakeHuffmanTable();
            var tree = huffCode.MakeHuffmanTree(table);
            var treeCode = huffCode.GetHuffmanTable(tree);
            Assert.AreEqual("11", treeCode['a']);
            Assert.AreEqual("0", treeCode['b']);
            Assert.AreEqual("101", treeCode['c']);
            Assert.AreEqual("100", treeCode['d']);
        }

        [TestMethod]
        public void CompressTest()
        {
            var text = "cababbbdca";
            var huffCode = new HufCode(text);
            var table = huffCode.MakeHuffmanTable();
            var tree = huffCode.MakeHuffmanTree(table);
            var treeCode = huffCode.GetHuffmanTable(tree);
            var byteArray = huffCode.GetBynary(treeCode);
            Assert.AreEqual(187, byteArray[0]);
            Assert.AreEqual(18, byteArray[1]);
        }
    }
}

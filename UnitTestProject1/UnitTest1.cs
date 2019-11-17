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
            var byteArray = hufCode.CompressText();
            hufCode.Save(filePath, byteArray);
            hufCode.Text = "";

            var textResult = hufCode.Load(filePath);
            Assert.AreEqual(text, textResult);
        }
        [TestMethod]
        public void CountTest()
        {
            var huffTable = new FrequencyTable("cababbbdca");
            var res = huffTable.MakeFrequencyTable();
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
            var huffTable = new FrequencyTable(huffCode.Text);
            var table = huffTable.MakeFrequencyTable();
            var huffTree = new HuffmanTree();

            huffTree.MakeHuffmanTree(table);
            var treeCode = huffTree.GetHuffmanTable(huffTree.Node);
            var res1 = huffTree.Node.LeftNode.Value;
            var res2 = huffTree.Node.RightNode.RightNode.Value;
            var res3 = huffTree.Node.RightNode.LeftNode.LeftNode.Value;
            var res4 = huffTree.Node.RightNode.LeftNode.RightNode.Value;
            Assert.AreEqual('b', res1);
            Assert.AreEqual('a', res2);
            Assert.AreEqual('d', res3);
            Assert.AreEqual('c', res4);
        }
        [TestMethod]
        public void CodeTest()
        {
            var huffCode = new HufCode("cababbbdca");
            var huffTable = new FrequencyTable(huffCode.Text);
            var table = huffTable.MakeFrequencyTable();
            var huffTree = new HuffmanTree();

            huffTree.MakeHuffmanTree(table);
            var treeCode = huffTree.GetHuffmanTable(huffTree.Node);
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
                        RightNode = new Node() { Value = 'c'}
                    },
                    RightNode = new Node() { Value = 'a' }
                }
            };

            var text = huffCode.RestoreText(tree, bits);
            Assert.AreEqual("cababbbdca", text);
        }
    }
}

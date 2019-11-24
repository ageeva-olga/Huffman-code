using System;
using Huffman_code;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HuffmanTreeTests
{
    [TestClass]
    public class HuffmanTreeTests
    {
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
    }
}

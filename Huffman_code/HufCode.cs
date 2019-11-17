using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;


namespace Huffman_code
{
    public class HufCode
    {
        public string Text { get; set; }
        private Dictionary<char, string> hufTable;

        private HuffmanTree tree;
        private int byteLengthTail;

        public HufCode()
        {
            hufTable = new Dictionary<char, string>();
            tree = new HuffmanTree();
        }
        public HufCode(string text) : this()
        {
            Text = text;
        }

        public byte[] CompressText()
        {
            var huffTable = new FrequencyTable(Text);
            var table = huffTable.MakeFrequencyTable();

            tree.MakeHuffmanTree(table);
            var treeCode = tree.GetHuffmanTable(tree.Node);

            var binaryHelper = new BinaryHelper();
            var byteArray = binaryHelper.GetBynary(Text, treeCode, out byteLengthTail);
            return byteArray;
        }

        public string RestoreText(Node tree, string bits)
        {
            var node = tree;
            string text = "";
            for (int i = 0; i<bits.Length; i++)
            {
                    if(bits[i] == '1' && node.RightNode != null)
                    {
                        node = node.RightNode;
                    }
                    else if(node.LeftNode != null)
                    {
                        node = node.LeftNode;
                    }
                if (node.Value != null)
                {
                    text += node.Value;
                    node = tree;
                }
            }
            return text;
        }
        public void Save(string path, byte[] bytes)
        {
            Save(path, tree.Node, byteLengthTail, bytes);
        }

        private void Save(string path, Node tree, int tail, byte[] bytes)
        {
            var compressedFile = new CompressedFile(tree, bytes, tail);
            string json = JsonConvert.SerializeObject(compressedFile, Formatting.None);
            using (var writetext = new StreamWriter(path))
            {
                writetext.WriteLine(json);
            }
        }
        public string Load(string path)
        {
            CompressedFile compressedFile = null;
            using (var streamReader = new StreamReader(path))
            {
                var fileText = streamReader.ReadToEnd();
                compressedFile = JsonConvert.DeserializeObject<CompressedFile>(fileText);
            }
            var binaryHelper = new BinaryHelper();

            var bits = binaryHelper.RestorBynaryString(compressedFile.Bytes, compressedFile.Tail);
            var text = RestoreText(compressedFile.Tree, bits);
            return text;
        }

        public string LoadText(string path)
        {
            path = Path.GetFullPath(path);
            using (var streamReader = new StreamReader(Path.GetFullPath(path)))
            {
                var fileText = streamReader.ReadToEnd();
                return fileText;
            }
        }

        public void SaveText(string path, string text)
        {
            path = Path.GetFullPath(path);
            using (var writetext = new StreamWriter(path))
            {
                writetext.WriteLine(text);
            }
        }
    }
}

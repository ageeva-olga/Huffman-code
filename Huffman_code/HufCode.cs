using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_code
{
    public class HufCode
    {
        public string Text { get; set; }
        private Dictionary<char, string> hufTable;
        public HufCode()
        {
            hufTable = new Dictionary<char, string>();
        }
        public HufCode(string text) : this()
        {
            Text = text;
        }

        public KeyValuePair<char, int>[] MakeHuffmanTable()
        {

            var charsCountDic = new Dictionary<char, int>();
            for (int i = 0; i < Text.Length; i++)
            {
                var symbol = Text[i];
                if (!charsCountDic.ContainsKey(symbol))
                    charsCountDic.Add(symbol, 1);
                else
                    charsCountDic[symbol] = charsCountDic[symbol] + 1;
            }
            var sortedDic = charsCountDic.OrderBy(x => x.Value).ToArray();
            return sortedDic;

        }

        public Node MakeHuffmanTree(KeyValuePair<char, int>[] source)
        {
            var huffTable = source.Select(
                x =>
                    new KeyValuePair<Node, int>(
                        new Node() { Value = x.Key },
                        x.Value
                    )
               ).ToArray();
            return MakeTreeRecursivly(huffTable)[0].Key;
        }
        private KeyValuePair<Node, int>[] MakeTreeRecursivly(KeyValuePair<Node, int>[] source)
        {
            if (source.Length == 1)
                return source;

            var newNode = new Node();
            newNode.LeftNode = source[0].Key;
            newNode.RightNode = source[1].Key;
            var hufTable = new KeyValuePair<Node, int>[source.Length - 1];
            hufTable[0] = new KeyValuePair<Node, int>(newNode, source[0].Value + source[1].Value);
            for (int i = 2; i < source.Length; i++)
            {
                hufTable[i - 1] = source[i];
            }
            hufTable = hufTable.OrderBy(x => x.Value).ToArray();
            return MakeTreeRecursivly(hufTable);
        }
        public Dictionary<char, string> GetHuffmanTable(Node tree)
        {
            hufTable = new Dictionary<char, string>();
            GetHufCodeRecursivly(tree, "");

            return hufTable;
        }
        private void GetHufCodeRecursivly(Node tree, string code)
        {
            if (tree.RightNode != null)
            {
                 GetHufCodeRecursivly(tree.RightNode, code + '1');
            }

            if (tree.LeftNode != null)
                 GetHufCodeRecursivly(tree.LeftNode, code + '0');

            if (tree.Value.HasValue)
                hufTable.Add(tree.Value.Value, code);
        }

        public byte[] GetBynary(Dictionary<char, string> table, out int tail)
        {
            var bytesString = "";
            for(int i = 0; i<Text.Length;i++)
            {
                bytesString+= table[Text[i]];
            }

            tail = 8 - bytesString.Length % 8;
            for (int i=0; i< tail; i++)
            {
                bytesString += "0";
            }

            int nBytes =(int)Math.Ceiling((double)bytesString.Length / 8);
            var bytesAsStrings =
                Enumerable.Range(0, nBytes)
                          .Select(i => bytesString.Substring(8 * i, 8));
            byte[] bytes = bytesAsStrings.Select(s => Convert.ToByte(s, 2)).ToArray();
            return bytes;
        }
        public string RestorBynaryString(byte[] bytes, int tail)
        {
            var bitString = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                string bits = Convert.ToString(bytes[i], 2);
                if(bits.Length<8)
                {
                    var fillCount = 8 - bits.Length;
                    for (int j = 0; j< fillCount; j++)
                    {
                        bits = "0" + bits;
                    }
                }
                if(i== bytes.Length-1 && tail != 0)
                {
                    bits = bits.Substring(0, 8 - tail);
                }

                bitString += bits;
            }
            return bitString;
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
        public void Save(string path, Node tree, int tail, byte[] bytes)
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
            var bits = RestorBynaryString(compressedFile.Bytes, compressedFile.Tail);
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

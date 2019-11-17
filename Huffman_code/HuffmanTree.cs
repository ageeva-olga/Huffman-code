using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_code
{
    public class HuffmanTree
    {
        public Dictionary<char, string> hufTable { get; private set; }
        public Node Node { get; private set; }

        public void MakeHuffmanTree(KeyValuePair<char, int>[] source)
        {
            var huffTable = source.Select(
                x =>
                    new KeyValuePair<Node, int>(
                        new Node() { Value = x.Key },
                        x.Value
                    )
               ).ToArray();
            Node = MakeTreeRecursivly(huffTable)[0].Key;
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
    }
}

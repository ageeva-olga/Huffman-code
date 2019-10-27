using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_code
{
    public class CompressedFile
    {
        [JsonProperty("tr")]
        public Node Tree { get; set; }
        [JsonProperty("b")]
        public byte[] Bytes { get; set; }
        [JsonProperty("t")]
        public int Tail { get; set; }
        public CompressedFile(Node tree, byte[] bytes, int tail)
        {
            Tree = tree;
            Bytes = bytes;
            Tail = tail;

        }
    }
}

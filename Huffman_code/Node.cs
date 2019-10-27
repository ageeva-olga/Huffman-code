using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_code
{
    public class Node
    {
        [JsonProperty("l")]
        public Node LeftNode;
        [JsonProperty("r")]
        public Node RightNode;
        [JsonProperty("v")]
        public char? Value;

    }
}

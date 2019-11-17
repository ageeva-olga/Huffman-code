using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_code
{
    public class BinaryHelper
    {
        public byte[] GetBynary(string text, Dictionary<char, string> table, out int tail)
        {
            var bytesString = "";
            for (int i = 0; i < text.Length; i++)
            {
                bytesString += table[text[i]];
            }

            tail = 8 - bytesString.Length % 8;
            for (int i = 0; i < tail; i++)
            {
                bytesString += "0";
            }

            int nBytes = (int)Math.Ceiling((double)bytesString.Length / 8);
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
                if (bits.Length < 8)
                {
                    var fillCount = 8 - bits.Length;
                    for (int j = 0; j < fillCount; j++)
                    {
                        bits = "0" + bits;
                    }
                }
                if (i == bytes.Length - 1 && tail != 0)
                {
                    bits = bits.Substring(0, 8 - tail);
                }

                bitString += bits;
            }
            return bitString;
        }
    }
}

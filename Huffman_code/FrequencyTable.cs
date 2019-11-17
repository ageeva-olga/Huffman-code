using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_code
{
    public class FrequencyTable
    {
        public string Text { get; private set; }

        public FrequencyTable(string text)
        {
            Text = text;
        }

        public KeyValuePair<char, int>[] MakeFrequencyTable()
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
    }
}

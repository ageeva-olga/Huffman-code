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
        public HufCode(string text)
        {
            Text = text;
        }
        public void Save(string path)
        {
            using (var writetext = new StreamWriter(path))
            {
                writetext.WriteLine(string.Format(Text));
            }
        }
        public Dictionary<char, int> CountDictionary(string path)
        {
            using (var streamReader = new StreamReader(path))
            {
                var text = streamReader.ReadToEnd();
                var charsCountDic = new Dictionary<char, int>();
                for(int i = 0; i<text.Length-2; i++)
                {
                    var symbol = text[i];
                    if (!charsCountDic.ContainsKey(symbol))
                        charsCountDic.Add(symbol, 1);
                    else
                        charsCountDic[symbol] = charsCountDic[symbol] + 1;
                }
                var sortedDic = charsCountDic.OrderByDescending(x => x.Value).ToDictionary(x => x.Key);// Dictonary<char,int>() { Key, Value }
                return charsCountDic;
            }
        }
    }
}

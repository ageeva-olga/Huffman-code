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
    }
}

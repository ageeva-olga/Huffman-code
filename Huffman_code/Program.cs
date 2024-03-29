using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_code
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Error, I sould use this format: <key> <source path> <destination path>");
            }

            if (args[0] == "a")
            {
                var hufCode = new HufCode();
                hufCode.Text = hufCode.LoadText(args[1]);
                var byteArray = hufCode.CompressText();
                hufCode.Save(args[2], byteArray);
            }

            if (args[0] == "x")
            {
                var hufCode = new HufCode();

                var textResult = hufCode.Load(args[1]);
                hufCode.SaveText(args[2], textResult);
            }

            //Console.ReadKey();
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscreteMathLab10_A
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("isheap.in");
            int n = int.Parse(sr.ReadLine());
            int[] data = (from s in sr.ReadLine().Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                          select int.Parse(s)).ToArray();
            bool result = true;
            for (int i = 1; i <= n && result; i++)
            {
                if (i*2 <= n)
                    result &= data[i*2 - 1] >= data[i - 1];
                if (i*2 + 1 <= n)
                    result &= data[i*2] >= data[i - 1];
            }
            StreamWriter sw = new StreamWriter("isheap.out") {AutoFlush = true};
            sw.WriteLine(result ? "YES" : "NO");
        }
    }
}

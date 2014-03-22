using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscreteMathLab10_D
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("parking.in");
            string[] parts = sr.ReadLine().Split(' ');
            int n = int.Parse(parts[0]),
                m = int.Parse(parts[1]);
            int[] dsu = new int[n];
            for (int i = 0; i < n; i++)
            {
                dsu[i] = i;
            }
            StreamWriter sw = new StreamWriter("parking.out");
            for (int i = 0; i < m; i++)
            {
                parts = sr.ReadLine().Split(' ');
                if (parts[0] == "enter")
                {
                    sw.WriteLine(Enter(dsu, int.Parse(parts[1]) - 1) + 1);
                }
                if (parts[0] == "exit")
                {
                    Exit(dsu, int.Parse(parts[1]) - 1);
                }
            }
            sw.Close();
        }

        static int Get(int[] dsu, int i)
        {
            if (dsu[i] != i)
                dsu[i] = Get(dsu, dsu[i]);
            return dsu[i];
        }

        static int Enter(int[] dsu, int i)
        {
            int result = Get(dsu, i);
            dsu[result] = result == dsu.Length - 1 ? Get(dsu, 0) : Get(dsu, result + 1);
            return result;
        }

        static void Exit(int[] dsu, int i)
        {
            dsu[i] = i;
            for (int j = i > 0 ? i - 1 : dsu.Length - 1; dsu[j] != j; j--)
            {
                dsu[j] = i;
                if (j == 0)
                    j = dsu.Length;
            }
        }
    }
}

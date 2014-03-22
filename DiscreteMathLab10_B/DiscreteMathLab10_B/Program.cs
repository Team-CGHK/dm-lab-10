using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace DiscreteMathLab10_B
{
    class Program
    {
        static void Main()
        {
            string[] lines = File.ReadAllLines("dsu.in");
            int n = int.Parse(lines[0]);
            DSU dsu = new DSU();
            for (int i = 1; i <= n; ++i)
                dsu.AddSingleItem(i);
            StreamWriter sw = new StreamWriter("dsu.out");
            foreach (string s in lines.Skip(1))
            {
                string[] parts = s.Split(' ');
                if (parts[0] == "union")
                {
                    int a = int.Parse(parts[1]) - 1,
                        b = int.Parse(parts[2]) - 1;
                    dsu.Union(a,b);
                }
                else 
                if (parts[0] == "get")
                {
                    int a = int.Parse(parts[1]) - 1;
                    DSU.DsuSet x = dsu.Get(a);
                    sw.WriteLine("{0} {1} {2}", x.Min, x.Max, x.Count);
                }
            }
            sw.Close();
        }

        class DSU
        {
            public class DsuSet
            {
                public int Count,
                           Min,
                           Max,
                           Delegate;

                public DsuSet()
                {
                }

                public DsuSet(int value)
                {
                    Count = 1;
                    Min = Max = value;
                    Delegate = -1;
                }
            }

            private int FindDelegate(int i)
            {
                DsuSet ith = _sets[i];
                if (ith.Delegate == -1)
                    return i;
                return ith.Delegate = FindDelegate(ith.Delegate);
            }

            private readonly List<DsuSet> _sets = new List<DsuSet>();   

            public void AddSingleItem(int value)
            {
                _sets.Add(new DsuSet(value));
            }

            public void Union(int a, int b)
            {
                a = FindDelegate(a);
                b = FindDelegate(b);
                if (a == b) return;
                _sets[a].Count = _sets[a].Count + _sets[b].Count;
                _sets[a].Max = Math.Max(_sets[a].Max, _sets[b].Max);
                _sets[a].Min = Math.Min(_sets[a].Min, _sets[b].Min);
                _sets[b].Delegate = a;
            }

            public DsuSet Get(int a)
            {
                //possible code: return _sets[FindDelegate(a)];
                //but it breaks OOP
                a = FindDelegate(a);
                var result = new DsuSet
                {
                    Count = _sets[a].Count,
                    Max = _sets[a].Max,
                    Min = _sets[a].Min
                };
                return result;
            }
        }
    }
}

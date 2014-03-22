using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DiscreteMathLab10_C
{
    class Program
    {
        private enum Command
        {
            Push,
            ExtractMin,
            DecreaseKey
        };

        static void Main(string[] args)
        {
            StreamWriter sw = new StreamWriter("priorityqueue.out");
            HeapInt32 h = new HeapInt32();
            foreach (var query in
                from s in File.ReadAllLines("priorityqueue.in")
                let parts = s.Split(' ')
                select new
                {
                    Command = parts[0] == "push"
                                  ? Command.Push
                                  : parts[0] == "extract-min"
                                        ? Command.ExtractMin
                                        : Command.DecreaseKey,
                    Arg0 = parts[0] != "extract-min" ? int.Parse(parts[1]) : 0,
                    Arg1 = parts[0] == "decrease-key" ? int.Parse(parts[2]) : 0
                })
            {
                if (query.Command == Command.Push)
                    h.Push(query.Arg0);
                if (query.Command == Command.ExtractMin)
                {
                    int min = h.ExtractMin();
                    sw.WriteLine(min != int.MaxValue ? min.ToString() : "*");
                }
                if (query.Command == Command.DecreaseKey)
                {
                    h.DecreaseKey(query.Arg0, query.Arg1);
                }
            }
            sw.Close();
        }
    }

    internal class HeapInt32
    {
        public int Operations = 0;
        public int Size { get; private set; }
        class HeapItem
        {
            public int Value,
                       OperationNumber;
        }

        private HeapItem[] items = new HeapItem[4];
        private int capacity = 4;

        public void Push(int item)
        {
            if (Size == capacity)
            {
                HeapItem[] extendedArray = new HeapItem[capacity * 2];
                for (int i = 0; i < capacity; i++)
                    extendedArray[i] = items[i];
                items = extendedArray;
                capacity *= 2;
            }
            Size++;
            items[Size - 1] = new HeapItem() { Value = item, OperationNumber = Operations};
            SiftUp(Size - 1);
            Operations++;
        }

        public int ExtractMin()
        {
            Operations++;
            if (Size > 0)
            {
                int result = items[0].Value;
                items[0] = items[Size - 1];
                Size--;
                SiftDown(0);
                return result;
            }
            else
                return int.MaxValue; //nothing
        }

        public void DecreaseKey(int operationNumber, int newKey)
        {
            Operations++;
            int index = -1;
            for (int i = 0; i < Size; i++)
                if (items[i].OperationNumber == operationNumber - 1)
                {
                    index = i;
                    break;
                }
            items[index].Value = newKey;
            SiftUp(index);
        }

        private void SiftUp(int index)
        {
            if (index == 0) return;
            if (items[index].Value <= items[(index - 1) / 2].Value)
            {
                HeapItem t = items[index];
                items[index] = items[(index - 1) / 2];
                items[(index - 1) / 2] = t;
                SiftUp((index - 1) / 2);
            }
        }

        private void SiftDown(int index)
        {
            int l = int.MaxValue;
            int r = int.MaxValue;
            if (index * 2 + 1 < Size) l = items[index * 2 + 1].Value;
            if (index * 2 + 2 < Size) r = items[index * 2 + 2].Value;
            if (r <= l && r <= items[index].Value)
            {
                HeapItem i = items[index * 2 + 2];
                items[index * 2 + 2] = items[index];
                items[index] = i;
                SiftDown(index * 2 + 2);
            }
            else
                if (l < items[index].Value)
                {
                    HeapItem i = items[index * 2 + 1];
                    items[index * 2 + 1] = items[index];
                    items[index] = i;
                    SiftDown(index * 2 + 1);
                }
        }
    }
}
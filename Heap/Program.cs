
using System;
using System.Collections.Generic;

namespace Heap
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var data = new int[] { 5, 3, 2, 8, 9, 10 };

            // Sort using Heapsort
            data.HeapSort();

            // Print result
            Console.WriteLine("data:");
            foreach (var v in data)
                Console.WriteLine(v);
            Console.WriteLine();

            var list = new List<int>(){ 5, 3, 2, 8, 9, 10};

            list.MakeHeap();

            // Add element and make it a heap again
            list.Add(70);
            list.PushHeap();

            // Add element and make it a heap again
            list.Add(99);
            list.PushHeap();

            // Print result
            Console.WriteLine("list:");
            foreach (var v in list)
                Console.WriteLine(v);
            Console.WriteLine();
        }
    }
}

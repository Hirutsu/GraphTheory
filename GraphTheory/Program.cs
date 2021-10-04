using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = @"C:\Users\burchuladzela\Desktop\GraphTheory\GraphTheory\InPutGraph.txt";
            Graph graph = new Graph(str);
            graph.ShowToConsole();
        }
    }
}

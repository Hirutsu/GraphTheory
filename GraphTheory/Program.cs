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
            string str = @"G:\GIT\GraphTheory\GraphTheory\Graph.txt";
            Graph graph = new Graph(str);
            graph.ShowToConsole();

            graph.DeleteVertex("5");
            Console.WriteLine();

            graph.ShowToConsole();
            Console.WriteLine();

            graph.DeleteEdge("1","2");
            Console.WriteLine();
            graph.ShowToConsole();

            graph.Oriented = false;
            graph.DeleteEdge("3", "4");
            Console.WriteLine();
            graph.ShowToConsole();

            str = @"G:\GIT\GraphTheory\GraphTheory\OutPutGraph.txt";
            graph.OutPutInFile(str);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory
{
    class Graph
    {
        private Dictionary<string, Dictionary<string, int>> _graph;
        private bool _oriented = true;

        public bool Oriented
        {
            get
            {
                return _oriented;
            }
            set
            {
                _oriented = value;
            }
        }

        public Dictionary<string, Dictionary<string, int>> DictGraph
        {
            get
            {
                return _graph;
            }

        }

        //конструктор по умолчанию
        public Graph()
        {
            _graph = new Dictionary<string, Dictionary<string, int>>();
        }

        //конструктор добавления из файла
        public Graph(string filePath = @"G:\GIT\GraphTheory\GraphTheory\Graph.txt")
        {
            _graph = new Dictionary<string, Dictionary<string, int>>();
            using (StreamReader file = new StreamReader(filePath, Encoding.GetEncoding(1251)))
            {
                string tempStr;
                string[] nodeStr;
                Dictionary<string, int> nodes = new Dictionary<string, int>();
                while ((tempStr = file.ReadLine()) != null)
                {
                    string[] nodesStr = tempStr.Split();
                    Dictionary<string, int> tempNextNodes = new Dictionary<string, int>();
                    for(int i = 1; i<nodesStr.Length;i++)
                    {
                        nodeStr = nodesStr[i].Split(':');
                        tempNextNodes.Add(nodeStr[0], Convert.ToInt32(nodeStr[1]));
                    }
                    _graph.Add(nodesStr[0], tempNextNodes);
                }
            }
        }

        //добавление вершины
        public void AddVertex(string nameNode)
        {
            _graph.Add(nameNode, null);
        }

        //добавление ребра
        public void AddEdge(string fromNode, string toNode, int weight)
        {
            if(_oriented)
            {
                _graph[fromNode].Add(toNode, weight);
            }
            else
            {
                _graph[fromNode].Add(toNode, weight);
                _graph[toNode].Add(fromNode, weight);
            }
        }

        //удаление вершины
        public void DeleteVertex(string nameNode)
        {
            _graph.Remove(nameNode);
            foreach (var keyValue in _graph)
            {
                _graph[keyValue.Key].Remove(nameNode);
            }
        }

        //удаление ребра
        public void DeleteEdge(string fromNode,string toNode)
        {
            if(_oriented)
            {
                foreach (var keyValue in _graph)
                {
                    if(keyValue.Key.Equals(fromNode))
                    {
                        _graph[keyValue.Key].Remove(toNode);
                    }
                }
            }
            else
            {
                foreach (var keyValue in _graph)
                {
                    if (keyValue.Key.Equals(fromNode))
                    {
                        _graph[keyValue.Key].Remove(toNode);
                    }
                    if (keyValue.Key.Equals(toNode))
                    {
                        _graph[keyValue.Key].Remove(fromNode);
                    }
                }

            }
        }

        //вывод в консоль
        public void ShowToConsole()
        {
            foreach (var keyValue in _graph)
            {
                Console.Write(keyValue.Key +" ");
                foreach(var keyValue2 in keyValue.Value)
                {
                    Console.Write(keyValue2.Key + ":" + keyValue2.Value + " ");
                }
                Console.WriteLine();
            }
        }

        //вывод в файл
        public void OutPutInFile(string str)
        {
            string oneNode;
            using (StreamWriter FileOut = new StreamWriter(str, false, Encoding.GetEncoding(1251)))
            {
                foreach (var keyValue in _graph)
                {
                    oneNode = keyValue.Key + " ";
                    foreach (var keyValue2 in keyValue.Value)
                    {
                        oneNode += (keyValue2.Key + ":" + keyValue2.Value + " ");
                    }
                    FileOut.WriteLine(oneNode);
                }
            }
        }
    }
}

using System;
using System.Collections;
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
        private bool _weighed = true;

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

        public bool Weighed
        {
            get
            {
                return _weighed;
            }
            set
            {
                _weighed = value;
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
        public Graph(string filePath)
        {
            _graph = new Dictionary<string, Dictionary<string, int>>();
            using (StreamReader file = new StreamReader(@filePath, Encoding.GetEncoding(1251)))
            {
                string[] orAndWei = file.ReadLine().Split();

                if (orAndWei[0] == "1")
                    _oriented = true;
                else
                    _oriented = false;

                if (orAndWei[1] == "1")
                    _weighed = true;
                else
                    _weighed = false;

                string tempStr;
                string[] nodeStr;
                string[] nodesStr;
                while ((tempStr = file.ReadLine()) != null)
                {
                    nodesStr = tempStr.Split();
                    Dictionary<string, int> tempNextNodes = new Dictionary<string, int>();
                    for (int i = 1; i < nodesStr.Length; i++)
                    {
                        nodeStr = nodesStr[i].Split(':');
                        tempNextNodes.Add(nodeStr[0], Convert.ToInt32(nodeStr[1]));
                    }
                    _graph.Add(nodesStr[0], tempNextNodes);
                }
            }
        }

        //добавление вершины
        public bool AddVertex(string nameNode)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            if(_graph.ContainsKey(nameNode))
            {
                return false;
            }
            {
                _graph.Add(nameNode, dict);
                return true;
            }
        }

        //добавление ребра
        public bool AddEdge(string fromNode, string toNode, int weight = 0)
        {
            if(_oriented)
            {
                if (_graph[fromNode].ContainsKey(toNode) == false && _graph[fromNode].ContainsKey(toNode) == false)
                {
                    _graph[fromNode].Add(toNode, weight);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if(_graph.ContainsKey(fromNode) && _graph.ContainsKey(toNode))
                {
                    if(_graph[fromNode].ContainsKey(toNode) == false && _graph[fromNode].ContainsKey(toNode) == false)
                    {
                        _graph[fromNode].Add(toNode, weight);
                        _graph[toNode].Add(fromNode, weight);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        //удаление вершины
        public bool DeleteVertex(string nameNode)
        {
            bool deleted = _graph.Remove(nameNode);
            foreach (var keyValue in _graph)
            {
                _graph[keyValue.Key].Remove(nameNode);
            }
            if(deleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //удаление ребра
        public bool DeleteEdge(string fromNode,string toNode)
        {
            bool deletedEdge = false;
            if(String.IsNullOrEmpty(fromNode) || String.IsNullOrEmpty(toNode))
            {
                return false;
            }
            if(_oriented)
            {
                foreach (var keyValue in _graph)
                {
                    if (keyValue.Key.Equals(fromNode) && keyValue.Value.ContainsKey(toNode))
                    {
                        deletedEdge = true;
                        _graph[keyValue.Key].Remove(toNode);
                    }
                }
            }
            else
            {
                foreach (var keyValue in _graph)
                {
                    if (keyValue.Key.Equals(fromNode) && keyValue.Value.ContainsKey(toNode) || keyValue.Key.Equals(toNode) && keyValue.Value.ContainsKey(fromNode))
                    {
                        deletedEdge = true;
                        _graph[keyValue.Key].Remove(toNode);
                        _graph[keyValue.Key].Remove(fromNode);
                    }
                }
            }
            return deletedEdge;
        }

        public Dictionary<string,bool> GetNoJoint(string node)
        {
            Dictionary<string, bool> degree = new Dictionary<string, bool>();
            foreach (var keyValue in _graph)
            {
                degree[keyValue.Key] = false;
            }
            degree[node] = true;
            foreach(var item in _graph)
            {
                if(item.Value.ContainsKey(node))
                {
                    degree[item.Key] = true;
                }
            }
            Dictionary<string, int> tempDic = _graph[node];
            foreach(var item in tempDic)
            {
                degree[item.Key] = true;
            }
            return degree;
        }

        public Dictionary<string,int> GetDegreeNode()
        {
            Dictionary<string, int> degree = new Dictionary<string, int>();
            foreach(var keyValue in _graph)
            {
                degree[keyValue.Key] = 0;
            }

            foreach (var keyValue in _graph)
            {
                foreach (var keyValue2 in keyValue.Value)
                {
                    if (degree.ContainsKey(keyValue2.Key))
                    {
                        degree[keyValue2.Key] += 1;
                    }
                }
            }
            return degree;
        }

        //вывод в консоль
        public void ShowToConsole()
        {
            Console.WriteLine();
            if (Oriented)
                Console.Write("1 ");
            else
                Console.Write("0");
            if (Weighed)
                Console.WriteLine("1");
            else
                Console.WriteLine("0");

            foreach (var keyValue in _graph)
            {
                Console.Write(keyValue.Key + "->");
                foreach(var keyValue2 in keyValue.Value)
                {
                    if(_weighed)
                    {
                        Console.Write(keyValue2.Key + ":" + keyValue2.Value + " ");
                    }
                    else
                    {
                        Console.Write(keyValue2.Key + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
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

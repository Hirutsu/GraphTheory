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
        //список смежности
        private Dictionary<string, Dictionary<string, int>> _graph;
        //(не)ориентированный
        private bool _oriented = true;
        //(не)взвешенный
        private bool _weighed = true;
        //свойство ориентированности
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
        //свойство взвешенности
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
        //получить весь граф
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

        //конструктор копирования
        public Graph(Graph graph)
        {
            _oriented = graph.Oriented;
            _weighed = graph.Weighed;
            _graph = new Dictionary<string, Dictionary<string, int>>();
            foreach (var item in graph.DictGraph)
            {
                string nameNode = "";
                nameNode += item.Key;
                Dictionary<string, int> vertex = new Dictionary<string, int>();
                foreach (var items in item.Value)
                {
                    vertex.Add(items.Key, items.Value);
                }
                _graph.Add(nameNode, vertex);
            }
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

        //поиск несмежных вершин с заданной
        public Dictionary<string,int> GetNoJoint(string node)
        {
            Dictionary<string, int> degree = new Dictionary<string, int>();
            foreach (var keyValue in _graph)
            {
                degree[keyValue.Key] = 0;
            }
            degree[node] = 1;
            foreach(var item in _graph)
            {
                if(item.Value.ContainsKey(node))
                {
                    degree[item.Key] += 1;
                }
            }
            Dictionary<string, int> tempDic = _graph[node];
            foreach(var item in tempDic)
            {
                degree[item.Key] += 1;
            }
            return degree;
        }

        //все вершины графа
        public string[] GetNodes()
        {
            string[] node = new string[_graph.Count];
            int i = 0;
            foreach(var item in _graph)
            {
                node[i] = item.Key;
                i++;
            }
            return node;
        }

        //подсчет степени выхода
        public int GetCountOutDegree(string node)
        {
            return _graph[node].Count;
        }

        //подсчет степени входа
        public int GetCountInDegreeNode(string node)
        {
            int count = 0;
            foreach (var keyValue in _graph)
            {
                foreach (var keyValue2 in keyValue.Value)
                {
                    if (keyValue2.Key == node)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        //компоненты связности
        public void BFS(ref Dictionary<string,bool> usedNodes){
            Queue<string> vertex = new Queue<string>();
            foreach (var item in usedNodes){
                if (item.Value == false){
                    vertex.Enqueue(item.Key);
                    break;
                }
            }
            while (vertex.Count != 0){
                string vrtx = vertex.Peek();
                if (usedNodes[vrtx] == false){
                    usedNodes[vrtx] = true;
                    foreach (var ver in _graph[vrtx]){
                        if (usedNodes.Keys.Contains(ver.Key) && usedNodes[ver.Key] == false){
                            usedNodes[ver.Key] = true;
                            vertex.Enqueue(ver.Key);
                        }
                    }
                }
                else{
                    vertex.Dequeue();
                }
            }
            Console.Write("Компонента: ");
            foreach (var component in usedNodes){
                if (component.Value == true)
                    Console.Write(component.Key + " ");
            }
            Dictionary<string, bool> tempDic = new Dictionary<string, bool>();
            foreach (var item in usedNodes){
                if (item.Value == false)
                    tempDic.Add(item.Key, item.Value);
            }
            usedNodes.Clear();
            foreach (var item in tempDic)
                usedNodes.Add(item.Key, item.Value);
            Console.WriteLine();
        }

        //вывод в консоль
        public void ShowToConsole()
        {
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

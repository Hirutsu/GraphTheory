using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory
{
    public class Edge
    {
        public string _start;
        public string _end;
        public int _cost;

        public Edge(string start, string end, int cost)
        {
            _start = start;
            _end = end;
            _cost = cost;
        }
    }

    class Graph
    {
        //список смежности
        private Dictionary<string, Dictionary<string, int>> _graph;
        //матрица смежности
        public int[,] matrix;
        //матрица предков
        private int[,] parent;
        //список ребер
        private List<Edge> edge;
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
        //получить матрицу
        public int[,] GetMatrix
        {
            get
            {
                return matrix;
            }
        }
        //получить матрицу
        public int[,] GetParent
        {
            get
            {
                return parent;
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

        //создание матрицы смежности
        public void SetMatrix()
        {
            matrix = new int[_graph.Count, _graph.Count];
            parent = new int[_graph.Count, _graph.Count];
            foreach (var items in _graph)
            {
                int i = Convert.ToInt32(items.Key);
                foreach (var item in items.Value)
                {
                    int j = Convert.ToInt32(item.Key);
                    matrix[i, j] = item.Value;
                }
            }
            for (int index = 0; index < matrix.GetLength(0); index++)
            {
                for (int jndex = 0; jndex < matrix.GetLength(1); jndex++)
                {
                    if (index == jndex || matrix[index, jndex] == 0)
                    {
                        matrix[index, jndex] = int.MaxValue;
                    }
                }
            }
        }

        //создание матрицы смежности
        public void SetParent()
        {
            parent = new int[_graph.Count, _graph.Count];
            for (int index = 0; index < parent.GetLength(0); index++)
            {
                for (int jndex = 0; jndex < parent.GetLength(1); jndex++)
                {
                    if(matrix[index,jndex] == int.MaxValue)
                    {
                        parent[index, jndex] = -1;
                    }
                    else
                    {
                        parent[index, jndex] = index + 1;
                    }
                }
            }
        }


        //создание списка ребер
        public void SetEdge()
        {
            edge = new List<Edge>();
            foreach(var items in _graph)
            {
                foreach(var item in items.Value)
                {
                    Edge tmp = new Edge(items.Key,item.Key, item.Value);
                    edge.Add(tmp);
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

        //обход в ширину
        public List<string> BFS(ref Dictionary<string, bool> usedNodes)
        {
            Queue<string> nodes = new Queue<string>();
            string tmp ="";
            foreach (var item in usedNodes)
            {
                if (item.Value == false)
                {
                    tmp = item.Key;
                    nodes.Enqueue(item.Key);
                    break;
                }
            }
            usedNodes[tmp] = true;

            while (nodes.Count != 0){
                string vrtx = nodes.Peek();
                nodes.Dequeue();
                foreach (var ver in _graph[vrtx])
                {
                    if (usedNodes.Keys.Contains(ver.Key) && usedNodes[ver.Key] == false)
                    {
                        usedNodes[ver.Key] = true;
                        nodes.Enqueue(ver.Key);
                    }
                }
            }
            List<string> lst = new List<string>();
            foreach (var component in usedNodes){
                if (component.Value == true)
                    lst.Add(component.Key);
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
            return lst;
        }

        //обход в глубину
        public void DFS(ref Dictionary<string, bool> usedNodes)
        {
            Queue<string> vertex = new Queue<string>();
            foreach (var item in usedNodes)
            {
                if (item.Value == false)
                {
                    vertex.Enqueue(item.Key);
                    break;
                }
            }
        }

        //алгоритм Прима
        public List<Edge> Prima(List<string> notUseV)
        {
            //неиспользованные ребра
            SetEdge();
            //использованные вершины
            List<string> useV = new List<string>();
            //неиспользованные вершины
            List<Edge> MST = new List<Edge>();
            useV.Add(notUseV[0]);
            notUseV.Remove(notUseV[0]);
            string v;
            string curV = notUseV[0];
            while (notUseV.Count > 0)
            {
                int minE = -1;
                int min = int.MaxValue;
                //поиск наим ребра
                for (int i = 0; i < edge.Count; i++)
                {
                    if (useV.IndexOf(edge[i]._start) != -1 && useV.IndexOf(edge[i]._end) == -1)
                    {
                        if (min > edge[i]._cost)
                        {
                            min = edge[i]._cost;
                            curV = edge[i]._end;
                            minE = i;
                        }
                    }
                }
                v = curV;
                useV.Add(v);
                notUseV.Remove(notUseV[0]);
                if(minE >= 0)
                {
                    MST.Add(edge[minE]);
                }
            }
            return MST;
        }

        //алгоритм Флойда
        public void Floyd()
        {
            for (int k = 0; k < _graph.Count; k++)
            {
                for (int i = 0; i < _graph.Count; i++)
                {
                    for (int j = 0; j < _graph.Count; j++)
                    {
                        if(matrix[i,k] < int.MaxValue && matrix[k,j] < int.MaxValue)
                        {
                            matrix[i, j] = Math.Min(matrix[i,j], matrix[i,k] + matrix[k, j]);
                            parent[i, j] = parent[i,k];
                        }
                    }
                }
            }
        }

        //алгоритм Дейкстры
        public List<int> Dijkstra(int st)
        {
            List<int> minWeight = new List<int>(_graph.Count);
            for (int i = 0; i < _graph.Count; i++)
            {
                minWeight.Add(0);
            }
            bool[] visited = new bool[_graph.Count];
            for (int i = 0; i < _graph.Count; i++)
            {
                if (_graph.ContainsKey(Convert.ToString(st)) && _graph[Convert.ToString(st)].ContainsKey(Convert.ToString(i)))
                {
                    minWeight[i] = _graph[Convert.ToString(st)][Convert.ToString(i)];
                }    
                else
                {
                    minWeight[i] = int.MaxValue;
                }
                visited[i] = false;
            }
            minWeight[st] = 0;
            int index = 0;
            int u = 0;

            List<int> p = new List<int>();
            for (int k = 0; k < _graph.Count; k++)
            {
                p.Add(-1);
            }
            p[st] = st;
            for (int i = 0; i < _graph.Count; i++)
            {
                int min = int.MaxValue;
                for (int j = 0; j < _graph.Count; j++)
                {
                    if (!visited[j] && minWeight[j] < min)
                    {
                        min = minWeight[j];
                        index = j;
                    }
                }
                u = index;
                visited[u] = true;

                for (int j = 0; j < _graph.Count; j++)
                {
                    if (_graph.ContainsKey(Convert.ToString(u)) && _graph[Convert.ToString(u)].ContainsKey(Convert.ToString(j)))
                    {
                        int value = _graph[Convert.ToString(u)][Convert.ToString(j)];
                        if (_graph.ContainsKey(Convert.ToString(u)) && _graph[Convert.ToString(u)].ContainsKey(Convert.ToString(j)))
                        {
                            if (!visited[j] && value != int.MaxValue && minWeight[u] != int.MaxValue && (minWeight[u] + value < minWeight[j]))
                            {
                                minWeight[j] = minWeight[u] + value;
                            }    
                            p[j] = u;
                        }
                    }
                }
            }
            return minWeight;
        }

        //алгоритм Беллмана-Форда
        public void BelmanFord(string st,string v)
        {
            Dictionary<string, int> d = new Dictionary<string, int>();
            foreach(var item in _graph)
            {
                d.Add(item.Key,int.MaxValue);
            }
            d[st] = 0;

            Dictionary<string,string> p = new Dictionary<string, string>();
            foreach (var item in _graph)
            {
                p.Add(item.Key,"");
            }
            string x = "";
            for (int i=0; i< _graph.Count; i++)
            {
                x = "";
                for(int j=0; j<edge.Count;j++)
                {
                    if (d[edge[j]._start] < int.MaxValue)
                    {
                        if (d[edge[j]._end] > d[edge[j]._start] + edge[j]._cost)
                        {
                            d[edge[j]._end] = Math.Max(int.MinValue,d[edge[j]._start] + edge[j]._cost);
                            p[edge[j]._end] = edge[j]._start;
                            x = edge[j]._end;
                        }
                    }
                }
            }
            if (d[v] == int.MaxValue)
            {
                Console.WriteLine("Пути нет");
                return;
            }
            if(x == "")
            {
                List<string> path = new List<string>();
                for (string cur = v; cur != ""; cur = p[cur])
                {
                    path.Add(cur);
                }
                path.Reverse();
                Console.WriteLine();
                for (int i = 0; i < path.Count; i++)
                {
                    Console.Write(path[i] + " ");
                }
            }
            else
            {
                Console.WriteLine("Есть отрицательный цикл");
                string y = x;
                for(int i=0;i< _graph.Count; i++)
                {
                    y = p[y];
                }
                List<string> path = new List<string>();
                for (string cur = y; ; cur = p[cur])
                {
                    path.Add(cur);
                    if (cur == y && path.Count > 1)
                    {
                        break;
                    }
                }
                path.Reverse();
                Console.WriteLine("Вывод отрицательного цикла");
                for(int i=0;i<path.Count;i++)
                {
                    Console.Write(path[i] + " ");
                }
            }
        }

        //вывод в консоль список смежности
        public void ShowLstVertex()
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

        //вывод матрицы смежности
        public void ShowMatrix()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i,j] == int.MaxValue)
                    {
                        Console.Write("oo\t");
                    }
                    else
                    {
                        Console.Write(matrix[i,j]+ "\t");
                    }
                }
                Console.WriteLine();
            }
        }

        //вывод списка ребер
        public void ShowEdge()
        {
            foreach(var item in edge)
            {
                Console.WriteLine("({0},{1}):{2}", item._start, item._end, item._cost);
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

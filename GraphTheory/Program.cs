using System;
using System.Collections;
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
            start:
            int num = 1;
            Console.WriteLine("Чтобы работать с графом нужно его создать:");
            Console.Write("Нажмите 1 -создать пустой граф; 2 - считать с файла: ");
            Int32.TryParse(Console.ReadLine(), out num);
            Graph graph = new Graph();
            if(num == 1)
            {
                graph = new Graph();
                Console.Write("Граф будет ориентированным? ");
                string str = Console.ReadLine();
                if(str == "Да")
                {
                    graph.Oriented = true;
                }
                else if (str =="Нет")
                {
                        graph.Oriented = false;
                }
                else
                {
                    Console.WriteLine("Не понимаю вас,введите заново");
                    goto start;
                }
                Console.Write("Граф будет взвешенным? ");
                str = Console.ReadLine();
                if (str == "Да")
                {
                    graph.Weighed = true;
                }
                else if (str == "Нет")
                {
                    graph.Weighed = false;
                }
                else
                {
                    Console.WriteLine("Не понимаю вас,введите заново");
                    goto start;
                }
            }
            else if (num == 2)
            {
                Console.WriteLine("Выберите файл: ");
                Console.WriteLine("1.Ориентированный взвешенный: ");
                Console.WriteLine("2.Ориентированный невзвешенный: ");
                Console.WriteLine("3.Неориентированный взвешенный: ");
                Console.WriteLine("4.Неориетированный невзвешенный: ");
                Console.WriteLine("5.Для компоненты связности: ");
                string filePath = Console.ReadLine();
                if (filePath == "1")
                    graph = new Graph(@"G:\GIT\GraphTheory\GraphTheory\OriWeiGraph.txt");
                else if (filePath == "2")
                    graph = new Graph(@"G:\GIT\GraphTheory\GraphTheory\OriNoWeiGraph.txt");
                else if (filePath == "3")
                    graph = new Graph(@"G:\GIT\GraphTheory\GraphTheory\NoOriWei.txt");
                else if (filePath == "4")
                    graph = new Graph(@"G:\GIT\GraphTheory\GraphTheory\NoOriNoWei.txt");
                else if (filePath == "5")
                    graph = new Graph(@"G:\GIT\GraphTheory\GraphTheory\ComponentOri.txt");
                else
                {
                    Console.WriteLine("Нет такого числа");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Неправильно,начните заново");
                goto start;
            }
            while(num != 0)
            {
                Console.WriteLine("Меню для работы с графом:");
                Console.WriteLine("1.Добавить вершину");
                Console.WriteLine("2.Удалить вершину");
                Console.WriteLine("3.Добавить ребро");
                Console.WriteLine("4.Удалить ребро");
                Console.WriteLine("5.Просмотреть граф в консоли");
                Console.WriteLine("6.Загрузить в файл");
                Console.WriteLine("7.Вывести все изолированные вершины");
                Console.WriteLine("8.Вывести все вершины,не смежные с данной");
                Console.WriteLine("9.Удалить висячие вершины");
                Console.WriteLine("10.Вывести компонент связности!");
                Console.WriteLine("11.Найти вершину, сумма длин кратчайших путей от которой до остальных вершин минимальна.(6) - ДЕЙКСТРА");
                Console.WriteLine("12.Вывести кратчайшие пути из вершин u1 и u2 до v.(15) - ФЛОЙД");
                Console.WriteLine("13.Вывести кратчайшие пути из вершины u до v1 и v2.(13) - БЕЛМАН");
                Console.WriteLine("0.Выйти из програамы");
                Console.Write("Введите число:");
                Int32.TryParse(Console.ReadLine(),out num);
                string str;
                string[] strArr;
                switch (num)
                {
                    case 1:
                        Console.WriteLine();
                        Console.Write("Введите название вершнины,которую нужно добавить: ");
                        str = Console.ReadLine();
                        if(graph.AddVertex(str))
                        {
                            Console.WriteLine("Вершина добавлена");
                        }
                        else
                        {
                            Console.WriteLine("Такая вершина уже есть! Добавлять можно только новые");
                        }
                        Console.WriteLine();
                        break;
                    case 2:
                        Console.WriteLine();
                        Console.Write("Введите название вершины,которую нужно удалить: ");
                        str = Console.ReadLine();
                        if (graph.DeleteVertex(str))
                        {
                            Console.WriteLine("Вершина удалена");
                        }
                        else
                        {
                            Console.WriteLine("Вершина не удалена,проверьте данные");
                        }
                        Console.WriteLine();
                        break;
                    case 3:
                        Console.WriteLine();
                        if (graph.Weighed)
                        {
                            Console.Write("Введите ('из', 'куда', 'вес') для добавления ребра: ");
                            strArr = Console.ReadLine().Split();
                            string[] vertex = graph.GetNodes();
                            if (strArr.Length == 3 && int.TryParse(strArr[2], out _) && vertex.Contains(strArr[0]) && vertex.Contains(strArr[1]))
                            {
                                if (graph.AddEdge(strArr[0], strArr[1], Convert.ToInt32(strArr[2])))
                                {
                                    Console.WriteLine("Ребро добавлено");
                                }
                                else
                                {
                                    Console.WriteLine("Нет вершины или есть ребро, добавление ребра не выполнено");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Введены неверно данные");
                            }
                        }
                        else
                        {
                            Console.Write("Введите ('из', 'куда') для добавления ребра: ");
                            strArr = Console.ReadLine().Split();
                            string[] vertex = graph.GetNodes();
                            if (strArr.Length == 2 && vertex.Contains(strArr[0]) && vertex.Contains(strArr[1]))
                            {
                                if (graph.AddEdge(strArr[0], strArr[1]))
                                {
                                    Console.WriteLine("Ребро добавлено");
                                }
                                else
                                {
                                    Console.WriteLine("Нет вершины или есть ребро, добавление ребра не выполнено");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Введены неверно данные");
                            }
                        }
                        Console.WriteLine();
                        break;
                    case 4:
                        Console.WriteLine();
                        Console.Write("Введите ('из', 'куда') для удаления ребра: ");
                        strArr = Console.ReadLine().Split();
                        if(strArr.Length == 2)
                        {
                            if (graph.DeleteEdge(strArr[0], strArr[1]))
                            {
                                Console.WriteLine("Ребро удалено");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ребро не удалено,проверьте данные");
                        }
                        Console.WriteLine();
                        break;
                    case 5:
                        Console.WriteLine();
                        graph.GetNodes();
                        graph.ShowLstVertex();
                        Console.WriteLine();
                        break;
                    case 6:
                        Console.WriteLine();
                        Console.WriteLine("Введите ссылку на файл");
                        str = Console.ReadLine();
                        Console.WriteLine();
                        break;
                    case 7:
                        Console.WriteLine();
                        Console.Write("Изолированные вершины: ");
                        string[] nodes = graph.GetNodes();
                        bool flag = false;
                        for(int i=0;i<nodes.Length;i++)
                        {
                            if(graph.GetCountInDegreeNode(nodes[i])==0 && graph.GetCountOutDegree(nodes[i]) == 0)
                            {
                                Console.Write(nodes[i] + " ");
                                flag = true;
                            }
                        }
                        if (flag == false)
                        {
                            Console.Write("Таких вершин нет");
                        }
                        Console.WriteLine();
                        break;
                    case 8:
                        Console.WriteLine();
                        Console.Write("Введите вершину:");
                        str = Console.ReadLine();
                        Dictionary<string,int> dictionary = graph.GetNoJoint(str);
                        Console.Write("Не смежные вершины с вершиной {0}: ", str);
                        foreach(var item in dictionary)
                        {
                            if(item.Value == 0)
                            {
                                Console.Write(item.Key+" ");
                            }
                        }
                        Console.WriteLine();
                        break;
                    case 9:
                        Console.WriteLine();
                        Console.WriteLine("Происходит удаление висячих вершин,подождите... ");
                        Graph newGraph = new Graph(graph);
                        nodes = graph.GetNodes();
                        flag = false;
                        List<string> hangNodes = new List<string>();
                        for (int i = 0; i < nodes.Length; i++){
                            if(graph.Oriented){
                                if (graph.GetCountInDegreeNode(nodes[i]) == 1 && graph.GetCountOutDegree(nodes[i]) == 0){
                                    hangNodes.Add(nodes[i]);
                                    flag = true;
                                }
                            }
                            else{
                                if (graph.GetCountInDegreeNode(nodes[i]) == 1 && graph.GetCountOutDegree(nodes[i]) == 1){
                                    hangNodes.Add(nodes[i]);
                                    flag = true;
                                }
                            }

                        }
                        if (flag == false){
                            Console.Write("Таких вершин нет");
                        }
                        else{
                            foreach(var item in hangNodes){
                                newGraph.DeleteVertex(item);
                            }
                            Console.WriteLine("Удаление завершено,теперь такой граф:");
                            newGraph.ShowLstVertex();
                            Console.WriteLine("Исходный граф");
                            graph.ShowLstVertex();

                        }
                        Console.WriteLine();
                        break;
                    case 10:
                        Console.WriteLine();
                        Console.WriteLine("Происходит поиск компонент связности...");
                        nodes = graph.GetNodes();
                        Dictionary<string, bool> usedNodes = new Dictionary<string, bool>();
                        for (int i = 0; i< nodes.Length; i++)
                        {
                            usedNodes.Add(nodes[i],false);
                        }
                        while(usedNodes.Count != 0)
                        {
                            graph.BFS(ref usedNodes);
                        }
                        Console.WriteLine();
                        break;
                    case 11:
                        Console.WriteLine();
                        List<List<int>> minweight = new List<List<int>>();
                        for (int i = 0; i < graph.DictGraph.Count; i++)
                        {
                            minweight.Add(graph.Dijkstra(i));
                        }
                        int minSum = 0;
                        int count = 0;
                        int tmpSum = 0;
                        int index = 0;

                        for (int i = 0; i < minweight.Count; i++)
                        {
                            for (int j = 0; j < minweight[i].Count; j++)
                            {
                                Console.Write(minweight[i][j] + " ");
                            }
                            Console.WriteLine();
                        }

                        for (int i = 0; i < minweight.Count; i++)
                        {
                            count = 0;
                            tmpSum = 0;
                            for (int j = 0; j < minweight[i].Count; j++)
                            {
                                if(minweight[i][j] != int.MaxValue)
                                {
                                    tmpSum += minweight[i][j];
                                    count++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if(count == minweight.Count)
                            {
                                index = i;
                                minSum = tmpSum;
                            }
                        }
                        Console.WriteLine("Мин вершина - " +index +" с мин суммой до всех вершин - "+minSum);

                        Console.WriteLine();
                        break;
                    case 12:
                        Console.WriteLine();
                        graph.SetMatrix();
                        graph.ShowMatrix();
                        graph.SetParent();
                        int[,] prev = graph.GetParent;

                        for (int i = 0; i < prev.GetLength(1); i++)
                        {
                            for (int j = 0; j < prev.GetLength(1); j++)
                            {
                                Console.Write(prev[i, j] + " ");
                            }
                            Console.WriteLine();
                        }

                        graph.Floyd();
                        int[,] matrix = graph.GetMatrix;

                        for (int i = 0; i < matrix.GetLength(1); i++)
                        {
                            for (int j = 0; j < matrix.GetLength(1); j++)
                            {
                                Console.Write(matrix[i,j]+" ");
                            }
                            Console.WriteLine();
                        }

                        prev = graph.GetParent;

                        for (int i = 0; i < prev.GetLength(1); i++)
                        {
                            for (int j = 0; j < prev.GetLength(1); j++)
                            {
                                Console.Write(prev[i, j] + " ");
                            }
                            Console.WriteLine();
                        }

                        Console.WriteLine("Введите вершину u1: ");
                        int u1 = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Введите вершину u2: ");
                        int u2 = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Введите вершину v: ");
                        int v = Convert.ToInt32(Console.ReadLine());
                        if (matrix[u1, v] == int.MaxValue)
                        {
                            Console.WriteLine("Такого пути нет из u1 в v");
                        }
                        int c = u1;
                        while (c != v)
                        {
                            Console.WriteLine(c + " ");
                            c = prev[c, v];
                        }

                        if (matrix[u2, v] == int.MaxValue)
                        {
                            Console.WriteLine("Такого пути нет из u1 в v");
                        }
                        c = u2;
                        while (c != v)
                        {
                            Console.WriteLine(c + " ");
                            c = prev[c, v];
                        }
                        break;
                    case 13:
                        Console.WriteLine();
                        Console.WriteLine("Введите вершину u: ");
                        int u = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Введите вершину v1: ");
                        int v1 = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Введите вершину v2: ");
                        int v2 = Convert.ToInt32(Console.ReadLine());
                        graph.SetEdge();
                        Console.WriteLine();
                        graph.BelmanFord(u,v1);
                        Console.WriteLine();
                        graph.BelmanFord(u, v2);
                        Console.WriteLine();
                        break;
                    case 0:
                        Console.WriteLine();
                        Console.WriteLine("Выход из программы!");
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Вы ввели не то число!");
                        Console.WriteLine();
                        break;
                }
            }
        }
    }
}

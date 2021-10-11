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
                string filePath = Console.ReadLine();
                if (filePath == "1")
                    graph = new Graph(@"G:\GIT\GraphTheory\GraphTheory\OriWeiGraph.txt");
                else if (filePath == "2")
                    graph = new Graph(@"G:\GIT\GraphTheory\GraphTheory\OriNoWeiGraph.txt");
                else if (filePath == "3")
                    graph = new Graph(@"G:\GIT\GraphTheory\GraphTheory\NoOriWei.txt");
                else
                    graph = new Graph(@"G:\GIT\GraphTheory\GraphTheory\NoOriNoWei.txt");
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
                            if (strArr.Length == 3 && int.TryParse(strArr[2], out _))
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
                            if (strArr.Length == 2 && int.TryParse(strArr[2], out _))
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
                        graph.ShowToConsole();
                        Console.WriteLine();
                        break;
                    case 6:
                        Console.WriteLine();
                        Console.WriteLine("Введите ссылку на файл");
                        str = Console.ReadLine();
                        graph.OutPutInFile(str);
                        Console.WriteLine();
                        break;
                    case 7:
                        Console.WriteLine();
                        Console.Write("Изолированные вершины: ");
                        Dictionary<string,int> arr = graph.GetDegreeNode();
                        foreach (var item in arr)
                        {
                            if(item.Value == 0)
                            {
                                Console.Write(item.Key+" ");
                            }
                        }
                        Console.WriteLine();
                        break;
                    case 8:
                        Console.WriteLine();
                        Console.Write("Введите вершину:");
                        str = Console.ReadLine();
                        Dictionary<string,bool> dictionary = graph.GetNoJoint(str);
                        Console.Write("Не взвешенные вершины с вершиной {0}: ", str);
                        foreach(var item in dictionary)
                        {
                            if(item.Value == false)
                            {
                                Console.Write(item.Key+" ");
                            }
                        }
                        Console.WriteLine();
                        break;
                    case 9:
                        Console.WriteLine();
                        Console.WriteLine("Происходит удаление висячих вершин,подождите...");
                        arr = graph.GetDegreeNode();
                        foreach(var item in arr)
                        {
                            if(item.Value == 1)
                            {
                                graph.DeleteVertex(item.Key);
                            }
                        }
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

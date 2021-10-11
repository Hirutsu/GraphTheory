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
            start:
            int num = 1;
            Console.WriteLine("Чтобы работать с графом нужно его создать:");
            Console.Write("Нажмите 1 -создать пустой граф; 2 - считать с файла: ");
            num = Convert.ToInt32(Console.ReadLine());
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
                if(str =="Нет")
                {
                        graph.Oriented = false;
                }
                Console.Write("Граф будет взвешенным? ");
                str = Console.ReadLine();
                if (str == "Да")
                {
                    graph.Weighed = true;
                }
                if (str == "Нет")
                {
                    graph.Weighed = false;
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
                    graph = new Graph(@"C:\Users\burchuladzela\Desktop\GraphTheory\GraphTheory\OriWeiGraph.txt");
                else if (filePath == "2")
                    graph = new Graph(@"C:\Users\burchuladzela\Desktop\GraphTheory\GraphTheory\OriNoWeiGraph.txt");
                else if (filePath == "3")
                    graph = new Graph(@"C:\Users\burchuladzela\Desktop\GraphTheory\GraphTheory\NoOriWei.txt");
                else
                    graph = new Graph(@"C:\Users\burchuladzela\Desktop\GraphTheory\GraphTheory\NoOriNoWei.txt");
            }
            else
            {
                Console.WriteLine("Неправильно,начните заново");
                goto start;
            }
            while(num != 8)
            {
                Console.WriteLine("Меню для работы с графом:");
                Console.WriteLine("1.Добавить вершину");
                Console.WriteLine("2.Удалить вершину");
                Console.WriteLine("3.Добавить ребро");
                Console.WriteLine("4.Удалить ребро");
                Console.WriteLine("5.Просмотреть граф в консоли");
                Console.WriteLine("6.Загрузить в файл");
                Console.WriteLine("7.Вывести все изолированные вершины");
                Console.WriteLine("8.Выйти из програамы");
                Console.Write("Введите число:");
                num = Convert.ToInt32(Console.ReadLine());
                string str;
                string[] strArr;
                switch (num)
                {
                    case 1:
                        Console.WriteLine();
                        Console.Write("Введите название вершнины,которую нужно добавить: ");
                        str = Console.ReadLine();
                        graph.AddVertex(str);
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
                        Console.Write("Введите ('из', 'куда', 'вес') для добавления ребра: ");
                        strArr = Console.ReadLine().Split();
                        graph.AddEdge(strArr[0], strArr[1], Convert.ToInt32(strArr[2]));
                        Console.WriteLine();
                        break;
                    case 4:
                        Console.WriteLine();
                        Console.Write("Введите ('из', 'куда') для удаления ребра: ");
                        strArr = Console.ReadLine().Split();
                        if (graph.DeleteEdge(strArr[0], strArr[1]))
                        {
                            Console.WriteLine("Ребро удалено");
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
                        graph.ShowIsolatedNode();
                        Console.WriteLine();
                        break;
                    case 8:
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

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
            int num = 1;
            Console.WriteLine("Чтобы работать с графом нужно его создать:");
            Console.Write("Нажмите 1 -создать пустой граф; 2 - считать с файла: ");
            num = Convert.ToInt32(Console.ReadLine());
            Graph graph = new Graph();
            while (num != 1 && num != 2)
            if(num == 1)
            {
                graph = new Graph();
            }
            if (num == 2)
            {
                Console.Write("Введите путь к файлу: ");
                string filePath = Console.ReadLine();
                graph = new Graph(@"C:\Users\burchuladzela\Desktop\GraphTheory\GraphTheory\InPutGraph.txt");
            }
            while(num != 7)
            {
                Console.WriteLine("Меню для работы с графом:");
                Console.WriteLine("1.Добавить вершину");
                Console.WriteLine("2.Удалить вершину");
                Console.WriteLine("3.Добавить ребро");
                Console.WriteLine("4.Удалить ребро");
                Console.WriteLine("5.Просмотреть граф в консоли");
                Console.WriteLine("6.Загрузить в файл");
                Console.WriteLine("7.Выйти из програамы");
                Console.Write("Введите число:");
                num = Convert.ToInt32(Console.ReadLine());
                string str;
                switch (num)
                {
                    case 1:
                        Console.Write("Введите название вершнины: ");
                        str = Console.ReadLine();
                        graph.AddVertex(str);
                        break;
                    case 2:
                        Console.Write("Введите название вершины: ");
                        str = Console.ReadLine();
                        graph.DeleteVertex(str);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        graph.ShowToConsole();
                        break;
                    case 6:
                        break;
                    case 7:
                        Console.WriteLine("Выход из программы!");
                        break;
                    default:
                        Console.WriteLine("Вы ввели не то число!");
                        break;
                }
            }
        }
    }
}

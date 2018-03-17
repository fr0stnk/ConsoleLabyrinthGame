using System;
using System.Threading;

namespace LabAutomats
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            bool[,] boolArray = new bool[12, 12];
            string[,] stringArray = new string[12, 12];
            int coordX = 10;
            int coordY = 1;
            int steps = 1;

            // Препятствия лабиринта
            // Первая строчка
            boolArray[1, 8] = true;
            // Вторая строчка
            boolArray[2, 2] = true;
            boolArray[2, 8] = true;
            // Третья строчка
            boolArray[3, 2] = true;
            boolArray[3, 3] = true;
            boolArray[3, 5] = true;
            boolArray[3, 6] = true;
            // Четвертая строчка
            boolArray[4, 3] = true;
            boolArray[4, 10] = true;
            // Пятая строчка
            boolArray[5, 6] = true;
            boolArray[5, 7] = true;
            boolArray[5, 8] = true;
            boolArray[5, 9] = true;
            boolArray[5, 10] = true;
            // Шестая строчка
            boolArray[6, 2] = true;
            // Седьмая строчка
            boolArray[7, 5] = true;
            boolArray[7, 7] = true;
            boolArray[7, 8] = true;
            // Восьмая строчка
            boolArray[8, 4] = true;
            boolArray[8, 5] = true;
            boolArray[8, 7] = true;
            // Девятая строчка
            boolArray[9, 1] = true;
            boolArray[9, 2] = true;
            boolArray[9, 4] = true;
            boolArray[9, 5] = true;
            boolArray[9, 10] = true;
            // Десятая строчка
            boolArray[10, 9] = true;
            boolArray[10, 10] = true;

            //Границы лабиринта
            for (int x = 0; x < 12; x++)
            {
                for (int y = 0; y < 12; y++)
                {
                    boolArray[0, y] = true; // левая граница
                    boolArray[11, y] = true; // правая граница
                    boolArray[x, 0] = true; // верхняя граница
                    boolArray[x, 11] = true; // нижняя граница
                }
            }

            // Создание графики лабиринта
            stringArray = CreateGraphicalMap(boolArray, "#", "0");

            DrawMap(stringArray);
            Console.ReadKey();
            Console.WriteLine();


            while (!(coordX == 1 && coordY == 10))
            {
                Move(ref coordX, ref coordY, stringArray, boolArray);
                steps++;
            }

            Console.WriteLine($"\nПрограмма завершилась успешно!");
            Console.ReadKey();
        }

        // Движение
        static void Move(ref int x, ref int y, string[,] graphicalMap, bool[,] logicalMap)
        {
            if (CanMove(x - 1, y, logicalMap))
            {
                x--; // Движение вверх
                logicalMap[x, y] = true; // Метка о пройденном пути
                graphicalMap[x, y] = "X"; // Графика перемещения
                DrawMap(graphicalMap); // Отрисовка карты
            }
            else if (CanMove(x, y + 1, logicalMap))
            {
                y++; // Движение вправо
                logicalMap[x, y] = true;
                graphicalMap[x, y] = "X";
                DrawMap(graphicalMap);
            }
            else if (CanMove((x + 1), y, logicalMap))
            {
                x++; // Движение вниз
                logicalMap[x, y] = true;
                graphicalMap[x, y] = "X";
                DrawMap(graphicalMap);
            }
            else if (CanMove(x, y - 1, logicalMap))
            {
                y--; // Движение влево
                logicalMap[x, y] = true;
                graphicalMap[x, y] = "X";
                DrawMap(graphicalMap);
            }
        }

        // Проверка возможности перемещения
        static bool CanMove(int x, int y, bool[,] logicalMap)
        {
            return !logicalMap[x, y];
        }

        // Отрисовка карты
        static void DrawMap(string[,] graphicalMap)
        {
            Thread.Sleep(200);
            Console.Clear();

            for (int x = 1; x < 11; x++)
            {
                Console.WriteLine();
                for (int y = 1; y < 11; y++)
                {
                    Console.Write(graphicalMap[x, y]);
                }
            }
        }

        // Генерация графики для логической карты
        static string[,] CreateGraphicalMap(bool[,] logicMap, string wallSymbol = "#",
                                        string emptySymbol = "0", int startX = 10, int startY = 1)
        {
            string[,] graphicalMap = new string[12, 12];

            for (int x = 0; x < 11; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    if (logicMap[x, y])
                    {
                        graphicalMap[x, y] = wallSymbol;
                    }
                    else
                    {
                        graphicalMap[x, y] = emptySymbol;
                    }
                }
            }
            graphicalMap[startX, startY] = "X"; //Start point 
            return graphicalMap;
        }
    }

}

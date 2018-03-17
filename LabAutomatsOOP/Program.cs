using System;
using System.Threading;

namespace LabAutomatsOOP
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Map map1 = new Map(10, 10);

            // Препятствия лабиринта
            // Первая строчка
            map1.AddWall(1, 8);
            // Вторая строчка
            map1.AddWall(2, 2);
            map1.AddWall(2, 8);
            // Третья строчка
            map1.AddWall(3, 2);
            map1.AddWall(3, 3);
            map1.AddWall(3, 5);
            map1.AddWall(3, 6);
            // Четвертая строчка
            map1.AddWall(4, 3);
            map1.AddWall(4, 10);
            // Пятая строчка
            map1.AddWall(5, 6);
            map1.AddWall(5, 7);
            map1.AddWall(5, 8);
            map1.AddWall(5, 9);
            map1.AddWall(5, 10);
            // Шестая строчка
            map1.AddWall(6, 2);
            // Седьмая строчка
            map1.AddWall(7, 5);
            map1.AddWall(7, 7);
            map1.AddWall(7, 8);
            // Восьмая строчка
            map1.AddWall(8, 4);
            map1.AddWall(8, 5);
            map1.AddWall(8, 7);
            // Девятая строчка
            map1.AddWall(9, 1);
            map1.AddWall(9, 2);
            map1.AddWall(9, 4);
            map1.AddWall(9, 5);
            map1.AddWall(9, 10);
            // Десятая строчка
            map1.AddWall(10, 9);
            map1.AddWall(10, 10);

            Console.WriteLine("Выберите игру. \nДоступные варианты: \n1. Автопрохождение лабиринта\n2. Прохождение игроком");
            string gameChooser = Console.ReadLine();

            switch (gameChooser)
            {
                case "1":
                    Console.WriteLine("Автоматическое прохождение. Нажмите кнопку...");
                    map1.Draw();
                    Console.ReadKey();
                    map1.Game1(10, 1, 1, 10);
                    Console.ReadKey();
                    break;
                case "2":
                    Console.WriteLine("Прохождение игроком. Нажмите кнопку...");
                    map1.Draw();
                    Console.ReadKey();
                    map1.Game2(10, 1, 1, 10);
                    Console.ReadKey();
                    break;
                case "":
                    Console.WriteLine("Введите еще раз");
                    break;
            }
        }
    }

    internal class Map
    {
        private readonly string[,] _stringArray;
        private int _coordX;
        private int _coordY;
        private int _steps = 1;
        private char _pressedKey;

        /// <summary>
        /// Конструктор лабиринта по параметрам размера X и Y
        /// </summary>
        /// <param name="sizeX"></param>
        /// <param name="sizeY"></param>
        public Map(int sizeX, int sizeY)
        {
            _stringArray = new string[sizeX + 2, sizeY + 2];
            
            // Создание рамки вокруг лабиринта
            for (int x = 0; x < _stringArray.GetLength(0); x++)
            {
                for (int y = 0; y < _stringArray.GetLength(1); y++)
                {
                    _stringArray[x, y] = "0";
                    _stringArray[x, 0] = "|"; // Левая граница
                    _stringArray[x, _stringArray.GetLength(1) - 1] = "|"; // Правая граница
                    _stringArray[0, y] = "—"; // Верхняя граница
                    _stringArray[_stringArray.GetLength(0) - 1, y] = "—"; // Нижняя граница
                }
            }
        }

        /// <summary>
        /// Метод для добавления стен
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void AddWall(int x, int y)
        {
            if (x < _stringArray.GetLength(0) && y < _stringArray.GetLength(1))
            {
                _stringArray[x, y] = "#";
            }
        }
        
        /// <summary>
        /// Отрисовка карты с таймером (сделать более постоянный таймер)
        /// </summary>
        public void Draw()
        {
            Thread.Sleep(200);
            Console.Clear();
            Console.WriteLine("Карта лабиринта");
            Console.WriteLine($"Шагов сделано: {_steps}");
            for (int x = 0; x < _stringArray.GetLength(0); x++)
            {
                Console.WriteLine();
                for (int y = 0; y < _stringArray.GetLength(1); y++)
                {
                    Console.Write(_stringArray[x, y]);
                }
            }

            Console.Write("\nВвод: ");
        }
        
        /// <summary>
        /// Игра с автоматическим поиском пути от startX и StartY
        /// К точке finishX и FinishY
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="finishX"></param>
        /// <param name="finishY"></param>
        public void Game1(int startX, int startY, int finishX, int finishY)
        {
            _coordX = startX;
            _coordY = startY;
            _stringArray[_coordX, _coordY] = "X";

            bool CanMove(int x, int y)
            {
                return (_stringArray[x, y] == "0");
            }

            Console.WriteLine("\nИгра началась!");
            while (!(_coordX == finishX & _coordY == finishY))
            { 
                if (CanMove(_coordX - 1, _coordY))
                {
                    _coordX--; // Движение вверх
                    _stringArray[_coordX, _coordY] = "X"; // Графика перемещения
                    Draw(); // Отрисовка карты
                }
                else if (CanMove(_coordX, _coordY + 1))
                {
                    _coordY++; // Движение вправо
                    _stringArray[_coordX, _coordY] = "X";
                    Draw();
                }
                else if (CanMove(_coordX + 1, _coordY))
                {
                    _coordX++; // Движение вниз
                    _stringArray[_coordX, _coordY] = "X";
                    Draw();
                }
                else if (CanMove(_coordX, _coordY - 1))
                {
                    _coordY--; // Движение влево
                    _stringArray[_coordX, _coordY] = "X";
                    Draw();
                }

                _steps++;
            }

            Console.WriteLine("\nУровень пройден!");
        }

        /// <summary>
        /// Игра с управлением и поиском выхода
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="finishX"></param>
        /// <param name="finishY"></param>
        public void Game2(int startX, int startY, int finishX, int finishY)
        {
            _coordX = startX;
            _coordY = startY;
            _stringArray[_coordX, _coordY] = "X";

            bool CanMove(int x, int y)
            {
                return (_stringArray[x, y] == "0");
            }

            Console.WriteLine("\nИгра началась!");
            while (!(_coordX == finishX & _coordY == finishY))
            {
                
                _pressedKey = Console.ReadKey().KeyChar;
                
                if ((_pressedKey == 'w' || _pressedKey == 'W') && CanMove(_coordX - 1, _coordY))
                {
                    _coordX--; // Движение вверх
                    _stringArray[_coordX, _coordY] = "X"; // Графика перемещения
                    Draw(); // Отрисовка карты
                    _steps++;
                }
                else if ((_pressedKey == 'd' || _pressedKey == 'D') && CanMove(_coordX, _coordY + 1))
                {
                    _coordY++; // Движение вправо
                    _stringArray[_coordX, _coordY] = "X";
                    Draw();
                    _steps++;
                }
                else if ((_pressedKey == 's' || _pressedKey == 'S') && CanMove(_coordX + 1, _coordY))
                {
                    _coordX++; // Движение вниз
                    _stringArray[_coordX, _coordY] = "X";
                    Draw();
                    _steps++;
                }
                else if ((_pressedKey == 'a' || _pressedKey == 'A') && CanMove(_coordX, _coordY - 1))
                {
                    _coordY--; // Движение влево
                    _stringArray[_coordX, _coordY] = "X";
                    Draw();
                    _steps++;
                }
            }

            Console.WriteLine("\nУровень пройден!");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Figures
    {
        //Создаем фигуры - двумерные массивы
        public static int[,] I = new int[1, 4] { { 1, 1, 1, 1 } }; //0
        public static int[,] O = new int[2, 2] { { 1, 1 }, { 1, 1 } }; //1
        public static int[,] T = new int[3, 2] { { 0, 1 }, { 1, 1 }, { 0, 1 } };  //2
        public static int[,] S = new int[3, 2] { { 0, 1 }, { 1, 1 }, { 1, 0 } }; //3
        public static int[,] Z = new int[3, 2] { { 1, 0 }, { 1, 1 }, { 0, 1 } };  //4
        public static int[,] J = new int[2, 3] { { 1, 1, 1 }, { 0, 0, 1 } };  //5
        public static int[,] L = new int[2, 3] { { 0, 0, 1 }, { 1, 1, 1 } };  //6

        public static List<int[,]> tetrominoes = new List<int[,]>() { I, O, T, S, Z, J, L };
        public static List<Point> Figurepoints = new List<Point>(); //Список с координатами текущей фигуры
        public static List<Point> NextImageFigurepoints = new List<Point>(); //Список с координатами будущей фигуры
        public static List<Point> NextFigurepoints = new List<Point>();  //Список с сдвинутыми координатами
        public static List<Point> Floor = new List<Point>(); //Список с точками пола

        public static List<Point> PointsOnBulkOrFloor = new List<Point>();  //Список с точками для проверки, не на полу ли мы или на других точках

        private int[,] Figure;

        public Direction direction;

        public Figures(int NumberOfFigure)  //Конструктор создания фиугры
        {
            Figurepoints.Clear();
            Figure = tetrominoes.ElementAt(NumberOfFigure);
            for (int i = 0; i < Figure.GetLength(0); i++) //GetLength(0) - Возвращает 32-разрядное целое число, представляющее количество строк.
            {
                for (int j = 0; j < Figure.GetLength(1); j++) //GetLength(1) - Возвращает кол-во колонок
                {

                    if (Figure[i, j] == 1)
                    {
                        int x = (12 - Figure.GetLength(0)) + i;
                        int y = j;
                        //Создаем новую точку и добавляем ее в список точек
                        Point p = new Point(x, y);
                        Figurepoints.Add(p);
                    }
                }
            }
        }

        public void DrawFigure(List<Point> TEMP)
        {
            foreach (Point p in TEMP) //Для каждой точки в списке
            {
                p.Draw();
            }
        }

        public void MoveFigure()
        {
            for (int i = 0; i < 4; i++)   //убираем с экрана предыдущю точку
            {
                Figurepoints[i].Clear();  //Убираем первую, несдвинутую фигуру с экрана - на ее место выводим пустой символ
            }
            

            for (int i = 0; i < NextFigurepoints.Count; i++)
            {
                Figurepoints[i] = NextFigurepoints[i];
            }
            //DrawFigure(NextFigurepoints);
            DrawFigure(Figurepoints);
        }

        public void CalculateNextFigurepoints(int NumberOfFigure, Direction _direction)
        {
            NextFigurepoints.Clear();
            direction = _direction;

            if (direction == Direction.LEFT)
            {
                for (int i = 0; i < 4; i++)
                {
                    Point Temp = new Point(Figurepoints[i]); //Копируем точку из текущего реального положения фигуры для ее сдвига и проверки возможности движения фигуры
                    Temp.Move(1, direction);  //Сдвигаем координаты первой точки фигуры в выбранном направлении
                    NextFigurepoints.Insert(i, Temp);
                }
            }
            if (direction == Direction.RIGHT)
            {
                for (int i = 0; i < 4; i++)
                {
                    Point Temp = new Point(Figurepoints[i]); //Копируем точку из текущего реального положения фигуры для ее сдвига и проверки возможности движения фигуры
                    Temp.Move(1, direction);  //Сдвигаем координаты первой точки фигуры в выбранном направлении
                    NextFigurepoints.Insert(i, Temp);
                }
            }

            if (direction == Direction.DOWN)
            {
                for (int i = 0; i < 4; i++)
                {
                    Point Temp = new Point(Figurepoints[i]); //Копируем точку из текущего реального положения фигуры для ее сдвига и проверки возможности движения фигуры
                    Temp.Move(1, direction);  //Сдвигаем координаты первой точки фигуры в выбранном направлении
                    NextFigurepoints.Insert(i, Temp);
                }
            }

            if (direction == Direction.ROTATE)
            {
                switch (NumberOfFigure)
                {
                    case 0:
                        for (int i = 0; i < 4; i++)
                        {
                            Point Temp = new Point(Figurepoints[i]); //Берем каждую точку фигуры
                            Temp.Rotate(Figurepoints[i], Figurepoints[1]); //Отправляем точку на вращение вокруг точки вращения
                            NextFigurepoints.Insert(i, Temp);
                        }
                        break;
                    case 2:
                        for (int i = 0; i < 4; i++)
                        {
                            Point Temp = new Point(Figurepoints[i]); //Figurepoints[2] - точка вращения
                            Temp.Rotate(Figurepoints[i], Figurepoints[2]); //Figurepoints[3] - точка вращения
                            NextFigurepoints.Insert(i, Temp);
                        }
                        break;
                    case 3:
                        for (int i = 0; i < 4; i++)
                        {
                            Point Temp = new Point(Figurepoints[i]); //Берем каждую точку фигуры
                            Temp.Rotate(Figurepoints[i], Figurepoints[2]); //Figurepoints[3] - точка вращения
                            NextFigurepoints.Insert(i, Temp);
                        }
                        break;
                    case 4:
                        for (int i = 0; i < 4; i++)
                        {
                            Point Temp = new Point(Figurepoints[i]); //Берем каждую точку фигуры
                            Temp.Rotate(Figurepoints[i], Figurepoints[2]); //Отправляем точку на вращение вокруг точки вращения
                            NextFigurepoints.Insert(i, Temp);
                        }
                        break;
                    case 5:
                        for (int i = 0; i < 4; i++)
                        {
                            Point Temp = new Point(Figurepoints[i]); //Берем каждую точку фигуры
                            Temp.Rotate(Figurepoints[i], Figurepoints[2]); //Отправляем точку на вращение вокруг точки вращения
                            NextFigurepoints.Insert(i, Temp);
                        }
                        break;
                    case 6:
                        for (int i = 0; i < 4; i++)
                        {
                            Point Temp = new Point(Figurepoints[i]); //Берем каждую точку фигуры
                            Temp.Rotate(Figurepoints[i], Figurepoints[3]); //Отправляем точку на вращение вокруг точки вращения
                            NextFigurepoints.Insert(i, Temp);
                        }
                        break;
                }
            }
        }

        public bool OnBulkOrOnFloor() //Метод проверяет на массе ли упавших фигур мы находимся или на полу = проверяем что ВНИЗУ!!!
        {
            bool coincidence1 = false;
            bool coincidence2 = false;
            bool result = false;

            for (int i = 0; i < 4; i++)     //Проверка след точки по направлению ВНИЗ!
            {
                Point Temp = new Point(Figurepoints[i]); //Копируем точку из текущего реального положения фигуры для ее сдвига и проверки возможности движения фигуры
                Temp.Move(1, Direction.DOWN);  //Сдвигаем координаты первой точки фигуры в выбранном направлении
                PointsOnBulkOrFloor.Insert(i, Temp);
            }

            for (int i = 0; i < PointsOnBulkOrFloor.Count; i++) //Проверка столкновения с массой упавших фигур
            {
                for (int j = 0; j < BulkOfFigures.Bulk.Count; j++)  //Если в bulk ничего нет, то coincidence останется false
                {
                    bool areEqual = PointsOnBulkOrFloor[i].Equals(BulkOfFigures.Bulk[j]);
                    if (areEqual)
                    {
                        coincidence1 = true;
                        break;
                    }
                }
            }

            for (int i = 0; i < PointsOnBulkOrFloor.Count; i++) //Проверка столкновения с полом
            {

                for (int j = 0; j < Borders.Borderlines.Count; j++)  //Рассматриваем точки границы
                {
                    if (Borders.Borderlines[j].y == 23)  //Сужаем поиск до точек пола
                    {
                        bool areEqual = PointsOnBulkOrFloor[i].Equals(Borders.Borderlines[j]);
                        if (areEqual)
                        {
                            coincidence2 = true;
                            break;
                        }
                    }
                }
            }

            if (coincidence1 == false & coincidence2 == false) //Если фигура не на упавших фигурах и не на полу
            {
                result = false;
            }
            else //Если фигура опустилась на упавшие фигуры или на пол, то список упавших фигур дополняется этой фигурой
            {
                result = true;
                foreach (Point P in Figurepoints)
                {
                    BulkOfFigures.Bulk.Add(P);
                }
            }
            PointsOnBulkOrFloor.Clear(); //Удаляем эти точки
            return result;
        }

        public bool MayNextStepPointHappen()
        {
            bool areEqualWalls = false;
            bool areEqualBulk = false;
            bool Go = false;
            int FreeSteps = 8; //Количество свободных шагов уменьшается, если наша фигура сталкивается с чем-то

            for (int i = 0; i < NextFigurepoints.Count; i++)
            {
                Point Temp = NextFigurepoints[i]; //Создаем новую точку, чьи координаты сдвинем
                for (int j = 0; j < Borders.Borderlines.Count; j++)
                {
                    areEqualWalls = Temp.Equals(Borders.Borderlines[j]);     //Сравниваем смещеные координаты со границами поля
                    if (areEqualWalls == true) //Если первая сдвинутая координата совпала с какой-то точкой границы, значит дальше можно не проверять
                    {
                        FreeSteps--; //Раз точки совпали, то значит на одну точку в фигуре не сдвинуться. Этого уже, конечно, достаточно, чтобы результатом всей функции был FALSE
                    }
                }
            }

            for (int i = 0; i < NextFigurepoints.Count; i++)
            {
                Point Temp = NextFigurepoints[i];
                for (int j = 0; j < BulkOfFigures.Bulk.Count; j++)
                {
                    areEqualBulk = Temp.Equals(BulkOfFigures.Bulk[j]);     //Сравниваем смещеные координаты со границами поля
                    if (areEqualBulk == true)
                    {
                        FreeSteps--;
                    }
                }
            }

            if (FreeSteps==8)
            {
                Go = true;
            }
            return Go;
        }

        public void ShowNextFigure(int NumberOfFigure)
        {
            for (int i = 0; i < NextImageFigurepoints.Count; i++)
            {
                NextImageFigurepoints[i].Clear();
            }
            NextImageFigurepoints.Clear();
            Figure = tetrominoes.ElementAt(NumberOfFigure);
            for (int i = 0; i < Figure.GetLength(0); i++) //GetLength(0) - Возвращает 32-разрядное целое число, представляющее количество строк.
            {
                for (int j = 0; j < Figure.GetLength(1); j++) //GetLength(1) - Возвращает кол-во колонок
                {

                    if (Figure[i, j] == 1)
                    {
                        int x = (32 - Figure.GetLength(0)) + i;
                        int y = j+4;
                        //Создаем новую точку и добавляем ее в список точек
                        Point p = new Point(x, y);
                        NextImageFigurepoints.Add(p);
                    }
                }
            }
            DrawFigure(NextImageFigurepoints);
        }

        public bool IsGameOver() //Проверка - можем ли мы шагнуть вниз, сразу после выдачи новой фигуры
        {
            bool coincidence = false;
            
            for (int i = 0; i < 4; i++)     //Проверка след точки по направлению ВНИЗ!
            {
                Point Temp = new Point(Figurepoints[i]); //Копируем точку из текущего реального положения фигуры для ее сдвига и проверки возможности движения фигуры
                Temp.Move(1, Direction.DOWN);  //Сдвигаем координаты первой точки фигуры в выбранном направлении
                PointsOnBulkOrFloor.Insert(i, Temp);
            }

            for (int i = 0; i < PointsOnBulkOrFloor.Count; i++) //Проверка столкновения с массой упавших фигур
            {
                for (int j = 0; j < BulkOfFigures.Bulk.Count; j++)  //Если в bulk ничего нет, то coincidence останется false
                {
                    bool areEqual = PointsOnBulkOrFloor[i].Equals(BulkOfFigures.Bulk[j]);
                    if (areEqual)
                    {
                        coincidence = true;
                        break;
                    }
                }
            }
            return coincidence; //Если совпало - ну все, гейм овер:)
        }
    }
}
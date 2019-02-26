using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Point
    {
        public int x;
        public int y;
        public char sym;


        public Point(Point p) //Point p - то, что на вход данного конструтора - на вход конструктора - объект класса точка, а не ее координаты
        {
            x = p.x;
            y = p.y;
            sym = '*';
        }

        public Point(int _x, int _y) //Функция, которая вызывается при каждом создании новой точки - эта функция называется конструктором
        {                                       //Конструктор никогда ничего не возвращает - не указываем int, float итп
            x = _x;
            y = _y;
            sym = '*';
        }

        public void Draw()
        {
            sym = '*';
            Console.SetCursorPosition(x, y);
            Console.Write(sym);
        }

        public void Move(int offset, Direction direction)
        {
            if (direction == Direction.RIGHT)
            {
                x = x + offset;
            }
            else if (direction == Direction.LEFT)
            {
                x = x - offset;
            }
            else if (direction == Direction.DOWN)
            {
                y = y + offset;
            }
            else if (direction == Direction.ROTATE)
            {
            }
        }

        public void Clear()
        {
            sym = ' ';
            Console.SetCursorPosition(x, y);
            Console.Write(sym);
        }

        public void Rotate(Point Point, Point PointOfRotation)
        {
            x = (0 * (Point.x - PointOfRotation.x) + 1 * (Point.y - PointOfRotation.y)) + PointOfRotation.x;
            y = ((-1) * (Point.x - PointOfRotation.x) + 0 * (Point.y - PointOfRotation.y)) + PointOfRotation.y;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Point;

            if (other == null)
                return false;

            if (x == other.x && y == other.y)
                return true;

            return false;
        }

        public void BulkClear()
        {
            sym = ' ';
            Console.SetCursorPosition(x, y);
            Console.Write(sym);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Borders
    {
        public static List<Point> Borderlines = new List<Point>();
        public static List<Point> Floor = new List<Point>();

        public Borders()
        {
            for (int y = 0; y <= 22; ++y)
            {
                int x = 0;
                Point p = new Point(x, y);  //Создаем точку с соответствующими координатами - обращаемся к конструктору в классе Point
                Borderlines.Add(p);
            }

            for (int y = 0; y <= 22; ++y)
            {

                int x = 21;
                Point p = new Point(x, y);  //Создаем точку с соответствующими координатами - обращаемся к конструктору в классе Point
                Borderlines.Add(p);
            }

            for (int x = 0; x <= 40; ++x)
            {
                int y = 23;
                Point p = new Point(x, y);  //Создаем точку с соответствующими координатами - обращаемся к конструктору в классе Point
                Borderlines.Add(p);
            }

            for (int y = 0; y <= 22; ++y)
            {

                int x = 40;
                Point p = new Point(x, y);  //Создаем точку с соответствующими координатами - обращаемся к конструктору в классе Point
                Borderlines.Add(p);
            }

            DrowBorders();
        }



        internal void DrowBorders()
        {
            foreach (Point p in Borderlines) //Для каждой точки в списке
            {
                p.Draw();
            }
        }

        public static List<Point> GetList()
        {
            return Borderlines;
        }
    }
}
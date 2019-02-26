using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Tetris
{
    class Program
    {
        public static int NumberOfTetromino;
        public static int NextNumberOfTetromino;
        public static int Time;
        public static List<ConsoleKeyInfo> keyBuffer = new List<ConsoleKeyInfo>();
        public static int Score, Speed;
        

        static void Main(string[] args)  //Поток - опрос кнопок 
        {
            new Thread(() =>
            {
                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        if (keyBuffer.Count > 0)
                        {
                            if (keyBuffer[keyBuffer.Count - 1] != key)
                                keyBuffer.Add(key);
                        }
                        else
                            keyBuffer.Add(key);
                    }
                    Thread.Sleep(1);
                }
            }).Start();

            Borders sp = new Borders(); //Создаем границы
            BulkOfFigures Bulk = new BulkOfFigures();                            // BulkOfFigures Bulk = new BulkOfFigures(); //Создаем список для хранения точек упавших и застывших фигур 
            InformationForm();
            InformationFormInfo(0, 0);
            Speed = 0;
            //Information Info = new Information(Score);

            Random rnd = new Random();
            NumberOfTetromino = rnd.Next(7);

            while (true)
            {
                Figures Tetromino = new Figures(NumberOfTetromino); //Текушая фигура в рабочек поле
                Tetromino.DrawFigure(Figures.Figurepoints);

                NextNumberOfTetromino = rnd.Next(7);    //Номер следующей фигуры
                Tetromino.ShowNextFigure(NextNumberOfTetromino);

                if (Tetromino.IsGameOver() == true)
                {
                    for (int i = 0; i < Figures.Figurepoints.Count; i++)
                    {
                        Figures.Figurepoints[i].Clear(); //Удаляем все точки, что настроили с экрана
                    }
                    Figures.Figurepoints.Clear();   //Удаляем все, что настроили, из списка

                    for (int i = 0; i < BulkOfFigures.Bulk.Count; i++)
                    {
                        BulkOfFigures.Bulk[i].Clear(); //Удаляем все точки, что настроили с экрана
                    }
                    BulkOfFigures.Bulk.Clear();   //Удаляем все, что настроили, из списка

                    string txt = "GAME OVER!";
                    while (true)
                    {
                        //Console.SetCursorPosition(5, 10);
                        //Console.WriteLine("GAME OVER");
                        //Thread.Sleep(200);

                        WriteBlinkingText(txt, 500, true);
                        WriteBlinkingText(txt, 500, false);
                    }
                }

                while (Tetromino.OnBulkOrOnFloor() == false)  //Пока фигура не находится на полу или на дургих фигурах - можем выполнять с ней всяческие действия
                {
                    for (int i = 0; i < 5; i++) //Вдруг мы на полу уже, но фигуру еще можно чуть чуть подвести под другую...
                    {
                        Tetromino.direction = DirectionOfFigure(Tetromino);  //Определяем направление движения фигуры
                        Tetromino.CalculateNextFigurepoints(NumberOfTetromino, Tetromino.direction); //Считаем, возможно ли сместить фигуру по этому направлению.
                        if (Tetromino.MayNextStepPointHappen() == true)   // Если фигуру можно перенести на один шаг по выбранному направлению, то делаем это.
                        {
                            Thread.Sleep(Time);
                            Tetromino.MoveFigure();
                        }
                    }
                }

                //После того, как фигура упала, начинаем проверку уровней
                if (Bulk.CheckHorizontalLevel() == true) //Проверяем, ести ли заполненные уровни
                {
                    Bulk.DeleteLayers();    //Удаляем уровни.
                    Score = Score + 100;//После удаления уровней - надо наращивать очки,
                }
                Speed = (Score / 10000)*30;
                InformationFormInfo(Score, Score / 10000);
                NumberOfTetromino = NextNumberOfTetromino;
            }
        }

        private static Direction DirectionOfFigure(Figures Tetromino)
        {
            if (keyBuffer.Count > 0)
            {
                switch (keyBuffer[0].Key)
                {
                    case ConsoleKey.LeftArrow:
                        Time = 60;
                        Tetromino.direction = Direction.LEFT;
                        break;
                    case ConsoleKey.RightArrow:
                        Time = 60;
                        Tetromino.direction = Direction.RIGHT;
                        break;
                    case ConsoleKey.Spacebar:
                        Time = 60;
                        Tetromino.direction = Direction.ROTATE;
                        break;
                }
                keyBuffer.RemoveAt(0);
            }
            else
            {
                Time = 300-Speed;
                Tetromino.direction = Direction.DOWN;
            }
            return Tetromino.direction;
        }

        private static void InformationForm()
        {
            Console.SetCursorPosition(25, 2);
            Console.WriteLine("Next Figure:");

            Console.SetCursorPosition(28, 10);
            Console.WriteLine("Score:");

            Console.SetCursorPosition(28, 15);
            Console.WriteLine("Speed:");
        }

        private static void InformationFormInfo (int score, int speed)
        {
            Console.SetCursorPosition(28, 11);
            Console.Write(score);

            Console.SetCursorPosition(28, 16);
            Console.Write(speed);
        }

        private static void WriteBlinkingText(string text, int delay, bool visible)
        {
            if (visible)
            {
                Console.SetCursorPosition(5, 10);
                Console.Write(text);
            }
            else
                for (int i = 0; i < text.Length; i++)
                    Console.Write(" ");
            Console.SetCursorPosition(5, 10);
            //Console.CursorLeft -= text.Length;
            System.Threading.Thread.Sleep(delay);
        }
    }
}

using System;

namespace 셀룰러_오토마타
{
    internal class Program
    {
        static int FillWallRatio = 45;
        static int Post_Processing_Wall_Standard = 5;

        static int[,] Map;

        static void Main(string[] args)
        {
            Input();
            MapSet();
            Console.Clear();
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == 1)
                        Console.Write("■");
                    else
                        Console.Write("□");
                }
                Console.WriteLine();
            }

            for (int i = 0; i < 5; i++)
                Post_Processing1();

            Console.WriteLine();

            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == 1)
                        Console.Write("■");
                    else
                        Console.Write("□");
                }
                Console.WriteLine();
            }

            MapSet();
            Console.WriteLine("\nㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ\n");

            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == 1)
                        Console.Write("■");
                    else
                        Console.Write("□");
                }
                Console.WriteLine();
            }

            for (int i = 0; i < 5; i++)
                Post_Processing2();

            Console.WriteLine();

            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == 1)
                        Console.Write("■");
                    else
                        Console.Write("□");
                }
                Console.WriteLine();
            }

        }

        static void Input()
        {
            Console.WriteLine("맵의 크기를 입력하시오.");
            string[] s = Console.ReadLine().Split(' ');
            if(s.Length != 2)
            {
                Console.Clear();
                Console.WriteLine("올바르지 않은 입력입니다. ");
                Input();
                return;
            }

            foreach (string s2 in s)
                if (!int.TryParse(s2, out int i) && i >= 10)
                    if (s.Length != 0)
                    {
                        Console.Clear();
                        Console.WriteLine("올바르지 않은 입력입니다.");
                        Input();
                        return;
                    }

            Map = new int[Int32.Parse(s[1]), Int32.Parse(s[0])];

        }

        static void MapSet()
        {
            Random r = new Random();

            for(int i = 0; i < Map.GetLength(1); i++)
                for(int j = 0; j < Map.GetLength(0); j++)
                    Map[i, j] = r.Next(100) < FillWallRatio ? 1 : 0;       //  1 = 벽,  0 = 공간

        }

        static int NeighboredWall(int a, int b)
        {

            int count = 0;

            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                    if (a + i >= 0 && a + i <= Map.GetLength(1) - 1
                     && b + j >= 0 && b + j <= Map.GetLength(0) - 1)
                        count += Map[i + a, j + b];
                    else
                        count++;

            return count;
        }

        static void Post_Processing1()
        {
            for(int i = 0; i < Map.GetLength(1); i++)
                for(int j = 0; j < Map.GetLength(0); j++)
                {
                    if (NeighboredWall(i, j) >= Post_Processing_Wall_Standard)
                        Map[i, j] = 1;
                    else if (NeighboredWall(i, j)< Post_Processing_Wall_Standard)
                        Map[i, j] = 0;
                }
        }

        static void Post_Processing2()
        {
            int[,] tmp = new int[Map.GetLength(1), Map.GetLength(0)];

            for (int i = 0; i < Map.GetLength(1); i++)
                for (int j = 0; j < Map.GetLength(0); j++)
                {
                    if (NeighboredWall(i, j) >= Post_Processing_Wall_Standard)
                        tmp[i, j] = 1;
                    else if (NeighboredWall(i, j) < 2)
                        tmp[i, j] = 0;
                }
            Map = tmp;
        }

    }
}

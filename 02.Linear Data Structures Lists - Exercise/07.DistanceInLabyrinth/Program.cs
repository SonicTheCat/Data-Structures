using System;
using System.Collections.Generic;
using System.Linq;

namespace DistanceInLabyrinth
{
    public class Program
    {
        /*
         * In this exercise I will use BFS (breadth first search) algorithm. 
         * Appropriate data struct for this algorithm is Queue
         * 
         * I am also using Tuple class, with three items, to store information about each cell
         * - First Item in the Tuple is the row of cell
         * - Second Item in the Tuple is the col of the cell
         * - Third Item in the Tuple keeps information about how many steps are needed to reach that cell from starting position
         */
        private static Queue<Tuple<int, int, int>> cells;
        private static string[][] matrix;
        private static Tuple<int, int, int> startingPosition;
        private static int step;

        public static void Main()
        {
            var countOfLines = int.Parse(Console.ReadLine());
            matrix = new string[countOfLines][];

            for (int i = 0; i < countOfLines; i++)
            {
                var input = Console.ReadLine()
                    .ToCharArray()
                    .Select(x => x.ToString())
                    .ToArray();

                matrix[i] = input;
            }

            FindStartingPosition();

            cells = new Queue<Tuple<int, int, int>>();
            cells.Enqueue(startingPosition);

            while (cells.Count != 0)
            {
                var currentPositionSteps = cells.Dequeue();
                var row = currentPositionSteps.Item1;
                var col = currentPositionSteps.Item2;
                step = currentPositionSteps.Item3;

                if (row - 1 >= 0 && matrix[row - 1][col] == "0")
                {
                    AddFoundedEmptySpace(row - 1, col);
                }

                if (row + 1 < matrix.Length && matrix[row + 1][col] == "0")
                {
                    AddFoundedEmptySpace(row + 1, col);
                }

                if (col - 1 >= 0 && matrix[row][col - 1] == "0")
                {
                    AddFoundedEmptySpace(row, col - 1);
                }

                if (col + 1 < matrix.Length && matrix[row][col + 1] == "0")
                {
                    AddFoundedEmptySpace(row, col + 1);
                }
            }
            PrintMatrix();
        }

        public static void FindStartingPosition()
        {
            var firstStep = 1;
            for (int row = 0; row < matrix.Length; row++)
            {
                for (int col = 0; col < matrix.Length; col++)
                {
                    if (matrix[row][col] == "*")
                    {
                        startingPosition = new Tuple<int, int, int>(row, col, firstStep);
                    }
                }
            }
        }

        public static void AddFoundedEmptySpace(int row, int col)
        {
            matrix[row][col] = step.ToString();
            cells.Enqueue(new Tuple<int, int, int>(row, col, step + 1));
        }

        public static void PrintMatrix()
        {
            for (int row = 0; row < matrix.Length; row++)
            {
                for (int col = 0; col < matrix.Length; col++)
                {
                    if (matrix[row][col] == "0")
                    {
                        matrix[row][col] = "u";
                    }
                    Console.Write(matrix[row][col]);
                }
                Console.WriteLine();
            }
        }
    }
}
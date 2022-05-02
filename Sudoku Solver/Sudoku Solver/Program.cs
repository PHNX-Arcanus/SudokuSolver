using System;
using System.Collections.Generic;

namespace Sudoku_Solver
{
    class SudokuTracker
    {

    }

    class Program
    {
        static void Main(string[] args)
        {
            //for (int i = 0; i < 5; i++)
            char[,] myGrid = new char[9, 9];
            List<string> myBlanks = new List<string> { };
            string[] fileLines = System.IO.File.ReadAllLines("C:\\Users\\Alex\\Documents\\Visual Studio 2022\\Projects\\SudokuSolver\\Puzzle1.txt");

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    myGrid[i, j] = fileLines[i][j];

                    if (fileLines[i][j] == 'X')
                    {
                        myBlanks.Add(i.ToString() + "," + j.ToString());
                    }
                }
            }

            while (myBlanks.Count > 0)
            {

            }
        }
    }
}

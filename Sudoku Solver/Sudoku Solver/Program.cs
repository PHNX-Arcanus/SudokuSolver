/*
 
I unfortunately was not able to create an algorithm to solve puzzle 5, as that puzzle requires mutual pair exclusion to solve.
Rather than continue to delay my submission, I can explain my thought process on how I would solve it.
I would want to create three dictionaries of row, column, and box snapshots, using the current possibilities of each cell as a key, and its number of appearances as a value.
When adding to the dictionary, if a key already exists it would increment the value.
Afterwards, I would loop through the dictionary and find all entries where the length of the key is equal to its value, and then retroactively exclude those numbers from the other unknowns.
I created a dictionary of unknown values rather than set it up a second 2d array to represent unknowns or replace instances of X with 1-9 before going through exclusion.
The latter would be easier to iterate through and would allow me to process mutual pair exclusion through it.
 
 */

using System;
using System.IO;
using System.Collections.Generic;

namespace Sudoku_Solver 
{ 
    class Program
    {
        static void Main(string[] args)
        {
            string[] files = { "puzzle1.txt", "puzzle2.txt", "puzzle3.txt", "puzzle4.txt", "puzzle5.txt" };

            for (int f = 0; f < 4; f++)
            {
                char[,] myGrid = new char[9, 9];
                Dictionary<string, string> myBlanks = new Dictionary<string, string>();
                string[] fileLines = System.IO.File.ReadAllLines("C:\\Users\\arcan\\Desktop\\Sudoku\\" + files[f]);

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        myGrid[i, j] = fileLines[i][j];

                        if (fileLines[i][j] == 'X')  //Assembles a dictionary of known blanks using the coordinates as a key and 1-9 as a default value to eliminate
                        {
                            myBlanks.Add(i.ToString() + j.ToString(), "123456789");
                        }
                    }
                }
                Solve(myGrid, myBlanks, files[f]);
            }
        }

        static void Solve(char[,] grid, Dictionary<string, string> myBlanks, string fileName)
        { 
            while (myBlanks.Count > 0)
            {
                //Elimination - clear out possibilities using adjacent numbers
                List<string> myKeys = new List<string>(myBlanks.Keys);
                foreach (string key in myKeys)
                {
                    //Set up positional data
                    int col = Convert.ToInt32(key[1].ToString());
                    int row = Convert.ToInt32(key[0].ToString());
                    int boxRow = (row / 3) * 3;
                    int boxCol = (col / 3) * 3;

                    for (int i = 0; i < 9; i++)  //Validate through row
                    {
                        if (grid[i, col] != 'X')
                        {
                            myBlanks[key] = myBlanks[key].Replace(grid[i, col].ToString(), "");
                        }
                    }

                    for (int i = 0; i < 9; i++)  //Validate through column
                    {
                        if (grid[row, i] != 'X')
                        {
                            myBlanks[key] = myBlanks[key].Replace(grid[row, i].ToString(), "");
                        }
                    }

                    for (int i = 0; i < 3; i++)  //Validate through boxes
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (grid[boxRow + i, boxCol + j] != 'X')
                            {
                                myBlanks[key] = myBlanks[key].Replace(grid[boxRow + i, boxCol + j].ToString(), "");
                            }
                        }
                    }

                    //Remove solved cells from the dictionary
                    if (myBlanks[key].Length == 1)
                    {
                        grid[row, col] = char.Parse(myBlanks[key]);
                        myBlanks.Remove(key);
                    }
                }
            }

            //Write solution to file
            string[] solvedLines = new string[9];
            for (int i = 0; i < 9; i++)
            {
                solvedLines[i] = "";
                for (int j = 0; j < 9; j++)
                {
                    solvedLines[i] += grid[i, j];
                }
            }
            File.WriteAllLines("C:\\Users\\arcan\\Desktop\\Sudoku\\Solutions\\" + fileName, solvedLines);
        }
    }
}

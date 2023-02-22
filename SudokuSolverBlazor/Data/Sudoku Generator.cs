using Microsoft.Extensions.Localization;
using System.Data.Common;
using System.Text;

namespace SudokuSolverBlazor.Data
{
    public class Sudoku_Generator
    {
        private static readonly Random random = new Random();

        private static readonly List<char[]> sudoku = new List<char[]>
    {
        "000000000".ToCharArray(),
        "000000000".ToCharArray(),
        "000000000".ToCharArray(),
        "000000000".ToCharArray(),
        "000000000".ToCharArray(),
        "000000000".ToCharArray(),
        "000000000".ToCharArray(),
        "000000000".ToCharArray(),
        "000000000".ToCharArray()
    };

        public static readonly List<string> board = new List<string>();

        public static List<string> Generate()
        {
            while (ContainsZero())
            {
                for (int row = 0; row < sudoku.Count; row++)
                {
                    for (int column = 0; column < sudoku[row].Length; column++)
                    {
                        if (sudoku[row][column] == '0')
                        {
                            int value = GenerateRandomNumberNotInList(GetRowValues(row));

                            sudoku[row][column] = (char)(value + '0');
                        }
                    }
                }
            }

            GenerateBoard();

            return sudoku.Select(row => new string(row)).ToList();
        }

        private static bool ContainsZero()
        {
            return sudoku.Any(row => row.Contains('0'));
        }

        private static bool IsValidPosition(int row, int column, int value)
        {
            // Check if value in row
            if (sudoku[row].Contains((char)(value + '0')))
            {
                return false;
            }

            // Check if value in column
            for (int i = 0; i < 9; i++)
            {
                if (sudoku[i][column] == (char)(value + '0'))
                {
                    return false;
                }
            }

            // Check if value in square
            int squareRow = row / 3 * 3;
            int squareColumn = column / 3 * 3;

            for (int i = squareRow; i < squareRow + 3; i++)
            {
                for (int j = squareColumn; j < squareColumn + 3; j++)
                {
                    if (sudoku[i][j] == (char)(value + '0'))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static int GenerateRandomNumberNotInList(List<int> list)
        {
            var set = new HashSet<int>(list);

            int randomNumber = random.Next(1, 10);

            while (set.Contains(randomNumber))
            {
                randomNumber = random.Next(1, 10);
            }

            return randomNumber;
        }

        private static List<int> GetRowValues(int row)
        {
            return sudoku[row]
                .Where(c => c != '0')
                .Select(c => c - '0')
                .ToList();
        }

        private static void GenerateBoard()
        {
            board.Clear();

            foreach (var row in sudoku)
            {
                board.Add(ReplaceRandomCharacterWithDashXTimes(new string(row), random.Next(4, 9)));
            }
        }

        private static string ReplaceRandomCharacterWithDashXTimes(string input, int numTimes)
        {
            char[] charArray = input.ToCharArray();

            for (int i = 0; i < numTimes; i++)
            {
                int randomIndex = random.Next(0, input.Length);

                charArray[randomIndex] = '-';
            }

            return new string(charArray);
        }
    }
}

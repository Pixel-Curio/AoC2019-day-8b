using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Day_8b
{
    class Program
    {
        static void Main(string[] args)
        {
            const int width = 25;
            const int height = 6;

            string raw = File.ReadAllText(@"day8a-input.txt").Replace("\n", "");

            //split into rows
            var results = raw.Select((x, i) => i)
                .Where(i => i % width == 0)
                .Select(i => raw.Substring(i, raw.Length - i >= width ? width : raw.Length - i));

            //split into layers
            var layeredResults = results
                .Select((s, i) => i)
                .Where(i => i % height == 0)
                .Select(i => results.Where((s, index) => index >= i && index < i + height).ToList()).ToList();

            //compress layers
            for (int i = 1; i < layeredResults.Count; i++)
                for (int y = 0; y < layeredResults[i].Count; y++)
                {
                    StringBuilder row = new StringBuilder(layeredResults[i][y]);
                    for (int x = 0; x < layeredResults[i][y].Length; x++)
                    {
                        row[x] = layeredResults[0][y][x] == '2' ? layeredResults[i][y][x] : layeredResults[0][y][x];
                    }
                    layeredResults[0][y] = row.ToString();
                }


            //print image
            foreach (string row in layeredResults[0])
            {
                foreach (char c in row) Console.Write(c == '1' ? 'X' : ' ');
                Console.WriteLine();
            }
        }
    }
}

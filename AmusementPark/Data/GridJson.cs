using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AmusementPark.Data
{
    public class GridJson
    {
        public static string SerializeGrid(string[,] grid)
        {
            var rows = grid.GetLength(0);
            var cols = grid.GetLength(1);
            var list = new List<List<string>>();

            for (int i = 0; i < rows; i++)
            {
                var row = new List<string>();
                for (int j = 0; j < cols; j++)
                    row.Add(grid[i, j]);
                list.Add(row);
            }

            return JsonSerializer.Serialize(list);
        }

        public static string[,]? DeserializeGrid(string? json)
        {
            if (string.IsNullOrWhiteSpace(json)) return null;

            var list = JsonSerializer.Deserialize<List<List<string>>>(json);
            if (list == null || list.Count == 0) return null;

            int rows = list.Count;
            int cols = list[0].Count;
            var grid = new string[rows, cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    grid[i, j] = list[i][j];

            return grid;
        }

        public static string[,] GetDefaultGrid() => new string[,]
        {
    { ":green_square:", ":green_square:", ":green_square:", ":green_square:", ":green_square:" },
    { ":green_square:", ":green_square:", ":green_square:", ":green_square:", ":green_square:" },
    { ":green_square:", ":green_square:", ":green_square:", ":green_square:", ":green_square:" },
    { ":green_square:", ":green_square:", ":green_square:", ":green_square:", ":green_square:" },
    { ":green_square:", ":green_square:", ":green_square:", ":green_square:", ":green_square:" }
        };

    }
}

class Day04
{
    public static long Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day04.txt");
        string toSearch = "XMAS";

        var matrix = new List<List<char>>();
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine() ?? "";
            matrix.Add(line.ToCharArray().ToList());
        }
        sr.Close();

        int tot = 0;

        for (var x = 0; x < matrix.Count; x++)
        {
            for (var y = 0; y < matrix[x].Count; y++)
            {
                if (partOne)
                {
                    if (matrix[x][y] == toSearch[0])
                    {
                        tot += searchLine(matrix[x], y, toSearch, true) ? 1 : 0;
                        tot += searchLine(matrix[x], y, toSearch, false) ? 1 : 0;
                        tot += searchColumn(matrix, x, y, toSearch, true) ? 1 : 0;
                        tot += searchColumn(matrix, x, y, toSearch, false) ? 1 : 0;
                        tot += searchDiagonal(matrix, x, y, toSearch, true, true) ? 1 : 0;
                        tot += searchDiagonal(matrix, x, y, toSearch, true, false) ? 1 : 0;
                        tot += searchDiagonal(matrix, x, y, toSearch, false, true) ? 1 : 0;
                        tot += searchDiagonal(matrix, x, y, toSearch, false, false) ? 1 : 0;
                    }
                }

                if (!partOne)
                {
                    if (matrix[x][y] == 'A')
                    {
                        try
                        {
                            if (matrix[x + 1][y + 1] == 'M' && matrix[x - 1][y - 1] == 'S' || matrix[x + 1][y + 1] == 'S' && matrix[x - 1][y - 1] == 'M')
                            {
                                if (matrix[x + 1][y - 1] == 'M' && matrix[x - 1][y + 1] == 'S' || matrix[x + 1][y - 1] == 'S' && matrix[x - 1][y + 1] == 'M')
                                {
                                    tot++;
                                }
                            }
                        }
                        catch (Exception) { }
                    }
                }
            }
        }

        return tot;
    }

    private static bool searchLine(List<char> line, int startIndex, string toSearch, bool direction)
    {
        for (int i = 0; i < toSearch.Length; i++)
        {
            try
            {
                if (line[startIndex + (i * (direction ? 1 : -1))] != toSearch[i])
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }
        return true;
    }

    private static bool searchColumn(List<List<char>> matrix, int startIndex, int y, string toSearch, bool direction)
    {
        for (int i = 0; i < toSearch.Length; i++)
        {
            try
            {
                if (matrix[startIndex + (i * (direction ? 1 : -1))][y] != toSearch[i])
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }
        return true;
    }

    private static bool searchDiagonal(List<List<char>> matrix, int startIndexX, int startIndexY, string toSearch, bool xDirection, bool yDirection)
    {
        for (int i = 0; i < toSearch.Length; i++)
        {
            try
            {
                if (matrix[startIndexX + (i * (xDirection ? 1 : -1))][startIndexY + (i * (yDirection ? 1 : -1))] != toSearch[i])
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }
        return true;
    }
}

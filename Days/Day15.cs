class Day15
{
    public static int Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day15.txt");

        var matrix = new List<List<char>>();
        int currentLine = 0;
        bool isPath = false;
        var path = new List<char>();
        var startPos = (X: 0, Y: 0);
        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine() ?? "";
            if (line == "")
            {
                isPath = true;
                continue;
            }

            if (isPath)
            {
                path.AddRange(line);
                continue;
            }
            else
            {
                if (!partOne)
                {
                    line = line.Replace(".", "..");
                    line = line.Replace("@", "@.");
                    line = line.Replace("O", "[]");
                    line = line.Replace("#", "##");
                }
                for (int i = 0; i < line.Length; i++)
                {
                    if (currentLine == 0)
                    {
                        matrix.Add(new List<char>());
                    }
                    if (line[i] == '@')
                    {
                        startPos = (i, currentLine);
                        matrix[i].Add('.');
                    }

                    else
                    {
                        matrix[i].Add(line[i]);
                    }
                }
                currentLine++;
            }
        }
        sr.Close();

        var currentPos = startPos;
        foreach (var dir in path)
        {
            var nextPos = Move(currentPos, dir, matrix);
            if (nextPos != currentPos) currentPos = nextPos;
        }

        var tot = 0;
        for (int i = 0; i < matrix.Count; i++)
        {
            for (int j = 0; j < matrix[i].Count; j++)
            {
                if (matrix[i][j] == 'O' || matrix[i][j] == '[')
                {
                    tot += 100 * j + i;
                }
            }
        }

        return tot;
    }

    private static (int X, int Y) getNextPosition((int X, int Y) pos, char direction)
    {
        return direction switch
        {
            '^' => (pos.X, pos.Y - 1),
            'v' => (pos.X, pos.Y + 1),
            '<' => (pos.X - 1, pos.Y),
            '>' => (pos.X + 1, pos.Y),
            _ => pos
        };
    }

    private static (int X, int Y) Move((int X, int Y) pos, char direction, List<List<char>> matrix)
    {
        var nextPos = getNextPosition(pos, direction);

        if (matrix[nextPos.X][nextPos.Y] == '#') return pos;

        if (matrix[nextPos.X][nextPos.Y] == '.') return nextPos;

        if ((direction == '^' || direction == 'v') && (matrix[nextPos.X][nextPos.Y] == '[' || matrix[nextPos.X][nextPos.Y] == ']'))
        {
            var adiacent = matrix[nextPos.X][nextPos.Y] == '[' ? 1 : -1;
            var matrixCopy = copyMatrix(matrix);
            var movedCurrentTo = Move(nextPos, direction, matrix);
            var movedAdiacentTo = Move((nextPos.X + adiacent, nextPos.Y), direction, matrix);
            if ((movedCurrentTo == nextPos) || (movedAdiacentTo.X == nextPos.X + adiacent && movedAdiacentTo.Y == nextPos.Y))
            {
                rollbackMatrix(matrix, matrixCopy);
                return pos;
            }
            if (movedCurrentTo.Y == movedAdiacentTo.Y)
            {
                matrix[movedCurrentTo.X][movedCurrentTo.Y] = matrix[nextPos.X][nextPos.Y];
                matrix[nextPos.X][nextPos.Y] = '.';
                matrix[movedAdiacentTo.X][movedAdiacentTo.Y] = matrix[nextPos.X + adiacent][nextPos.Y];
                matrix[nextPos.X + adiacent][nextPos.Y] = '.';
                return nextPos;
            };
            return pos;
        }

        var movedTo = Move(nextPos, direction, matrix);
        if (movedTo == nextPos) return pos;

        matrix[movedTo.X][movedTo.Y] = matrix[nextPos.X][nextPos.Y];
        matrix[nextPos.X][nextPos.Y] = matrix[pos.X][pos.Y];

        return nextPos;
    }

    private static List<List<char>> copyMatrix(List<List<char>> matrix)
    {
        var matrixCopy = new List<List<char>>();
        for (int i = 0; i < matrix.Count; i++)
        {
            matrixCopy.Add(new List<char>());
            for (int j = 0; j < matrix[i].Count; j++)
            {
                matrixCopy[i].Add(matrix[i][j]);
            }
        }
        return matrixCopy;
    }

    private static void rollbackMatrix(List<List<char>> matrix, List<List<char>> matrixCopy)
    {
        for (int i = 0; i < matrix.Count; i++)
        {
            for (int j = 0; j < matrix[i].Count; j++)
            {
                matrix[i][j] = matrixCopy[i][j];
            }
        }
    }
}

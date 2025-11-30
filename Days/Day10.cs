class Day10
{
    public static int Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day10.txt");

        var matrix = new List<List<int>>();
        int currentLine = 0;
        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine() ?? "";

            for (int i = 0; i < line.Length; i++)
            {
                if (currentLine == 0)
                {
                    matrix.Add(new List<int>());
                }
                matrix[i].Add(int.Parse(line[i].ToString()));
            }
            currentLine++;

        }
        sr.Close();

        var tot = 0;
        for (int x = 0; x < matrix.Count; x++)
        {
            for (int y = 0; y < matrix[x].Count; y++)
            {
                if (matrix[x][y] == 0)
                {
                    var value = 0;
                    var currentPos = new Position(x, y);
                    var positionsToVisit = new Queue<List<Position>>();
                    positionsToVisit.Enqueue(new List<Position>() { currentPos });

                    var uniqueHeads = new HashSet<Position>();
                    while (positionsToVisit.Count > 0)
                    {
                        var ptc = positionsToVisit.Dequeue();
                        value++;
                        var neighbours = new List<Position>();
                        foreach (var posToCheck in ptc)
                        {
                            if (matrix[posToCheck.X][posToCheck.Y] == 9)
                            {
                                if (partOne) uniqueHeads.Add(posToCheck);
                                else tot++;
                                continue;
                            }
                            neighbours.AddRange(posToCheck.GetNeighboursOfValue(matrix, value));
                        }
                        if (neighbours.Count > 0) positionsToVisit.Enqueue(neighbours);
                    }
                    if (partOne) tot += uniqueHeads.Count;
                }
            }
        }

        return tot;
    }

    class Position
    {
        public int X, Y;
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public List<Position> GetNeighboursOfValue(List<List<int>> matrix, int valueToSearch)
        {
            var positions = new List<Position>();
            try
            {
                if (matrix[X + 1][Y] == valueToSearch)
                    positions.Add(new Position(X + 1, Y));
            }
            catch (Exception) { }

            try
            {
                if (matrix[X - 1][Y] == valueToSearch)
                    positions.Add(new Position(X - 1, Y));
            }
            catch (Exception) { }

            try
            {
                if (matrix[X][Y + 1] == valueToSearch)
                    positions.Add(new Position(X, Y + 1));
            }
            catch (Exception) { }

            try
            {
                if (matrix[X][Y - 1] == valueToSearch)
                    positions.Add(new Position(X, Y - 1));
            }
            catch (Exception) { }

            return positions;
        }

        public override bool Equals(object? obj)
        {
            return obj is Position position &&
                   X == position.X &&
                   Y == position.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}

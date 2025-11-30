class Day18
{
    public static int Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day18.txt");

        var matrixSize = 70;

        var blockedPositions = new HashSet<Position>();
        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine() ?? "";
            blockedPositions.Add(new Position(int.Parse(line.Split(",")[0]), int.Parse(line.Split(",")[1])));
        }
        sr.Close();

        var fixedBlockedPositions = blockedPositions.Take(1024).ToHashSet();
        var elasticBlockedPositions = blockedPositions.Skip(1024).ToHashSet();

        if (partOne)
        {
            return GetShortestPath(matrixSize, fixedBlockedPositions);
        }

        if (!partOne)
        {
            foreach (var blockedPosition in elasticBlockedPositions)
            {
                fixedBlockedPositions.Add(blockedPosition);
                var result = GetShortestPath(matrixSize, fixedBlockedPositions);
                if (result == -1)
                {
                    Console.WriteLine(blockedPosition.X + "," + blockedPosition.Y);
                    return 1;
                }
            }
        }

        return 0;
    }

    private static int GetShortestPath(int matrixSize, HashSet<Position> blockedPositions)
    {
        var endPos = new Position(matrixSize, matrixSize);
        var queue = new List<(Position p, int n)>() { (new Position(0, 0), 0) };
        var visited = new HashSet<Position>();
        while (queue.Count > 0)
        {
            var pos = queue[0];
            queue.RemoveAt(0);

            if (visited.Contains(pos.p)) continue;
            visited.Add(pos.p);

            var neighbours = pos.p.GetNeighbours(matrixSize, blockedPositions);
            foreach (var neighbour in neighbours)
            {
                if (neighbour.Equals(endPos))
                {
                    return pos.n + 1;
                }
                queue.Add((neighbour, pos.n + 1));
            }
            queue = queue.OrderBy(x => x.n).ToList();
        }

        return -1;
    }

    class Position
    {
        public int X, Y;
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public List<Position> GetNeighbours(int matrixSize, HashSet<Position> blocked)
        {
            var neighbours = new List<Position>();

            if (X > 0 && !blocked.Contains(new Position(X - 1, Y))) neighbours.Add(new Position(X - 1, Y));
            if (X <= matrixSize - 1 && !blocked.Contains(new Position(X + 1, Y))) neighbours.Add(new Position(X + 1, Y));
            if (Y > 0 && !blocked.Contains(new Position(X, Y - 1))) neighbours.Add(new Position(X, Y - 1));
            if (Y <= matrixSize - 1 && !blocked.Contains(new Position(X, Y + 1))) neighbours.Add(new Position(X, Y + 1));
            return neighbours;
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

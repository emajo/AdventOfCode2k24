class Day16
{
    public static int Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day16.txt");

        var matrix = new List<List<char>>();
        int currentLine = 0;
        var startPos = new Position(0, 0);
        var endPos = new Position(0, 0);
        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine() ?? "";

            for (int i = 0; i < line.Length; i++)
            {
                if (currentLine == 0)
                {
                    matrix.Add(new List<char>());
                }
                if (line[i] == 'S')
                {
                    startPos = new Position(i, currentLine);
                    matrix[i].Add('.');
                    continue;
                };
                if (line[i] == 'E')
                {
                    endPos = new Position(i, currentLine);
                    matrix[i].Add('.');
                    continue;
                }
                matrix[i].Add(line[i]);
            }
            currentLine++;

        }
        sr.Close();

        var queue = new List<Move>() { new Move(startPos, 0, new Position(1, 0), new List<Position>()) };

        var visited = new Dictionary<(Position pos, Position dir), int>();

        var minCost = int.MaxValue;
        var shortestPath = new HashSet<Position>();

        while (queue.Count > 0)
        {
            var currentMove = queue[0];
            queue.RemoveAt(0);

            currentMove.Path.Add(currentMove.Pos);

            if (visited.ContainsKey((currentMove.Pos, currentMove.Dir)))
            {
                if (currentMove.Score > visited[(currentMove.Pos, currentMove.Dir)]) continue;
            };
            visited[(currentMove.Pos, currentMove.Dir)] = currentMove.Score;

            if (currentMove.Score > minCost) continue;

            if (currentMove.Pos.Equals(endPos))
            {
                if (partOne) return currentMove.Score;

                minCost = Math.Min(minCost, currentMove.Score);
                currentMove.Path.ForEach(p => shortestPath.Add(p));
            }

            var nextMoves = currentMove.GetNextMoves(matrix, currentMove.Score, currentMove.Dir);

            queue.AddRange(nextMoves);
            queue = queue.OrderBy(x => x.Score).ToList();
        }

        return shortestPath.Count;

    }

    class Move
    {
        public Position Pos;
        public int Score;
        public Position Dir;
        public List<Position> Path;

        public Move(Position p, int score, Position dir, List<Position> path)
        {
            Pos = p;
            Dir = dir;
            Score = score;
            Path = new List<Position>(path);
        }

        public List<Move> GetNextMoves(List<List<char>> matrix, int actualScore, Position dir)
        {
            var possibilities = new List<Move>();

            var sameDir = Pos.TryAdd(dir);
            if (Position.isVisitable(matrix, sameDir)) possibilities.Add(new Move(sameDir, actualScore + 1, dir, Path));

            if (dir.X != 0)
            {
                var down = Pos.TryAdd(new Position(0, 1));
                if (Position.isVisitable(matrix, down)) possibilities.Add(new Move(down, actualScore + 1001, new Position(0, 1), Path));

                var up = Pos.TryAdd(new Position(0, -1));
                if (Position.isVisitable(matrix, up)) possibilities.Add(new Move(up, actualScore + 1001, new Position(0, -1), Path));
            }
            else
            {
                var right = Pos.TryAdd(new Position(1, 0));
                if (Position.isVisitable(matrix, right)) possibilities.Add(new Move(right, actualScore + 1001, new Position(1, 0), Path));

                var left = Pos.TryAdd(new Position(-1, 0));
                if (Position.isVisitable(matrix, left)) possibilities.Add(new Move(left, actualScore + 1001, new Position(-1, 0), Path));
            }

            return possibilities;
        }
    }

    class Position
    {
        public int X, Y;
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position TryAdd(Position direction)
        {
            return new Position(X + direction.X, Y + direction.Y);
        }

        public static bool isVisitable(List<List<char>> matrix, Position pos)
        {
            if (pos.X < 0 || pos.Y < 0 || pos.X >= matrix.Count || pos.Y >= matrix[0].Count) return false;
            return matrix[pos.X][pos.Y] != '#';
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

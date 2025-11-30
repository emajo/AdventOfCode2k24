using System.Collections.Concurrent;

class Day20
{
    public static long Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day20.txt");

        var startPos = new Position(0, 0);
        var endPos = new Position(0, 0);
        var path = new List<Position>();

        var currentLine = 0;
        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine() ?? "";

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == 'S')
                {
                    startPos = new Position(i, currentLine);
                };
                if (line[i] == 'E')
                {
                    endPos = new Position(i, currentLine);
                }
                if (line[i] != '#')
                {
                    path.Add(new Position(i, currentLine));
                    continue;
                }
            }
            currentLine++;

        }
        sr.Close();


        var cachedLength = new Dictionary<Position, int>() { { endPos, 0 } };

        var currentPos = endPos;
        var len = 0;

        while (!currentPos.Equals(startPos))
        {
            currentPos = currentPos.GetNearPositions(1, path).Where(p => !cachedLength.ContainsKey(p.p)).First().p;
            cachedLength[currentPos] = ++len;
        }

        var totLen = cachedLength[startPos];
        var tot = 0;

        var totBag = new ConcurrentBag<int>();

        Parallel.ForEach(path, pos =>
        {
            var currentLen = totLen - cachedLength[pos];
            var nearPositions = pos.GetNearPositions(partOne ? 2 : 20, path);

            Parallel.ForEach(nearPositions, nearPos =>
                        {
                            var addedLen = nearPos.l + cachedLength[nearPos.p];
                            if (totLen - currentLen - addedLen >= 100)
                            {
                                totBag.Add(1);
                            }
                        });
        });

        tot = totBag.Sum();

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

        public List<(Position p, int l)> GetNearPositions(int maxDistance, List<Position> positionsPool)
        {
            return positionsPool
                .Select(x => (x, Math.Abs(x.X - X) + Math.Abs(x.Y - Y)))
                .Where(x => x.Item2 <= maxDistance)
                .Where(x => !x.Equals(this))
                .ToList();
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

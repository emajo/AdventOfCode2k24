using System.Diagnostics;

class Day08
{
    public static long Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day08.txt");
        var groupedPositions = new Dictionary<char, List<Position>>();
        var matrixLen = 0;
        int currentLine = 0;

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine() ?? "";
            if (matrixLen == 0) matrixLen = line.Length;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] != '.')
                {
                    if (!groupedPositions.ContainsKey(line[i]))
                    {
                        groupedPositions.Add(line[i], new List<Position>());
                    }
                    groupedPositions[line[i]].Add(new Position(i, currentLine));
                }
            }
            currentLine++;

        }
        sr.Close();

        var visitedPos = new HashSet<Position>();

        foreach (var item in groupedPositions)
        {
            foreach (var pos in item.Value)
            {
                if (!partOne) visitedPos.Add(pos);
                foreach (var comp in item.Value)
                {
                    var res = pos.CalculateAnti(comp);
                    if (res == null || res.X >= matrixLen || res.Y >= currentLine) continue;
                    visitedPos.Add(res);

                    if (!partOne)
                    {
                        var comp2 = pos;
                        while (true)
                        {
                            var res2 = res.CalculateAnti(comp2);
                            if (res2 == null || res2.X >= matrixLen || res2.Y >= currentLine) break;
                            visitedPos.Add(res2);
                            comp2 = res;
                            res = res2;
                        }
                    }
                }
            }
        }

        return visitedPos.Count;
    }

    class Position
    {
        public int X, Y;
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position? CalculateAnti(Position p)
        {
            if (Equals(p)) return null;

            var xDist = X - p.X;
            var yDist = Y - p.Y;

            if (X + xDist < 0 || Y + yDist < 0)
                return null;

            return new Position(X + xDist, Y + yDist);
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
class Day12
{
    public static int Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day12.txt");

        var matrix = new List<List<char>>();
        int currentLine = 0;
        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine() ?? "";

            for (int i = 0; i < line.Length; i++)
            {
                if (currentLine == 0)
                {
                    matrix.Add(new List<char>());
                }
                matrix[i].Add(line[i]);
            }
            currentLine++;

        }
        sr.Close();

        var visitedPos = new List<Position>();
        var tot = 0;
        for (int x = 0; x < matrix.Count; x++)
        {
            for (int y = 0; y < matrix[x].Count; y++)
            {
                var currentPos = new Position(x, y);
                var localVisited = new List<Position>();
                if (visitedPos.Contains(currentPos)) continue;
                var area = 0;
                var perimeter = 0;

                var positionsToVisit = new Queue<Position>();
                positionsToVisit.Enqueue(currentPos);

                var sides = 0;

                while (positionsToVisit.Count > 0)
                {
                    var posToCheck = positionsToVisit.Dequeue();
                    visitedPos.Add(posToCheck);
                    localVisited.Add(posToCheck);
                    area++;

                    var neighbours = posToCheck.GetNeighboursOfValue(matrix, matrix[x][y]);

                    perimeter += 4;
                    foreach (var neighbour in neighbours)
                    {
                        if (!visitedPos.Contains(neighbour) && !positionsToVisit.Contains(neighbour)) positionsToVisit.Enqueue(neighbour);
                        perimeter--;
                    }
                    if (neighbours.Count == 1 || neighbours.Count == 3)
                    {
                        sides++;
                    }
                    if (neighbours.Count == 2)
                    {
                        //check if it either has cells left up && right down OR left down && right up
                        if (neighbours.Contains(new Position(posToCheck.X + 1, posToCheck.Y)) && neighbours.Contains(new Position(posToCheck.X, posToCheck.Y + 1)) ||
                            neighbours.Contains(new Position(posToCheck.X - 1, posToCheck.Y)) && neighbours.Contains(new Position(posToCheck.X, posToCheck.Y - 1)))
                        {
                            sides++;
                        }
                    }

                }

                //Console.WriteLine($"Char: {matrix[x][y]} Area: {area} Perimeter: {perimeter}");
                Console.WriteLine($"Sides: {sides}");

                tot += area * (partOne ? perimeter : sides);
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

        public List<Position> GetNeighbours(List<List<char>> matrix)
        {
            var positions = new List<Position>();

            if (X > 0) positions.Add(new Position(X - 1, Y));
            if (Y > 0) positions.Add(new Position(X, Y - 1));
            if (X < matrix.Count - 1) positions.Add(new Position(X + 1, Y));
            if (Y < matrix[0].Count - 1) positions.Add(new Position(X, Y + 1));



            return positions;
        }

        public List<Position> GetNeighboursOfValue(List<List<char>> matrix, char valueToSearch)
        {

            return GetNeighbours(matrix).Where(n => matrix[n.X][n.Y] == valueToSearch).ToList();
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

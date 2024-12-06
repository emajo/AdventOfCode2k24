using System.Diagnostics;

class Day06
{
    public static int Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day06.txt");

        var matrix = new List<List<char>>();
        var startPos = new Position(0, 0);
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
                if (line[i] == '^')
                {
                    startPos = new Position(i, currentLine);
                }
            }
            currentLine++;

        }
        sr.Close();

        var direction = 0;
        var visitedPos = new HashSet<Position>(){
            startPos
        };
        var currPos = new Position(startPos.X, startPos.Y);

        while (true)
        {
            var nextPos = GetNextPosition(currPos, ref direction, matrix);
            if (nextPos == null) break;
            currPos = nextPos;
            visitedPos.Add(currPos);
        }

        if (partOne) return visitedPos.Count;

        Stopwatch sw = new Stopwatch();
        sw.Start();
        int loops = 0;
        for (int i = 1; i < visitedPos.Count; i++)
        {
            var p = visitedPos.ElementAt(i);
            // var vp = new HashSet<Position>(); //Visualize
            int iterations = 0;
            direction = 0;
            currPos = new Position(startPos.X, startPos.Y);

            matrix[p.X][p.Y] = '#';

            while (true)
            {
                var nextPos = GetNextPosition(currPos, ref direction, matrix);
                if (nextPos == null) break;
                if (iterations++ > 10000)
                {
                    loops++;
                    break;
                }
                currPos = nextPos;
                // vp.Add(currPos); //Visualize
            }
            matrix[p.X][p.Y] = '.';
            // Visualize(matrix, vp, p);
        }
        sw.Stop();
        Console.WriteLine(sw.ElapsedMilliseconds + "ms");
        return loops;
    }

    private static Position? GetNextPosition(Position currPos, ref int direction, List<List<char>> matrix)
    {
        Position nextPos;
        for (int i = 0; i < 4; i++)
        {
            nextPos = currPos.Move(directions[direction % 4]);

            if (nextPos.X < 0 || nextPos.X >= matrix.Count || nextPos.Y < 0 || nextPos.Y >= matrix[nextPos.X].Count)
            {
                return null;
            }
            if (matrix[nextPos.X][nextPos.Y] != '#')
            {
                return nextPos;
            }
            direction++;
        }
        throw new Exception("No valid position found");
    }

    private static void Visualize(List<List<char>> matrix, HashSet<Position> visitedPos, Position objPos)
    {
        for (int y = 0; y < matrix.Count; y++)
        {
            for (int x = 0; x < matrix[y].Count; x++)
            {
                if (x == objPos.X && y == objPos.Y)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write('W');
                }
                else if (matrix[x][y] == '#')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(matrix[x][y]);
                }
                else if (matrix[x][y] == '^')
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(matrix[x][y]);
                }
                else if (visitedPos.Contains(new Position(x, y)))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write('O');
                }
                else
                {
                    Console.Write(matrix[x][y]);
                }
                Console.ResetColor();
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    class Position
    {
        public int X, Y;
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position Move(string direction)
        {
            return direction switch
            {
                "up" => new Position(X, Y - 1),
                "right" => new Position(X + 1, Y),
                "down" => new Position(X, Y + 1),
                "left" => new Position(X - 1, Y),
                _ => throw new ArgumentException("Invalid direction")
            };
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

    private static readonly string[] directions = new string[] { "up", "right", "down", "left" };

}

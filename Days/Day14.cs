class Day14
{
    public static int Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day14.txt");

        var robots = new List<Robot>();

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine() ?? "";

            var splittedLine = line.Split(" ");
            var pos = splittedLine[0].Split("=")[1].Split(",");
            var speed = splittedLine[1].Split("=")[1].Split(",");

            robots.Add(new Robot(new Position(int.Parse(pos[0]), int.Parse(pos[1])), new Position(int.Parse(speed[0]), int.Parse(speed[1]))));
        }
        sr.Close();

        var width = 101;
        var height = 103;

        if (partOne)
        {
            for (int i = 0; i < 100; i++)
            {
                robots.ForEach(robot => robot.Move(width, height));
            }
            return robots.GroupBy
                (robot => robot.WhichQuarter(width, height))
                .Where(r => r.Key != 0)
                .Select(group => group.Count())
                .Aggregate(1, (a, b) => a * b);
        }

        if (!partOne)
        {
            var count = 1;
            while (true)
            {
                robots.ForEach(robot => robot.Move(width, height));
                for (int h = 0; h < height; h++)
                {
                    var row = robots.Where(r => r.Position.Y == h).OrderBy(r => r.Position.X).ToList();
                    var countX = 0;
                    for (int w = 0; w < row.Count - 1; w++)
                    {
                        countX = row[w + 1].Position.X - row[w].Position.X == 1 ? countX + 1 : 0;

                        if (countX == 10)
                        {
                            // printMatrix(width, height, robots);
                            return count;
                        };
                    }
                }
                count++;
            }
        }

        return 0;
    }

    private static void printMatrix(int width, int height, List<Robot> robots)
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var robot = robots.FirstOrDefault(r => r.Position.X == j && r.Position.Y == i);
                if (robot != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("X");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(".");
                }
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

    class Robot
    {
        public Position Position;
        public Position Speed;

        public Robot(Position position, Position speed)
        {
            Position = position;
            Speed = speed;
        }

        public void Move(int width, int height)
        {
            var newX = (Position.X + Speed.X) % width;
            if (newX < 0) newX += width;
            Position.X = newX;

            var newY = (Position.Y + Speed.Y) % height;
            if (newY < 0) newY += height;
            Position.Y = newY;
        }

        public int WhichQuarter(int width, int height)
        {
            int halfW = (width - 1) / 2;
            int halfH = (height - 1) / 2;
            if (Position.X < halfW && Position.Y < halfH) return 1;
            if (Position.X > halfW && Position.Y < halfH) return 2;
            if (Position.X < halfW && Position.Y > halfH) return 3;
            if (Position.X > halfW && Position.Y > halfH) return 4;
            return 0;
        }
    }
}

class Day13
{
    public static long Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day13.txt");

        long aX = 0;
        long aY = 0;
        long bX = 0;
        long bY = 0;

        long tot = 0;

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine() ?? "";
            if (line.StartsWith("Button A"))
            {
                aX = long.Parse(line.Split("X+")[1].Split(",")[0]);
                aY = long.Parse(line.Split("Y+")[1]);
            }
            if (line.StartsWith("Button B"))
            {
                bX = long.Parse(line.Split("X+")[1].Split(",")[0]);
                bY = long.Parse(line.Split("Y+")[1]);
            }
            if (line.StartsWith("Prize"))
            {

                var totX = long.Parse(line.Split("X=")[1].Split(",")[0]);
                var totY = long.Parse(line.Split("Y=")[1]);

                if (!partOne)
                {
                    totX += 10000000000000;
                    totY += 10000000000000;
                }

                long BCount = (totY * aX - totX * aY) / (aX * bY - aY * bX);
                long ACount = (totX - BCount * bX) / aX;

                if ((ACount * aX + BCount * bX) == totX && ACount * aY + BCount * bY == totY) tot += ACount * 3 + BCount;
            }

        }
        sr.Close();

        return tot;
    }
}

class Day01
{
    public static long Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day01.txt");
        var leftList = new List<int>();
        var rightList = new List<int>();

        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine() ?? "";
            var values = line.Split("   ").Select(int.Parse).ToList();

            leftList.Add(values.First());
            rightList.Add(values.Last());

        }
        sr.Close();

        // leftList = leftList.Order().ToList();
        // rightList = rightList.Order().ToList();

        int tot = 0;

        if (partOne)
        {
            for (int i = 0; i < leftList.Count; i++)
            {
                tot += Math.Abs(leftList[i] - rightList[i]);
            }
        }

        if (!partOne)
        {
            var intersections = rightList
                .Intersect(leftList).ToList();

            for (int z = 0; z < intersections.Count; z++)
            {
                tot += intersections[z] * rightList.Count(i => i == intersections[z]);
            }
        }

        return tot;
    }
}

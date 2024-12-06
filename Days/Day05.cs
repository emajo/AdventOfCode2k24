class Day05
{
    public static long Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day05.txt");

        var rules = new List<(int, int)>();
        var tests = new List<List<int>>();

        bool startTests = false;
        var tot = 0;

        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine() ?? "";
            if (line == "") { startTests = true; continue; }

            if (!startTests)
            {
                var rule = line.Split("|");
                rules.Add((int.Parse(rule[0]), int.Parse(rule[1])));
            }
            else
            {
                tests.Add(line.Split(",").Select(int.Parse).ToList());
            }
        }
        sr.Close();

        var testComparer = Comparer<int>.Create((x, y) =>
        {
            if (rules.Contains((x, y))) return -1;
            if (rules.Contains((y, x))) return 1;
            return 0;
        });

        foreach (var test in tests)
        {
            var orderedTest = test.OrderBy(page => page, testComparer).ToList();
            bool orderIsCorrect = test.SequenceEqual(orderedTest);

            var getMiddleItem = (List<int> list) => list[(list.Count - 1) / 2];

            if (partOne && orderIsCorrect) tot += getMiddleItem(test);

            if (!partOne && !orderIsCorrect) tot += getMiddleItem(orderedTest);
        }

        return tot;
    }

}

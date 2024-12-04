class Day02
{
    public static long Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day02.txt");
        int tot = 0;
        int minDelta = 1;
        int maxDelta = 3;

        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine() ?? "";
            var values = line.Split(" ").Select(int.Parse).ToList();

            var combinations = new List<List<int>> { values };
            if (!partOne)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    var newComb = new List<int>(values);
                    newComb.RemoveAt(i);
                    combinations.Add(newComb);
                }
            }

            foreach (var comb in combinations)
            {
                var deltas = new List<int>();
                for (int i = 1; i < comb.Count; i++)
                {
                    int delta = comb[i] - comb[i - 1];
                    deltas.Add(delta);
                }

                var positives = deltas.Count(d => d > 0);
                var negatives = deltas.Count(d => d < 0);

                var errorCount = 0;
                var maxErrors = 0;

                if (positives > negatives)
                {
                    errorCount = negatives;
                    foreach (var delta in deltas)
                    {
                        if (delta < minDelta || delta > maxDelta)
                        {
                            errorCount++;
                        }
                    }
                    if (errorCount <= maxErrors)
                    {
                        tot++;
                        break;
                    }
                }
                else
                {
                    errorCount = positives;
                    foreach (var delta in deltas)
                    {
                        if (Math.Abs(delta) < minDelta || Math.Abs(delta) > maxDelta)
                        {
                            errorCount++;
                        }
                    }
                    if (errorCount <= maxErrors)
                    {
                        tot++;
                        break;
                    }
                }
            }

        }
        sr.Close();

        return tot;
    }
}

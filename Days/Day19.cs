class Day19
{
    public static long Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day19.txt");

        var pieces = new List<string>();
        var combinations = new List<string>();

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine() ?? "";
            if (pieces.Count == 0)
            {
                pieces = line.Split(", ").ToList();
                continue;
            }

            if (line == "") continue;

            combinations.Add(line);

        }
        sr.Close();

        long tot = 0;

        foreach (var combination in combinations)
        {
            var possibilities = isPossible(combination, pieces);

            if (partOne && possibilities > 0)
            {
                tot++;
            }

            if (!partOne)
            {
                tot += possibilities;
            }
        }

        return tot;
    }

    private static long isPossible(string combination, List<string> pieces)
    {

        var queue = new List<string>() { "" };
        var visited = new Dictionary<string, long>() { { "", 1 } };

        while (queue.Count > 0)
        {
            var currentCombination = queue[0];
            queue.RemoveAt(0);

            foreach (var piece in pieces)
            {
                if (combination.StartsWith(currentCombination + piece))
                {
                    var newCombination = currentCombination + piece;
                    if (!visited.ContainsKey(newCombination))
                    {
                        queue.Add(newCombination);
                        visited.Add(newCombination, 0);
                    }
                    visited[newCombination] = visited[newCombination] + visited[currentCombination];
                }
            }
            queue = queue.OrderBy(x => x.Length).ToList();
        }
        return visited.ContainsKey(combination) ? visited[combination] : 0;
    }
}

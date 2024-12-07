class Day07
{
    public static long Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day07.txt");
        var operands = new char[] { '+', '*' };
        if (!partOne)
        {
            operands = new char[] { '+', '*', '|' };
        }
        long tot = 0;
        var combinationsCache = new Dictionary<int, List<List<char>>>();

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine() ?? "";

            var res = long.Parse(line.Split(':')[0]);

            var values = line.Split(':')[1].Trim().Split(' ').Select(long.Parse).ToList();

            if (!combinationsCache.ContainsKey(values.Count))
                combinationsCache.Add(values.Count, CalculateCombinations(values.Count, operands));

            var combinations = combinationsCache[values.Count];

            for (int z = 0; z < combinations.Count; z++)
            {
                var combination = combinations[z];
                long evaluation = values[0];
                for (int x = 0; x < values.Count - 1; x++)
                {
                    if (evaluation > res)
                    {
                        break;
                    }
                    if (combination[x] == '+')
                    {
                        evaluation += values[x + 1];
                    }
                    else if (combination[x] == '*')
                    {
                        evaluation *= values[x + 1];
                    }
                    else if (combination[x] == '|')
                    {
                        string s = evaluation.ToString() + values[x + 1].ToString();
                        evaluation = long.Parse(s);
                    }
                }
                if (evaluation == res)
                {
                    tot += res;
                    break;
                }
            }
        }
        sr.Close();

        return tot;
    }

    private static List<List<char>> CalculateCombinations(int len, char[] operators)
    {
        var combinations = new List<List<char>>();

        for (int i = 0; i < len; i++)
        {
            var newCombinations = new List<List<char>>();
            for (int j = 0; j < operators.Length; j++)
            {
                if (combinations.Count < operators.Length)
                {
                    newCombinations.Add(new List<char>() { operators[j] });
                    continue;
                }
                for (int k = 0; k < combinations.Count; k++)
                {
                    newCombinations.Add(new List<char>(combinations[k]) { operators[j] });
                }
            }
            combinations = newCombinations;
        }

        return combinations;
    }
}
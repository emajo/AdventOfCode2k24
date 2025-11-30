class Day11
{
    public static long Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day11.txt");

        var arrangements = new List<long>();
        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine() ?? "";
            line.Split(' ').Select(long.Parse).ToList().ForEach(arrangements.Add);
        }
        sr.Close();

        int totalRearrangements = partOne ? 25 : 75;

        var distinctCount = new Dictionary<long, long>();
        arrangements.ForEach(a => distinctCount.Add(a, 1));

        for (int i = 0; i < totalRearrangements; i++)
        {
            var newDistinctCount = new Dictionary<long, long>();
            foreach (var item in distinctCount)
            {
                var rearrangedItemList = GetRearrangedList(new List<long>() { item.Key });

                rearrangedItemList.ForEach(rearrangedItem =>
                {
                    if (newDistinctCount.ContainsKey(rearrangedItem))
                        newDistinctCount[rearrangedItem] += item.Value;
                    else
                        newDistinctCount.Add(rearrangedItem, item.Value);
                });
            }
            distinctCount = newDistinctCount;
        }

        return distinctCount.Values.Sum();
    }

    private static List<long> GetRearrangedList(List<long> list)
    {
        var newList = new List<long>();
        foreach (var item in list)
        {
            if (item == 0)
            {
                newList.Add(1);
                continue;
            }

            var stringItem = item.ToString();
            if (stringItem.Length % 2 == 0)
            {
                newList.Add(long.Parse(stringItem.Substring(0, stringItem.Length / 2)));
                newList.Add(long.Parse(stringItem.Substring(stringItem.Length / 2)));
                continue;
            }
            newList.Add(item * 2024);

        }
        return newList;
    }
}

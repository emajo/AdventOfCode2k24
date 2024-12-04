using System.Text.RegularExpressions;

class Day03
{
    public static long Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day03.txt");

        long tot = 0;
        string line = "";
        while (!sr.EndOfStream)
        {
            line += sr.ReadLine() ?? "";
        }
        sr.Close();

        line = "do()" + line + "don't()";
        MatchCollection matches = Regex.Matches(line, @"mul\((\w+),\s*(\w+)\)");

        var dosIndexes = new List<int>();
        var dosMatches = Regex.Matches(line, Regex.Escape("do()"));
        foreach (Match matched in dosMatches)
        {
            dosIndexes.Add(matched.Index);
        }

        var dontsIndexes = new List<int>();
        var dontsMatches = Regex.Matches(line, Regex.Escape("don't()"));

        foreach (Match matched in dontsMatches)
        {
            dontsIndexes.Add(matched.Index);
        }

        foreach (Match matched in matches)
        {
            if (partOne)
            {
                tot += long.Parse(matched.Groups[1].Value) * long.Parse(matched.Groups[2].Value);
            }

            if (!partOne)
            {
                var index = matched.Index;
                var maxDo = dosIndexes.Where(x => x <= index).Max();
                var dontExists = dontsIndexes.Where(x => x >= maxDo && x <= index).FirstOrDefault(-1);
                if (dontExists == -1)
                {
                    tot += long.Parse(matched.Groups[1].Value) * long.Parse(matched.Groups[2].Value);
                }
            }


        }

        return tot;
    }
}

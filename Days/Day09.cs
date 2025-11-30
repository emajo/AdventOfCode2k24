class Day09
{
    public static long Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day09.txt");
        var allocated = new List<MemorySlot>();
        var notAllocated = new List<MemorySlot>();

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine() ?? "";
            var count = 0;
            for (int i = 0; i < line.Length; i++)
            {
                var len = int.Parse(line[i].ToString());
                if (i % 2 == 0)
                {
                    if (partOne)
                        Enumerable.Range(0, len).ToList().ForEach(x => allocated.Add(new MemorySlot(count + x, 1, i / 2)));
                    else
                        allocated.Add(new MemorySlot(count, len, i / 2));
                }
                else
                {
                    notAllocated.Add(new MemorySlot(count, len, 0));
                }
                count += len;
            }

            for (int i = allocated.Count - 1; i >= 0; i--)
            {
                var lastAllocated = allocated[i];
                foreach (var naSlot in notAllocated)
                {
                    if (naSlot.Fits(lastAllocated))
                    {
                        if (lastAllocated.Index > naSlot.Index)
                            naSlot.Allocate(lastAllocated);

                        break;
                    }
                }
            }
        }
        sr.Close();
        long tot = 0;

        foreach (var slot in allocated)
        {
            for (int i = 0; i < slot.Length; i++)
            {
                tot += slot.Value * (slot.Index + i);
            }
        }

        return tot;
    }

    class MemorySlot
    {
        public long Index;
        public long Length;
        public int Value;

        public MemorySlot(long index, long length, int value)
        {
            Index = index;
            Length = length;
            Value = value;
        }

        public bool Fits(MemorySlot slot)
        {
            return Length >= slot.Length;
        }

        public void Allocate(MemorySlot slot)
        {
            slot.Index = Index;

            Length -= slot.Length;
            Index += slot.Length;
        }
    }
}
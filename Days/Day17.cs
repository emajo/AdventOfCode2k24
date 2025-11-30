class Day17
{
    public static long Part(bool partOne = true)
    {
        var sr = new StreamReader("inputs/day17.txt");

        long regA = 0;
        long regB = 0;
        long regC = 0;

        List<int> instructions = new List<int>();


        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine() ?? "";
            if (line.StartsWith("Register A"))
                regA = long.Parse(line.Split(": ")[1]);

            if (line.StartsWith("Register B"))
                regB = long.Parse(line.Split(": ")[1]);

            if (line.StartsWith("Register C"))
                regC = long.Parse(line.Split(": ")[1]);

            if (line.StartsWith("Program:"))
                instructions = line.Split(": ")[1].Split(",").Select(int.Parse).ToList();

        }
        sr.Close();

        if (partOne)
        {
            var computer = new Computer(regA, regB, regC, instructions);
            computer.Run();
        }

        if (!partOne)
        {
            var tryVal = 0;
            var instructionsToCheck = 0;
            var iteration = 0;
            var currentFound = 1;
            while (true)
            {
                var computer = new Computer(tryVal, 0, 0, instructions);
                computer.Run();

                if (string.Join(',', instructions) == string.Join(',', computer.Output))
                {
                    return tryVal;
                }

                if (computer.Output[computer.Output.Count - 1 - instructionsToCheck] == instructions[instructions.Count - 1 - instructionsToCheck].ToString() &&
                    computer.Output[computer.Output.Count - 2 - instructionsToCheck] == instructions[instructions.Count - 2 - instructionsToCheck].ToString())
                {
                    iteration = 1;
                    instructionsToCheck += 2;
                    currentFound = tryVal;
                }

                tryVal = iteration * 8 * currentFound;
                iteration++;
            }

        }

        return 0;
    }

    class Computer
    {
        public long RegisterA;
        public long RegisterB;
        public long RegisterC;

        public long InstructionPointer = 0;

        public List<string> Output = new List<string>();

        public List<int> Instructions = new List<int>();

        public Computer(long registerA, long registerB, long registerC, List<int> instructions)
        {
            RegisterA = registerA;
            RegisterB = registerB;
            RegisterC = registerC;
            Instructions = instructions;
        }

        private long Combo(long value)
        {
            return value switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                3 => 3,
                4 => RegisterA,
                5 => RegisterB,
                6 => RegisterC,
                _ => -1
            };
        }

        public void ApplyInstruction(Instruction instruction)
        {
            if (instruction.OpCode == 0)
            {
                var res = RegisterA / Math.Pow(2, Combo(instruction.Value));
                if (res > int.MaxValue) res = int.MaxValue;
                RegisterA = (long)res;
                InstructionPointer += 2;
                return;
            }

            if (instruction.OpCode == 1)
            {
                RegisterB = RegisterB ^ instruction.Value;
                InstructionPointer += 2;
                return;
            }

            if (instruction.OpCode == 2)
            {
                RegisterB = Combo(instruction.Value) % 8;
                InstructionPointer += 2;
                return;
            }

            if (instruction.OpCode == 3)
            {
                if (RegisterA == 0)
                {
                    InstructionPointer += 2;
                    return;
                }
                InstructionPointer = instruction.Value;
                return;
            }

            if (instruction.OpCode == 4)
            {
                RegisterB = RegisterB ^ RegisterC;
                InstructionPointer += 2;
                return;
            }

            if (instruction.OpCode == 5)
            {
                var res = Combo(instruction.Value) % 8;
                Output.Add(res.ToString());
                InstructionPointer += 2;
                return;
            }

            if (instruction.OpCode == 6)
            {
                var res = RegisterA / Math.Pow(2, Combo(instruction.Value));
                if (res > int.MaxValue) res = int.MaxValue;
                RegisterB = (long)res;
                InstructionPointer += 2;
                return;
            }

            if (instruction.OpCode == 7)
            {
                var res = RegisterA / Math.Pow(2, Combo(instruction.Value));
                if (res > int.MaxValue) res = int.MaxValue;
                RegisterC = (long)res;
                InstructionPointer += 2;
                return;
            }
        }

        public void Run()
        {
            while (InstructionPointer < Instructions.Count)
            {
                var instruction = new Instruction(Instructions[(int)InstructionPointer], Instructions[(int)InstructionPointer + 1]);
                ApplyInstruction(instruction);
            }

            Console.WriteLine(String.Join(",", Output));
        }
    }

    class Instruction
    {
        public int OpCode;
        public long Value;

        public Instruction(int opCode, long value)
        {
            OpCode = opCode;
            Value = value;
        }
    }


}

using System;
using System.IO;
using System.Linq;

namespace adventofcode
{
    public class Day02
    {
        public void Start()
        {
            Part1();
            Part2();
        }

        private void Part1()
        {
            var memory = File.ReadAllText("input/2").Split(',').Select(s => int.Parse(s)).ToArray();

            // fix "1202 program alarm"
            memory[1] = 12;
            memory[2] = 2;

            var res = Process(memory);
            Console.WriteLine($"Position 0 contains {res}");
        }

        private void Part2()
        {
            var memory = File.ReadAllText("input/2").Split(',').Select(s => int.Parse(s)).ToArray();
            var res = ProcessUntil(memory, 19690720);

            Console.WriteLine($"100 * noun + verb = {res}");
        }

        private int? ProcessUntil(int[] originalMemory, int expectedOutput)
        {
            for (int noun = 0; noun <= 99; noun++)
            {
                for (int verb = 0; verb <= 99; verb++)
                {
                    var memory = (int[]) originalMemory.Clone();
                    memory[1] = noun;
                    memory[2] = verb;
                    var output = Process(memory);

                    if (output == 19690720)
                    {
                        return 100 * noun + verb;
                    }
                }
            }

            return null;
        }

        private int Process(int[] memory)
        {
            var ip = 0;

            bool done = false;
            while (!done)
            {
                var opcode = memory[ip];

                switch (opcode)
                {
                    case 1:
                        memory[memory[ip + 3]] = memory[memory[ip + 1]] + memory[memory[ip + 2]];
                    break;
                    case 2:
                        memory[memory[ip + 3]] = memory[memory[ip + 1]] * memory[memory[ip + 2]];
                    break;
                    case 99:
                        done = true;
                    break;
                    default:
                        Console.WriteLine($"Unknown opcode {opcode}.");
                    break;
                }

                ip += 4;
            }

            var finalState = string.Join(",", memory);
            Console.WriteLine($"Processing done, final state is {finalState}");

            // What value is left at position 0 after the program halts?
            return memory[0];
        }
    }
}

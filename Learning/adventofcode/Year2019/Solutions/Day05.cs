using System.Collections.Generic;
using System.IO;
using Solutions.Shared;

namespace Solutions
{
    public class Day05
    {
        public static IReadOnlyCollection<int> Part1(FileInfo sourceCode)
        {   
            var program = new Program(IntcodeComputer.Parse(File.ReadAllText(sourceCode.FullName)));
            program.Input.Enqueue(1);
            
            program.Run();

            return program.Output;
        }
        
        public static IReadOnlyCollection<int> Part2(FileInfo sourceCode)
        {   
            var program = new Program(IntcodeComputer.Parse(File.ReadAllText(sourceCode.FullName)));
            program.Input.Enqueue(5);
            
            program.Run();

            return program.Output;
        }
    }
}
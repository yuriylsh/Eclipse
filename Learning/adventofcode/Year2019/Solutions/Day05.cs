using System;
using System.Collections.Generic;
using System.IO;
using Solutions.Shared;

namespace Solutions
{
    public class Day05
    {
        public static IReadOnlyCollection<int> Part1(FileInfo sourceCode)
        {   
            var input = new Queue<int>();
            input.Enqueue(1);
            var outputs = new List<int>();
            Action<int> output = x => outputs.Add(x);
            var program = IntcodeComputer.Parse(File.ReadAllText(sourceCode.FullName));
            
            IntcodeComputer.Run(program, input, output);

            return outputs;
        }
        
        public static IReadOnlyCollection<int> Part2(FileInfo sourceCode)
        {   
            var input = new Queue<int>();
            input.Enqueue(5);
            var outputs = new List<int>();
            Action<int> output = x => outputs.Add(x);
            var program = IntcodeComputer.Parse(File.ReadAllText(sourceCode.FullName));
            
            IntcodeComputer.Run(program, input, output);

            return outputs;
        }
    }
}
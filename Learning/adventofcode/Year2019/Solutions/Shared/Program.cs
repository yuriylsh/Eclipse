using System.Collections.Generic;

namespace Solutions.Shared
{
    public class Program
    {
        private readonly int[] _code;
        private readonly List<int> _output = new List<int>();
        private int _pointer;
        private IntcodeComputer.HaltReason _reason = IntcodeComputer.HaltReason.Done;
        public Queue<int> Input { get; }
        public IReadOnlyList<int> Output => _output;
        public IReadOnlyCollection<int> Code => _code;

        public Program(int[] code)
        {
            _code = code;
            Input = new Queue<int>();
        }

        public IntcodeComputer.HaltReason Run()
        {
            (_reason, _pointer) = IntcodeComputer.Run(_code, _pointer, Input, StoreOutput);
            return _reason;
        }

        private void StoreOutput(int x) => _output.Add(x);
    }
}
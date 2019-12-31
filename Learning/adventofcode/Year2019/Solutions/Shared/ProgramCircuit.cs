using System.Collections.Generic;

namespace Solutions.Shared
{
    public class ProgramCircuit
    {
        private readonly IReadOnlyList<Program> _programs;

        public ProgramCircuit(IReadOnlyList<Program> programs) => _programs = programs;

        public void Run()
        {
            var i = 0;
            while (true)
            {
                var current = _programs[i];
                var haltReason = current.Run();
                if (haltReason == IntcodeComputer.HaltReason.Done && i == _programs.Count - 1) break;
                var next = i != _programs.Count - 1 ? i + 1 : 0;
                _programs[next].Input.Enqueue(current.Output[^1]);
                i = next;
            }
        }
    }
}
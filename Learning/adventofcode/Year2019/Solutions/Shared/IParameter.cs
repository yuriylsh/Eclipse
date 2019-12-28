using System;

namespace Solutions.Shared
{
    public interface IParameter
    {
        int Read(ReadOnlySpan<int> memory);
        void Write(Span<int> memory, int value);
    }
    
    
    public struct ImmediateParameter : IParameter
    {
        private readonly Index _index;
        public ImmediateParameter(Index index) => _index = index;
        public int Read(ReadOnlySpan<int> memory) => memory[_index];
        public void Write(Span<int> memory, int value) => throw new NotImplementedException($"Attempted to write to a parameter in immediate mode at index {_index}");
    }
        
    public struct PositionParameter : IParameter
    {
        private readonly Index _index;
        public PositionParameter(Index index) => _index = index;
        public int Read(ReadOnlySpan<int> memory) => memory[memory[_index]];
        public void Write(Span<int> memory, int value) => memory[memory[_index]] = value;
    }
}
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;

namespace Solutions.Shared
{
    public class Canvas
    {
        private readonly int _width;
        private readonly int _height;
        private List<Layer> _layers;

        private Canvas(int width, int height)
        {
            _width = width;
            _height = height;
            _layers = new List<Layer>();
        }

        public static Canvas Load(Stream input, int width, int height)
        {
            var result = new Canvas(width, height);
            var layerSize = width * height;
            Span<byte> layerBuffer = stackalloc byte[layerSize];

            while(true)
            {
                var bytesRead = input.Read(layerBuffer);
                if (bytesRead == 0) break;
                if(bytesRead != layerSize) throw new ArgumentException($"Provided input size is not multiple of the layers size. Layer size is {layerSize} but read {bytesRead} only.");
                result._layers.Add(Layer.Load(layerBuffer, width, height));
            }
            
            return result;
        }

        public IReadOnlyList<Layer> Layers => _layers;
    }

    public class Layer
    {
        private readonly byte[] _data;
        private readonly int _columnsCount;

        public int RowsCount { get; }

        public IReadOnlyList<byte> AllPixels => _data;

        private Layer(byte[] data, int columnsCount, int rowsCount)
        {
            _data = data;
            _columnsCount = columnsCount;
            RowsCount = rowsCount;
        }

        public static Layer Load(in ReadOnlySpan<byte> input, int width, int height)
        {
            var result = new Layer(new byte[input.Length], width, height);
            input.CopyTo(result._data);
            return result;
        }

        public IEnumerable<byte> GetRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= RowsCount) throw new ArgumentException($"{nameof(rowIndex)} should be [0..{RowsCount}) but was {rowIndex}") ;
            var rowOffset = rowIndex * _columnsCount;
            for (int i = 0; i < _columnsCount; i++)
            {

                yield return _data[rowOffset + i];
            }
        }
    } 
}
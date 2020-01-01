using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

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
            using var reader = new StreamReader(input);
            var digitChars = reader.ReadLine().AsSpan();
            var position = 0;
            while(position < digitChars.Length)
            {
                result._layers.Add(Layer.Load(digitChars.Slice(position, layerSize), width, height));
                position += layerSize;
            }
            
            return result;
        }
        
        

        public IReadOnlyList<Layer> Layers => _layers;

        public Layer GetVisibleLayer()
        {
            var layerSize = _width * _height;
            var data = new byte[layerSize];
            for (var i = 0; i < layerSize; i++)
            {
                data[i] = _layers.FirstOrDefault(x => x.AllPixels[i] != 2)?.AllPixels[i] ?? 2;
            }
            return new Layer(data, _width, _height);
        }

        public void SaveAsBitmap(string path)
        {
            var visibleLayer = GetVisibleLayer();
            var result = new Bitmap(_width, _height);
            for (int y = 0; y < _height; y++)
            {
                for (var x = 0; x < _width; x++)
                {
                    var pixel = visibleLayer.AllPixels[_width * y + x];
                    result.SetPixel(x, y, pixel switch
                    {
                        0 => Color.Black,
                        1 => Color.White,
                        2 => Color.Transparent,
                        _ => Color.Red
                    });
                }
            }
            result.Save(path);
        }
    }

    public class Layer
    {
        private readonly byte[] _data;
        private readonly int _columnsCount;

        public int RowsCount { get; }

        public IReadOnlyList<byte> AllPixels => _data;

        public Layer(byte[] data, int columnsCount, int rowsCount)
        {
            _data = data;
            _columnsCount = columnsCount;
            RowsCount = rowsCount;
        }

        public static Layer Load(in ReadOnlySpan<char> input, int width, int height)
        {
            var result = new Layer(new byte[input.Length], width, height);
            for (var i = 0; i < input.Length; i++)
            {
                result._data[i] = byte.Parse(input.Slice(i, 1));
            }
            return result;
        }

        public IEnumerable<byte> GetRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= RowsCount) throw new ArgumentException($"{nameof(rowIndex)} should be [0..{RowsCount}) but was {rowIndex}") ;
            var rowOffset = rowIndex * _columnsCount;
            for (var i = 0; i < _columnsCount; i++)
            {
                yield return _data[rowOffset + i];
            }
        }
    } 
}
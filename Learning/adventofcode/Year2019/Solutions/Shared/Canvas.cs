using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Threading.Tasks;

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

        public static async Task<Canvas> Load(Stream input, int width, int height)
        {
            var result = new Canvas(width, height);
            var layerSize = width * height;
            var pipeline = PipeReader.Create(input, new StreamPipeReaderOptions(minimumReadSize: layerSize, bufferSize: layerSize));
            while (true)
            {
                var readResult = await pipeline.ReadAsync();
                var buffer = readResult.Buffer;

                foreach (var layerMemory in buffer)
                {
                    result._layers.Add(Layer.Load(layerMemory));
                }

                if (readResult.IsCompleted) break;
            }
            return result;
        }

        public IReadOnlyCollection<Layer> Layers => _layers;
    }

    public class Layer
    {
        public static Layer Load(in ReadOnlyMemory<byte> input)
        {
            return new Layer();
        }
    } 
}
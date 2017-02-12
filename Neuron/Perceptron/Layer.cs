
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Perceptron
{
    [DebuggerDisplay("id:{_id}")]
    class Layer
    {
        private int _id;
        private readonly Neuron[] _neurons;
        public readonly int Count;
        private Layer(int count)
        {
            _neurons = new Neuron[count];
            for (int i = 0; i < _neurons.Length; i++)
            {
                _neurons[i] = Neuron.GetNeuron(this);
            }
            Count = count;
        }

        public Neuron this[int index]
        {
            get
            {
                if (index > Count - 1)
                {
                    return null;
                }
                return _neurons[index];
            }
        }

        public void SetupNeuronsOuts(decimal[] neuronOutValues)
        {
            if (neuronOutValues.Length != _neurons.Length)
            {
                throw new ArgumentException($"neuronOutValues count {neuronOutValues.Length} not match with neurons count {_neurons.Length} in layer {_id}");
            }
            for (int i = 0; i < neuronOutValues.Length; i++)
            {
                _neurons[i].Out = neuronOutValues[i];
            }
        }
        public void SetupNeuronsErrors(decimal[] neuronErrorValues)
        {
            if (neuronErrorValues.Length != _neurons.Length)
            {
                throw new ArgumentException($"neuronErrorValues count {neuronErrorValues.Length} not match with neurons count {_neurons.Length} in layer {_id}");
            }
            for (int i = 0; i < neuronErrorValues.Length; i++)
            {
                _neurons[i].Out = neuronErrorValues[i];
            }
        }

        public decimal[] GetOuts()
        {
            return _neurons.Select(a => a.Out).ToArray();
        }

        #region factory

        public static Layer GetNeuron(int count)
        {
            var n = new Layer(count)
            {
                _id = Interlocked.Increment(ref _idSource),
            };
            return n;
        }

        private static int _idSource = 0;

        #endregion
    }
}


using System.Diagnostics;
using System.Threading;

namespace Perceptron
{
    [DebuggerDisplay("")]
    class Layer
    {
        private int _id;
        private Layer(int count)
        {
            _neurons = new Neuron[count];
            for (int i = 0; i < _neurons.Length; i++)
            {
                _neurons[i] = Neuron.GetNeuron(this);
            }
            Count = count;
        }

        private readonly Neuron[] _neurons;

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

        public readonly int Count;

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

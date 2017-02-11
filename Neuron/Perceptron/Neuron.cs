using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Perceptron
{
    [DebuggerDisplay("{_id},{_out},{_layer}")]
    class Neuron
    {
        private int _id;
        public List<NeuronRelation> NeuronRelationsPrev= new List<NeuronRelation>();
        public List<NeuronRelation> NeuronRelationsNext = new List<NeuronRelation>();
        private decimal _out;
        private Layer _layer;

        private Neuron()
        {
        }

        #region factory

        public static Neuron GetNeuron(Layer layer)
        {
            var n = new Neuron
            {
                _id = Interlocked.Increment(ref _idSource),
                _layer = layer
            };
            return n;
        }

        private static int _idSource = 0;

        #endregion
    }
}

using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Perceptron
{
    [DebuggerDisplay("id:{_id}, out:{Out}, layer:{_layer}")]
    class Neuron
    {
        private int _id;
        public Dictionary<int, NeuronRelation> RelationsToPrevLayer { get; set; } = new Dictionary<int, NeuronRelation>();
        public Dictionary<int, NeuronRelation> RelationsToNextLayer { get; set; } = new Dictionary<int, NeuronRelation>();
        public decimal Out { get; set; }
        public decimal Error { get; set; }
        private Layer _layer;

        private Neuron()
        { }

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

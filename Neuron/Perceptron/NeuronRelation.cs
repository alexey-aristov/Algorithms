using System;
using System.Diagnostics;
using System.Threading;

namespace Perceptron
{
    [DebuggerDisplay("from {_prevIndex} to {_nextIndex}")]
    class NeuronRelation
    {
        private int _id;
        private Neuron _neuronFromNextLayer;
        private Neuron _neuronFromPreviousLayer;
        private static readonly Random _random = new Random(1000);
        private int _prevIndex;
        private int _nextIndex;

        public decimal Weight { get; set; }

        private NeuronRelation()
        { }

        #region factory

        public static NeuronRelation SetupNeuronRelation(Neuron neuronOfCurrLayer, int currIndex, Neuron neuronOfNextLayer, int nextIndex)
        {
            var relation = new NeuronRelation
            {
                _id = Interlocked.Increment(ref _idSource),
                _neuronFromPreviousLayer = neuronOfCurrLayer,
                _neuronFromNextLayer = neuronOfNextLayer,
                _prevIndex = currIndex,
                _nextIndex = nextIndex
            };
            relation.Weight = (decimal)_random.NextDouble() - 0.5m;

            neuronOfCurrLayer.RelationsToNextLayer.Add(nextIndex, relation);
            neuronOfNextLayer.RelationsToPrevLayer.Add(currIndex, relation);

            return relation;
        }

        private static int _idSource = 0;

        #endregion
    }
}
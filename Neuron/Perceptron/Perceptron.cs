using System;
using System.Linq;

namespace Perceptron
{
    class Perceptron
    {
        private readonly Layer[] _layers;
        public Perceptron(int[] neuronsInLayer)
        {
            _layers = neuronsInLayer.Select(Layer.GetNeuron).ToArray();

            for (int i = 0; i < _layers.Length; i++)
            {
                Layer currLayer = _layers[i];
                if (i + 1 < _layers.Length)
                {
                    Layer nextLayer = _layers[i + 1];
                    for (int j = 0; j < _layers[i].Count; j++)
                    {
                        for (int k = 0; k < nextLayer.Count; k++)
                        {
                            var relation = NeuronRelation.SetupNeuronRelation(currLayer[j], j, nextLayer[k], k);
                        }
                    }
                }
            }
        }

        public decimal[] EvaluateInput(decimal[] input)
        {
            _layers[0].SetupNeuronsOuts(input);

            for (int i = 1; i < _layers.Length; i++)
            {
                for (int j = 0; j < _layers[i].Count; j++)
                {
                    decimal @out = 0;

                    for (int k = 0; k < _layers[i - 1].Count; k++)
                    {
                        var neuronFromPrevLayer = _layers[i - 1][k];
                        @out += neuronFromPrevLayer.RelationsToNextLayer[j].Weight * neuronFromPrevLayer.Out;
                    }
                    _layers[i][j].Out = Activate(@out);
                }
            }
            return _layers[_layers.Length - 1].GetOuts();
        }

        public void Calculate(decimal[] errors)
        {
            _layers[_layers.Length - 1].SetupNeuronsErrors(errors);
            for (int i = _layers.Length - 2; i > -1; i--)
            {
                for (int j = 0; j < _layers[i].Count; j++)
                {
                    decimal error = 0;
                    for (int k = 0; k < _layers[i + 1].Count; k++)
                    {
                        var neuronFromNextLayer = _layers[i + 1][k];
                        error += neuronFromNextLayer.Error * neuronFromNextLayer.RelationsToPrevLayer[j].Weight;
                    }
                    _layers[i][j].Error = error;
                }
            }
        }

        public void AdjustWeights()
        {
            for (int i = 1; i < _layers.Length - 1; i++)
            {
                for (int j = 0; j < _layers[i].Count; j++)
                {
                    for (int k = 0; k < _layers[i - 1].Count; k++)
                    {
                        var currentNeuron = _layers[i][j];
                        currentNeuron.RelationsToNextLayer[k].Weight += 0.5m * ActivateDerivative(currentNeuron.Error)*currentNeuron.Out;
                    }
                }
            }
        }


        private decimal Activate(decimal input)
        {
            return 1m / (1m + (decimal)Math.Exp((double)-input));
        }
        private decimal ActivateDerivative(decimal input)
        {
            var activated = Activate(input);
            return activated * (1 - activated);
        }

    }
}
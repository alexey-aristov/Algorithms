using System.Linq;

namespace Perceptron
{
    class Perception
    {
        private Layer[] layers;
        public Perception(int[]neuronsInLayer)
        {
            layers = neuronsInLayer.Select(Layer.GetNeuron).ToArray();
            for (int i = 0; i < layers.Length; i++)
            {
                Layer currLayer = layers[i];
                if (i+1<layers.Length)
                {
                    Layer nextLayer = layers[i + 1];
                    for (int j = 0; j < layers[i].Count; j++)
                    {
                        var relation = new NeuronRelation();
                        currLayer[j].NeuronRelationsPrev.Add(relation);
                        for (int k = 0; k < nextLayer.Count; k++)
                        {
                            nextLayer[k].NeuronRelationsPrev.Add(relation);
                        }
                    }
                }
            }
        }
    }
}
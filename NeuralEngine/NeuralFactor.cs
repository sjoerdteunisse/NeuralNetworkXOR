//
//Code for Research paper on neural networks.
//For questions contact: Sjoerdteunisse at google mail dot com
//

namespace NeuralEngine
{
    // Represents the weight of an input into a neuron
    public class NeuralFactor
    {
        // Create a NeuralFactor class that represents
        // the weight of an input into a neuron
        public NeuralFactor(double weight)
        {
            Weight = weight;
            Delta = 0;
        }

        /// <summary>
        /// Weight of neuralfactor
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Gradient decent rule for updating weights
        /// </summary>
        public double Delta { get; set; }

        /// <summary>
        /// Apply delta weight change
        /// </summary>
        public void ApplyWeightChange ()
        {
            Weight += Delta;
            Delta = 0;
        }
    }
}

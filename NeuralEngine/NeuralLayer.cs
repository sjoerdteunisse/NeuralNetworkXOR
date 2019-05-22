//
//Code for Research paper on neural networks.
//For questions contact: Sjoerdteunisse at google mail dot com
//

using System.Collections.Generic;

namespace NeuralEngine
{
   
    /// <summary>
    /// represents a layer of the neural network
    /// </summary>
    public class NeuralLayer : List<Neuron>
    {
        /// <summary>
        /// Applies learning to each neuron in the layer
        /// </summary>
        public void ApplyLearning()
        {
            foreach (Neuron neuron in this)
                neuron.ApplyLearning();
        }

        /// <summary>
        /// Pulse each neuron in layer
        /// </summary>
        public void Pulse()
        {
            foreach (Neuron neuron in this)
                neuron.Pulse();
        }
    }
}

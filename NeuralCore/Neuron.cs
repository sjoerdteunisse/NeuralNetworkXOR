//
//Code for Research paper on neural networks.
//For questions contact: Sjoerdteunisse at google mail dot com
//

using System;
using System.Collections.Generic;

namespace NeuralCore
{
    public class Neuron
    {
        public List<Dendrite> Dendrites { get; set; }
        public Pulse OutputPulse { get; set; }

       /// <summary>
       /// Creates a Neuron
       /// </summary>
        public Neuron()
        {
            Dendrites = new List<Dendrite>();
            OutputPulse = new Pulse();
        }


       /// <summary>
        /// Fires the activation with the value of each neuron
        /// </summary>
        public void Fire()
        {
            OutputPulse.Value = Sum();
            OutputPulse.Value = Activation(OutputPulse.Value);
        }

        /// <summary>
        /// Update the weights 
        /// </summary>
        /// <param name="newWeight">new weight to be set to the synapse</param>
        public void UpdateWeights(double newWeight)
        {
            foreach (var terminal in Dendrites)
            {
                terminal.SynapseWeight = newWeight;
            }
        }

        /// <summary>
        /// Compute value of neuron sum 
        /// </summary>
        /// <returns></returns>
        private double Sum()
        {
            double computeValue = 0.0f;
            foreach (var d in Dendrites)
            {
                computeValue += d.InputPulse.Value * d.SynapseWeight;
            }

            return computeValue;
        }

        /// <summary>
        /// Activation function 
        /// </summary>
        /// <param name="input">input to be activated</param>
        /// <returns></returns>
        public double Activation(double input)
        {
      
            double threshold = 1;
            return input <= threshold ? 0 : threshold;
        }

        /// <summary>
        /// Sigmoid function
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>

        public int Sigmoid(double x)
        {
            return (x >= 0) ? 1 : -1;
        }

        public override string ToString()
        {
            return OutputPulse.Value.ToString();
        }
    }
}
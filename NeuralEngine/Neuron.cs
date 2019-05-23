//
//Code for Research paper on neural networks.
//For questions contact: Sjoerdteunisse at google mail dot com
//

using System;
using System.Collections.Generic;

namespace NeuralEngine
{
    /// <summary>
    /// Represents a single Neuron
    /// </summary>
    public class Neuron
    {
        /// <summary>
        /// Initialze neuron
        /// </summary>
        /// <param name="bias">bias value</param>
        public Neuron(double bias)
        {
            Bias = new NeuralFactor(bias);
        }

        /// <summary>
        /// Apply delta that is calculated in back prop
        /// </summary>
        public void ApplyLearning()
        {
            foreach (var m in InputSignals)//Apply weight change
                m.Value.ApplyWeightChange();

            // Bias for shifting activation function val
            Bias.ApplyWeightChange();
        }

        /// <summary>
        /// Pulse neuron and calculate output signal
        /// </summary>
        public void Pulse()
        {
            double output = 0;

            foreach (var item in InputSignals)
                output += item.Key.Output * item.Value.Weight;

            output += Bias.Weight;

            OutputSignal.Output = Sigmoid(output);
        }

        /// <summary>
        /// Produce value between -1 and +1 based on input
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static double Sigmoid(double value)
        {
            return 1 / (1 + Math.Exp(-value));
        }

        /// <summary>
        /// Bias for shifting activation function val
        /// </summary>
        public NeuralFactor Bias { get; set; }
        /// <summary>
        /// Error rate
        /// </summary>
        public double Error { get; set; }
        
        /// <summary>
        /// Inputs
        /// </summary>
        public Dictionary<NeuronSignal, NeuralFactor> InputSignals { get; } =
            new Dictionary<NeuronSignal, NeuralFactor>();

        /// <summary>
        /// Outputs
        /// </summary>
        public NeuronSignal OutputSignal { get; set; } = new NeuronSignal();
    }
}

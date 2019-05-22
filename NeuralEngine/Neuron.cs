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
        public Neuron(double bias)
        {
            Bias = new NeuralFactor(bias);
        }

        /// <summary>
        /// Apply delta that is calculated in back prop
        /// </summary>
        public void ApplyLearning()
        {
            foreach (var m in InputSignals)
                m.Value.ApplyWeightChange();

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

        public NeuralFactor Bias { get; set; }
        public double Error { get; set; }
        public Dictionary<NeuronSignal, NeuralFactor> InputSignals { get; } = new Dictionary<NeuronSignal, NeuralFactor>();
        public NeuronSignal OutputSignal { get; set; } = new NeuronSignal();
    }
}

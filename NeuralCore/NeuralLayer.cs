//
//Code for Research paper on neural networks.
//For questions contact: Sjoerdteunisse at google mail dot com
//
using System;
using System.Collections.Generic;

namespace NeuralCore
{
    public class NeuralLayer
    {
        public List<Neuron> Neurons { get; set; }

        public string Name { get; set; }

        public double Weight { get; set; }

        public NeuralLayer(int count, double initialWeight, string name = "")
        {
            Neurons = new List<Neuron>();

            for (var i = 0; i < count; i++)
            {
                Neurons.Add(new Neuron());
            }

            Weight = initialWeight;
            Name = name;
        }

        /// <summary>
        /// Optimze the weights of a neuron based on the learning rate and delta
        /// </summary>
        /// <param name="learningRate"></param>
        /// <param name="delta">A gradient descent learning rule for updating the weights</param>
        public void Optimize(double learningRate, double delta)
        {
            Weight += learningRate * delta;
            foreach (var neuron in Neurons)
            {
                neuron.UpdateWeights(Weight);
            }
        }

        /// <summary>
        /// Forward neuron through layer
        /// </summary>
        public void Forward()
        {
            foreach (var neuron in Neurons)
            {
                //Fire neuron
                neuron.Fire();
            }
        }

        public override string ToString()
        {
            return string.Format("{0}, Weight: {1}", Name, Weight);
        }


    }
}
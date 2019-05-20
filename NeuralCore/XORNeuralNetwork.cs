//
//Code for Research paper on neural networks.
//For questions contact: Sjoerdteunisse at google mail dot com
//
using System;

namespace NeuralCore
{
    /// <summary>
    ///XOR problem
    /// Reference: https://medium.com/@lucaspereira0612/solving-xor-with-a-single-perceptron-34539f395182
    /// Reference: https://medium.com/@jayeshbahire/the-xor-problem-in-neural-networks-50006411840b
    ///The XOR, or “exclusive or”, problem is a classic problem in ANN research.
    /// It is the problem of using a neural network to predict the outputs of XOr logic gates
    /// given two binary inputs. An XOr function should:
    /// return a true value if the two inputs are not equal and a false value if they are equal.
    ///
    /// 
    /// </summary>
    public class XORNeuralNetwork : NeuralNetwork
    {
        /// <summary>
        /// Base constructor for XOR problem.
        /// </summary>
        public XORNeuralNetwork() :
            base()
        {}

        /// <summary>
        /// Run XOR network
        /// </summary>
        public void Run()
        {
            NetworkModel model = new NetworkModel();
            model.Layers.Add(new NeuralLayer(2, 0.1, "INPUT"));
            model.Layers.Add(new NeuralLayer(1, 0.1, "OUTPUT"));

            model.Build();
            Console.WriteLine("Init Training");
            model.Print();

            Console.WriteLine();

            NeuralData x = new NeuralData(4);
            x.Add(0, 0);
            x.Add(0, 1);
            x.Add(1, 0);
            x.Add(1, 1);

            NeuralData y = new NeuralData(4);
            y.Add(0);
            y.Add(0);
            y.Add(0);
            y.Add(1);

            model.Train(x, y, runItteration: 10, learningRate: 0.1);
            Console.WriteLine();
            Console.WriteLine("After Training");
            model.Print();
        }
    }
}

//
//Code for Research paper on neural networks.
//For questions contact: Sjoerdteunisse at google mail dot com
//

using System;
using NeuralCore;

namespace NeuralNetExplained
{
    class Program
    {
        static void Main(string[] args)
        {
            //Run XOR example
            Console.WriteLine("_____________Two layer network_________________\n");
            XORNeuralNetwork neuralNetwork = new XORNeuralNetwork();
            neuralNetwork.Run();

            //Create a three layer network with one hidden layer.
            Console.WriteLine("\n\n_____________Three layer network_________________");
            NetworkModel model = new NetworkModel();
            model.Layers.Add(new NeuralLayer(2, 0.1, "INPUT"));
            model.Layers.Add(new NeuralLayer(2, 0.1, "HIDDEN"));
            model.Layers.Add(new NeuralLayer(1, 0.1, "OUTPUT"));

            model.Build();
            model.Print();


            //create a 8 layer network
            Console.WriteLine("\n\n_____________8 layer network_________________");
            NetworkModel eightLayerNet = new NetworkModel();
            eightLayerNet.Layers.Add(new NeuralLayer(2, 0.1, "INPUT"));
            eightLayerNet.Layers.Add(new NeuralLayer(2, 0.1, "HIDDEN"));
            eightLayerNet.Layers.Add(new NeuralLayer(2, 0.1, "HIDDEN"));
            eightLayerNet.Layers.Add(new NeuralLayer(2, 0.1, "HIDDEN"));
            eightLayerNet.Layers.Add(new NeuralLayer(2, 0.1, "HIDDEN"));
            eightLayerNet.Layers.Add(new NeuralLayer(2, 0.1, "HIDDEN"));
            eightLayerNet.Layers.Add(new NeuralLayer(2, 0.1, "HIDDEN"));
            eightLayerNet.Layers.Add(new NeuralLayer(1, 0.1, "OUTPUT"));

            eightLayerNet.Build();
            eightLayerNet.Print();

            Console.ReadLine();
            
        }
    }
}

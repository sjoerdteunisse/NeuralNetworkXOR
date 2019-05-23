//
//Code for Research paper on neural networks.
//For questions contact: Sjoerdteunisse at google mail dot com
//

using System;
using NeuralEngine;

namespace NeuralNetExplained
{
    class Program
    {
        static void Main(string[] args)
        {
            //Input
            double[][] xorIn =
            {
                new double[] {0, 0},
                new double[] {0, 1},
                new double[] {1, 0},
                new double[] {1, 1}
            };

            //Desired
            double[][] xorDesired =
            {
                new double[] {0, 0},
                new double[] {1, 0},
                new double[] {1, 0},
                new double[] {0, 0}
            };

            //Explanation
            Console.WriteLine("Code for Research paper on neural networks.\n");
            Console.WriteLine("The XOr, or “exclusive or”, " +
                              "problem is a classic problem in ANN research. \nIt is the problem of using a neural network to predict the " +
                              "outputs of XOr logic gates given two binary inputs.\nAn XOr function should return a true value if the two " +
                              "inputs are not equal and a false value if they are equal. \n");


            //Creates the neural network
            var neuralNetwork = new NeuralNetwork(2, 4, 2, 0.25);
            //Runs the neural network to train 
            neuralNetwork.Run(xorIn, xorDesired);
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadLine();
        }
    }
}

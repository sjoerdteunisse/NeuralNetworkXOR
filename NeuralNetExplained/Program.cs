using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralCore;

namespace NeuralNetExplained
{
    class Program
    {
        static void Main(string[] args)
        {
            XORNeuralNetwork neuralNetwork = new XORNeuralNetwork();
            neuralNetwork.Run();
            Console.ReadLine();
        }
    }
}

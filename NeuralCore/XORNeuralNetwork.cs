using System;

namespace NeuralCore
{
    public class XORNeuralNetwork : NeuralNetwork
    {

        public XORNeuralNetwork() :
            base(10, 2, 0.1)
        {
            

        }


        public void Run()
        {
            NetworkModel model = new NetworkModel();
            model.Layers.Add(new NeuralLayer(2, 0.1, "INPUT"));
            model.Layers.Add(new NeuralLayer(1, 0.1, "OUTPUT"));

            model.Build();
            Console.WriteLine("----Before Training------------");
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

            model.Train(x, y, iterations: 10, learningRate: 0.1);
            Console.WriteLine();
            Console.WriteLine("----After Training------------");
            model.Print();
        }

        

    }
}

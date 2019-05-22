//
//Code for Research paper on neural networks.
//For questions contact: Sjoerdteunisse at google mail dot com
//
using System;
using System.Data;
using System.Linq;
using ConsoleTableExt;
using System.Collections.Generic;

namespace NeuralCore
{
    public class NetworkModel
    {
        public List<NeuralLayer> Layers { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public NetworkModel()
        {
            Layers = new List<NeuralLayer>();
        }

        /// <summary>
        /// Adds a neural layer to the existing NetworkModel
        /// </summary>
        /// <param name="layer">Layer to eb added</param>
        public void AddLayer(NeuralLayer layer)
        {
            int dendriteCount = 1;

            if (Layers.Count > 0)
            {
                dendriteCount = Layers.Last().Neurons.Count;
            }

            foreach (var element in layer.Neurons)
            {
                for (int i = 0; i < dendriteCount; i++)
                {
                    element.Dendrites.Add(new Dendrite());
                }
            }
        }
        
        /// <summary>
        /// Build network
        /// </summary>
        public void Build()
        {
            var i = 0;
            foreach (var layer in Layers)
            {
                if (i >= Layers.Count - 1)
                {
                    break;
                }

                var nextLayer = Layers[i + 1];
                CreateNetwork(layer, nextLayer);
                i++;
            }
        }

        /// <summary>
        /// Trains the neural network as following
        ///
        /// 1. Loops through training data
        /// 2. Input data in 1st layer
        /// 3. Forward pulse across layer to get the designated output
        /// 4. Measure output and compare to expected result
        /// 5. Optimize weights to achieve better results
        /// 
        /// </summary>
        /// <param name="xNeural">height data</param>
        /// <param name="yNeural">width data</param>
        /// <param name="runItteration">times to perform</param>
        /// <param name="learningRate">rate of learning</param>
        public void Train(NeuralData xNeural, NeuralData yNeural, int runItteration, double learningRate = 0.1)
        {
            var epoch = 1;

            //Loop till the number of runItteration
            while (runItteration >= epoch)
            {
                //Get the input layers
                var inputLayer = Layers[0];
                var outputs = new List<double>();

                //Loop through the record
                foreach (var t in xNeural.Data)
                {
                    //Set the input data into the first layer
                    for (var j = 0; j < t.Length; j++)
                    {
                        inputLayer.Neurons[j].OutputPulse.Value = t[j];
                    }

                    //Fire all the neurons and collect the output
                    ComputeOutput();

                    outputs.Add(Layers.Last().Neurons.First().OutputPulse.Value);
                }

                //Check the accuracy score against yNeural with the actual output
                double accuracySum = 0;
                var yCounter = 0;
                outputs.ForEach((x) =>
                {
                    if (x == yNeural.Data[yCounter].First())
                    {
                        accuracySum++;
                    }

                    yCounter++;
                });

                //Optimize the synaptic weights
                OptimizeWeights(accuracySum / yCounter);
                Console.WriteLine("Epoch: {0}, Accuracy: {1} %", 
                    epoch, (accuracySum / yCounter) * 100);
                epoch++;
            }
        }

        /// <summary>
        /// Print the current model with use of DataTable
        /// REFERENCE from nugget required (consoletableext).
        /// </summary>
        public void Print()
        {
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Neurons");
            dt.Columns.Add("Weight");

            foreach (var element in Layers)
            {
                var row = dt.NewRow();
                row[0] = element.Name;
                row[1] = element.Neurons.Count;
                row[2] = element.Weight;

                dt.Rows.Add(row);
            }

            var builder = ConsoleTableBuilder.From(dt);
            builder.ExportAndWrite();
        }

        private void ComputeOutput()
        {
            var first = true;
            foreach (var layer in Layers)
            {
                //First layer is input, so skip.
                if (first)
                {
                    first = false;
                    continue;
                }

                layer.Forward();
            }
        }

        private void OptimizeWeights(double accuracy)
        {
            var learningRate = 1f;

            //Skip if the accuracy reached 100%
            if (accuracy == 1)
            {
                return;
            }

            if (accuracy > 1)
            {
                learningRate = -learningRate;
            }

            //Update the weights for all the layers
            foreach (var layer in Layers)
            {
                layer.Optimize(learningRate, 1);
            }
        }

        /// <summary>
        /// Creates a network based on the connections
        /// </summary>
        /// <param name="connectingFrom">neuralLayerFrom</param>
        /// <param name="connectingTo">neuralLayerTo</param>
        private void CreateNetwork(NeuralLayer connectingFrom, NeuralLayer connectingTo)
        {
            foreach (var from in connectingFrom.Neurons)
            {
                from.Dendrites = new List<Dendrite> {new Dendrite()};
            }

            foreach (var to in connectingTo.Neurons)
            {
                to.Dendrites = new List<Dendrite>();
                foreach (var from in connectingFrom.Neurons)
                {
                    to.Dendrites.Add(new Dendrite() { InputPulse = from.OutputPulse, SynapseWeight = connectingTo.Weight });
                }
            }
        }
    }
}
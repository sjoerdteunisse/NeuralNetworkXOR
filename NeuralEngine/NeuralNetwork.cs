//
//Code for Research paper on neural networks.
//For questions contact: Sjoerdteunisse at google mail dot com
//

using System;
using System.Text;

namespace NeuralEngine
{
    /// <summary>
    /// Represents a Neural Network. Initial network is setup with random weights
    /// Back-prop supported
    /// </summary>
    public class NeuralNetwork
    {
        private double _learningRate;
        private Random _random;
        
        //Local for now
        private readonly double[][] _xorIn =
        {
            new double[] { 0, 0 },
            new double[] { 0, 1 },
            new double[] { 1, 0 },
            new double[] { 1, 1 }
        };

        private readonly double[][] _xorDesired =
        {
            new double[] {0, 0},
            new double[] {1, 0},
            new double[] {1, 0},
            new double[] {0, 0}
        };


        public NeuralLayer InputLayer { get; set; }
        public NeuralLayer HiddenLayer { get; set; }
        public NeuralLayer OutputLayer { get; set; }

        /// <summary>
        /// Creates a neural network, based on options
        /// </summary>
        /// <param name="inputNeuronCount">Number of input neurons</param>
        /// <param name="hiddenNeuronCount">Number of hidden neurons</param>
        /// <param name="outputNeuronCount">Number of output neurons</param>
        /// <param name="learningRate">Learning rate</param>
        /// <param name="randomSeed">seed, optional</param>
        public NeuralNetwork(int inputNeuronCount, int hiddenNeuronCount, int outputNeuronCount, double learningRate, int? randomSeed = null)
        {
            _random = randomSeed.HasValue ? new Random(randomSeed.Value) : new Random();
            _learningRate = learningRate;

            InputLayer = new NeuralLayer();
            HiddenLayer = new NeuralLayer();
            OutputLayer = new NeuralLayer();

            //Create neurons for each layer
            for (var i = 0; i < inputNeuronCount; i++)
                InputLayer.Add(new Neuron(0)); //Input neurons do not have a bias set to 0.

            for (var i = 0; i < hiddenNeuronCount; i++)
                HiddenLayer.Add(new Neuron(_random.NextDouble())); // Random bias to be improved in learning

            for (var i = 0; i < outputNeuronCount; i++)
                OutputLayer.Add(new Neuron(_random.NextDouble())); // Random bias to be improved in learning

            //Connect input layer to hidden layer
            for (var i = 0; i < hiddenNeuronCount; i++)
                for (var j = 0; j < inputNeuronCount; j++)
                    HiddenLayer[i].InputSignals.Add(InputLayer[j].OutputSignal, new NeuralFactor(CalcInitWeight(inputNeuronCount)));

            //Connect hidden layer to output layer
            for (var i = 0; i < outputNeuronCount; i++)
                for (var j = 0; j < hiddenNeuronCount; j++)
                    OutputLayer[i].InputSignals.Add(HiddenLayer[j].OutputSignal, new NeuralFactor(CalcInitWeight(hiddenNeuronCount)));
        }

        /// <summary>
        /// Applies learning to every Layer in network
        /// </summary>
        public void ApplyLearning()
        {
            HiddenLayer.ApplyLearning();
            OutputLayer.ApplyLearning();
        }

        /// <summary>
        /// Pulses each layer in the network
        /// </summary>
        public void Pulse()
        {
            HiddenLayer.Pulse();
            OutputLayer.Pulse();
        }

        /// <summary>
        /// Train network by In and Desired results
        /// </summary>
        /// <param name="inputs">Inputs</param>
        /// <param name="desiredResults">Desired output</param>
        public void Train(double[][] inputs, double[][] desiredResults)
        {
            for (var i = 0; i < inputs.Length; i++)
                Train(inputs[i], desiredResults[i]);
        }

        /// <summary>
        ///  Picks a random weight for a input signal based on Input of neurons
        /// 1-1--/sqrt(c)
        /// where c = neuron
        /// </summary>
        /// <param name="inputNeuronCount"></param>
        /// <returns></returns>
        private double CalcInitWeight(int inputNeuronCount)
        {
            var initialWeight = _random.NextDouble() / Math.Sqrt(inputNeuronCount);
            initialWeight = (initialWeight * 2) - initialWeight;
            return initialWeight;
        }

        /// <summary>
        /// Apply back propagation to the network
        /// </summary>
        /// <param name="desiredResults"></param>
        private void BackPropagation(double[] desiredResults)
        {
            if (desiredResults == null)
                throw new ArgumentNullException(nameof(desiredResults));

            //Output error rate
            for (var i = 0; i < OutputLayer.Count; i++)
            {
                var temp = OutputLayer[i].OutputSignal.Output;
                OutputLayer[i].Error = (desiredResults[i] - temp) * temp * (1.0 - temp);
            }

            //Hidden layer error rate
            foreach (var hiddenNode in HiddenLayer)
            {
                double error = 0;

                foreach (var outputNode in OutputLayer)
                {
                    error += outputNode.Error * outputNode.InputSignals[hiddenNode.OutputSignal].Weight * hiddenNode.OutputSignal.Output * (1.0 - hiddenNode.OutputSignal.Output);
                }

                hiddenNode.Error = error;
            }

            //Adjust output layer weights
            foreach (var outputNode in OutputLayer)
            {
                foreach (var hiddenNode in HiddenLayer)
                    outputNode.InputSignals[hiddenNode.OutputSignal].Delta += _learningRate * outputNode.Error * hiddenNode.OutputSignal.Output;

                outputNode.Bias.Delta += _learningRate * outputNode.Error * outputNode.Bias.Weight;
            }

            //Adjust hidden layer weights
            foreach (var hiddenNode in HiddenLayer)
            {
                foreach (var inputNode in InputLayer)
                    hiddenNode.InputSignals[inputNode.OutputSignal].Delta += _learningRate * hiddenNode.Error * inputNode.OutputSignal.Output;

                hiddenNode.Bias.Delta += _learningRate * hiddenNode.Error * hiddenNode.Bias.Weight;
            }
        }

        /// <summary>
        /// Runs the neural network
        /// </summary>
        /// <param name="input">input to train</param>
        /// <param name="desiredResult">desired result</param>
        /// <param name="maxXThousands">amount * 1000</param>
        public void Run(double[][] input, double[][] desiredResult, long? maxXThousands = null)
        {
            bool done;
            long count = 0;
            const int iterations = 10000;

            do
            {
                count++; //count * 100 = iteration

                //Each run the train method will run through all inputs and outpus
                for (var i = 0; i < iterations; i++)
                {
                    Train(input, desiredResult);
                }

                //Check if accuracy is achieved
                done = CheckResults(out var values);

                //Write for debug.
                Console.WriteLine("Neural Network XOR results: " + values);

                //As Ref XOR states: the following is correct;
                //string comparison as builder represents a hard built string anyway.
                if (values.Equals("(0,0=>0,0), (0,1=>1,0), (1,0=>1,0), (1,1=>0,0)"))
                {
                    //if correct print success
                    Console.WriteLine("Training success");
                }

            } while (!done && (!maxXThousands.HasValue || count * 100 < maxXThousands * 1000));
        }

        /// <summary>
        /// Train the net using single set of input array and output
        /// </summary>
        /// <param name="input">input to train</param>
        /// <param name="desiredResult">desired result</param>
        private void Train(double[] input, double[] desiredResult)
        {
            if (input.Length != InputLayer.Count)
                throw new ArgumentException($"Expected {InputLayer.Count} inputs");
            if (desiredResult.Length != OutputLayer.Count)
                throw new ArgumentException($"Expected {OutputLayer.Count} outputs");
            if (input.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(input));

            //Set input nodes
            for (var i = 0; i < InputLayer.Count; i++)
            {
                var neuron = InputLayer[i];
                neuron.OutputSignal.Output = input[i];
            }

            //Pule network
            Pulse();
            
            //Back-prop desired results
            BackPropagation(desiredResult);

            //Call Apply
            ApplyLearning();
        }

        /// <summary>
        /// Constructs a comparison message X and Y value of input and output
        /// </summary>
        /// <param name="message">out ref of message</param>
        /// <returns></returns>
        private bool CheckResults(out string message)
        {
            //Assume success, if not accurate change to false.
            var success = true;
            var thisMessage = new StringBuilder();

            //For each training set of data
            for (var i = 0; i < _xorIn.Length; i++)
            {
                if (i > 0)//separate with comma
                    thisMessage.Append(", ");

                thisMessage.Append("(");

                //Set inputs, and add the inputs to the message
                for (var j = 0; j < InputLayer.Count; j++)
                {
                    InputLayer[j].OutputSignal.Output = _xorIn[i][j];

                    if (j != 0) //next val comma separate
                        thisMessage.Append(",");

                    thisMessage.Append(_xorIn[i][j]);

                }

                //Pulse neurons
                Pulse();
                thisMessage.Append("=>");

                //Check outputs, and add the outputs to the message
                for (var j = 0; j < OutputLayer.Count; j++)
                {
                    var outputSignalOutput = OutputLayer[j].OutputSignal.Output;
                    var desired = _xorDesired[i][j];

                    if (Math.Abs(outputSignalOutput - desired) > 0.1)
                        success = false;

                    if (j != 0) //next val comma separate
                        thisMessage.Append(",");

                    thisMessage.Append(_xorDesired[i][j]);
                }
                thisMessage.Append(")");
            }

            message = thisMessage.ToString();
            return success;
        }
    }
}


//
//Code for Research paper on neural networks.
//For questions contact: Sjoerdteunisse at google mail dot com
//
namespace NeuralCore
{
    public class NeuralNetwork
    {
        private NetworkModel networkModel;

        public NeuralNetwork()
        {
            networkModel = new NetworkModel();
            networkModel.Layers.Add(new NeuralLayer(2, 0.1, "INPUT"));
            networkModel.Layers.Add(new NeuralLayer(1, 0.1, "OUTPUT"));
            networkModel.Build();
        }

        public NeuralNetwork(NetworkModel networkModel)
        {
            networkModel.Build();
        }

        public NetworkModel NeuralNetworkModel => networkModel;
    }
}

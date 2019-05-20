namespace NeuralCore
{
    public class NeuralNetwork
    {
        private NetworkModel networkModel;

        public NeuralNetwork(int layers, int count, double initialWeigh)
        {

            networkModel = new NetworkModel();
            networkModel.Layers.Add(new NeuralLayer(2, 0.1, "INPUT"));
            networkModel.Layers.Add(new NeuralLayer(1, 0.1, "OUTPUT"));

            networkModel.Build();
        }


        public NetworkModel NeuralNetworkModel => networkModel;
    }
}

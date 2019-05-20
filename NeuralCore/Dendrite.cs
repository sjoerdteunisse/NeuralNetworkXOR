namespace NeuralCore
{
    public class Dendrite
    {
        /// <summary>
        /// Initializes a dendrite with a Pulse 
        /// </summary>
        public Dendrite()
        {
            InputPulse = new Pulse();
        }

        public Pulse InputPulse { get; set; }

        public double SynapseWeight { get; set; }

        public bool LearnAble { get; set; } = true;
    }
}
//
//Code for Research paper on neural networks.
//For questions contact: Sjoerdteunisse at google mail dot com
//

namespace NeuralCore
{
    public class NeuralData
    {
        public double[][] Data { get; set; }

        private int dataCounter = 0;

        /// <summary>
        /// Construct NeuralData
        /// </summary>
        /// <param name="rows"></param>
        public NeuralData(int rows)
        {
            //Create a two dimensional array with Rows
            Data = new double[rows][];
        }

        /// <summary>
        /// Add to data
        /// </summary>
        /// <param name="rec"></param>
        public void Add(params double[] rec)
        {
            Data[dataCounter] = rec;
            dataCounter++;
        }
    }
}
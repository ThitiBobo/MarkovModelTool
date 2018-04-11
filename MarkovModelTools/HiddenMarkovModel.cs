
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovModelTools
{
    public class HiddenMarkovModel : MarkovChain
    {
        private List<MarkovMatrix> _emissionMatrix;

        public List<MarkovMatrix> EmissionMatrix
        {
            get { return new List<MarkovMatrix>(_emissionMatrix); }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                foreach (MarkovMatrix e in value)
                {
                    if (e.Row != _size)
                        throw new ArgumentException();
                }
                _emissionMatrix = value;
            }
        }

        public HiddenMarkovModel(uint size, MarkovMatrix matrix, List<MarkovMatrix> emissionMatrix) : 
            base(size,matrix)
        {
            EmissionMatrix = emissionMatrix;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("HiddenMarkovModel (" + _size + "," + _size + ")");
            result.AppendLine("Etat de départ: " + StartState);
            result.AppendLine("Etat actuel: " + CurrentState);
            result.AppendLine(GetStartProbToString());
            result.AppendLine(GetTransitionMatrixToString());
            foreach (MarkovMatrix e in _emissionMatrix)
            {
                result.AppendLine(e.GetMatrixToString());
            }

            return result.ToString();
        }
    }
}

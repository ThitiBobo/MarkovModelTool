/*
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

        public HiddenMarkovModel(
            uint size,
            double[,] matrix,
            Dictionary<int, string> states, 
            string startState, 
            List<MarkovMatrix> emissionMatrix
            ) : base(
                size,
                matrix,
                states,
                startState)
        {
            EmissionMatrix = emissionMatrix;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("MarkovModel (" + _row + "," + _col + ")");
            result.AppendLine("Etat de départ: " + StartState);
            result.AppendLine("Etat actuel: " + CurrentState);
            for (int i = 0; i < _row; i++)
            {
                result.Append(_colStates[i] + " [ ");
                for (int j = 0; j < _col; j++)
                {
                    result.AppendFormat("{0,10}", _matrix[i, j].ToString("0.000000"));
                }
                result.AppendLine("]");
            }
            foreach (MarkovMatrix e in _emissionMatrix)
            {
                result.AppendLine(e.ToString());
            }
            return result.ToString();
        }
    }
}
*/

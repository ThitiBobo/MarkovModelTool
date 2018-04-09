using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovModelTools
{
    public class HiddenMarkovModel : MarkovChain
    {
        private IList<MarkovMatrix> _emissionMatrix;

        public HiddenMarkovModel():
            base(5)
        {
                
        }
    }
}

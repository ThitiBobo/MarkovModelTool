using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovModelTools
{
    public class HiddenMarkovModel : MarkovModel
    {
        private IList<MarkovMatrix> _emissionMatrix;
    }
}

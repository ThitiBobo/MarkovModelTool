using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovModelTools
{
    public class MarkovModel : MarkovMatrix
    {
        #region MENBERS
        private uint _size;
        #endregion

        #region CONTRUCTORS
        public MarkovModel(uint size, double[,] matrix, Dictionary<int, string> states):
            base(size, size, matrix, states)
        {
            _size = size;
        }

        public MarkovModel(uint size,  Dictionary<int, string> states):
            base(size, size, states)
        {
            _size = size;
        }

        public MarkovModel(uint size):
            base(size,size)
        {
            _size = size;
        }
        #endregion


    }
}

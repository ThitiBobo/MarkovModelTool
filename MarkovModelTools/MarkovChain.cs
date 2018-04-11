/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovModelTools
{
    public class MarkovChain : MarkovMatrix
    {
        #region MENBERS
        /// <summary>
        /// dimension de la matrice (matrice carrée)
        /// </summary>
        protected uint _size;
        /// <summary>
        /// état actulle du model;
        /// </summary>
        protected uint _currentState;
        /// <summary>
        /// état de départ du model
        /// </summary>
        protected uint _startState;
        #endregion
        
        #region GETSET
        public string CurrentState {
            get { return _states.FirstOrDefault(x => x.Key == _currentState).Value; }
            set {
                if (!_states.ContainsValue(value))
                    throw new ArgumentOutOfRangeException();
                _currentState = (uint)_states.FirstOrDefault(x => x.Value == value).Key;
            }
                
        }

        public string StartState
        {
            get { return _states.FirstOrDefault(x => x.Key == _startState).Value; }
            private set
            {
                if (!_states.ContainsValue(value))
                    throw new ArgumentOutOfRangeException();
                _startState = (uint)_states.FirstOrDefault(x => x.Value == value).Key;
            }

        }
        #endregion
        #region CONTRUCTORS
        public MarkovChain(uint size, double[,] matrix, Dictionary<int, string> states, string startState):
            base(size, size, matrix, states)
        {
            StartState = startState;
            CurrentState = startState;
            _size = size;
        }

        public MarkovChain(uint size,  Dictionary<int, string> states, string startState):
            base(size, size, states)
        {
            StartState = startState;
            CurrentState = startState;
            _size = size;
        }

        public MarkovChain(uint size, double[,] matrix, string startState) :
            base(size, size, matrix)
        {
            StartState = startState;
            CurrentState = startState;
            _size = size;
        }

        public MarkovChain(uint size):
            base(size,size)
        {
            _size = size;
        }
        #endregion

        /// <summary>
        /// retourne l'instance sous forme de string 
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("MarkovModel (" + _row + "," + _col + ")");
            result.AppendLine("Etat de départ: " + StartState);
            result.AppendLine("Etat actuel: " + CurrentState);
            for (int i = 0; i < _row; i++)
            {
                result.Append(_states[i] + " [ ");
                for (int j = 0; j < _col; j++)
                {
                    result.AppendFormat("{0,10}", _matrix[i, j].ToString("0.000000"));
                }
                result.AppendLine("]");
            }
            return result.ToString();
        }

        public string NextState()
        {
            CurrentState = base.NextState((int)_currentState);
            return CurrentState;
        }

    }
}
*/

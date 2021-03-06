﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovModelTools
{
    public class MarkovChain
    {
        #region MENBERS
        /// <summary>
        /// dimension de la matrice (matrice carrée)
        /// </summary>
        protected uint _size;
        /// <summary>
        /// matrice d'émission
        /// </summary>
        protected MarkovMatrix _transitionMatrix;
        /// <summary>
        /// probabilité de départ
        /// </summary>
        protected MarkovMatrix _startProb;
        /// <summary>
        /// état actulle du model;
        /// </summary>
        protected uint _currentState;
        /// <summary>
        /// état de départ choisie;
        /// </summary>
        protected uint _startState;
        #endregion
        
        #region GETSET
        /// <summary>
        /// modifie / obtient l'état courrant du model
        /// </summary>
        public string CurrentState {
            get { return _transitionMatrix.RowStates.FirstOrDefault(x => x.Key == _currentState).Value; } 
            private set { _currentState = (uint)_transitionMatrix.RowStates.FirstOrDefault(x => x.Value == value).Key; }               
        }

        public string StartState
        {
            get { return _startProb.ColStates.FirstOrDefault(x => x.Key == _startState).Value ; }
            private set { _startState = (uint)_transitionMatrix.RowStates.FirstOrDefault(x => x.Value == value).Key; }
        }

        public int Size
        {
            get { return (int)_size; }
        }

        public MarkovMatrix TransitionMatrix
        {
            get { return new MarkovMatrix(_transitionMatrix); }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                if (value.Col != _size || value.Row != _size)
                    throw new ArgumentException();
                _transitionMatrix = value;
            }
        }

        public MarkovMatrix StartProb
        {
            get { return new MarkovMatrix(_startProb); }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                if (value.Col != _transitionMatrix.Row)
                    throw new ArgumentException();
                if (!(value.Row == 1))
                    throw new ArgumentException();
                _startProb = value;
            }
        }

        #endregion

        #region CONTRUCTORS
        public MarkovChain(uint size, MarkovMatrix matrix, MarkovMatrix startProb)
        {
            _size = size;
            TransitionMatrix = matrix;
            StartProb = startProb;
            InitStartState();
        }


        public MarkovChain(uint size, MarkovMatrix matrix) :
            this(size, matrix, new MarkovMatrix(1,size))
        { }

        public MarkovChain(uint size) : 
            this(size, new MarkovMatrix(size,size), new MarkovMatrix(1,size))
        { }

        public MarkovChain(MarkovChain markovChain)
        {
            if (markovChain == null)
                throw new ArgumentNullException();
            _size = (uint)markovChain.Size;
            TransitionMatrix = markovChain.TransitionMatrix;
            StartProb = markovChain.StartProb;
            StartState = markovChain.StartState;
            CurrentState = markovChain.CurrentState;
        }
        #endregion

        /// <summary>
        /// retourne l'instance sous forme de string 
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("MarkovChain (" + _size + "," + _size + ")");
            result.AppendLine("Etat de départ: " + StartState);
            result.AppendLine("Etat actuel: " + CurrentState);
            result.AppendLine(GetStartProbToString());
            result.AppendLine(GetTransitionMatrixToString());
            return result.ToString();
        }

        public string GetStartProbToString()
        {
            return _startProb.GetMatrixToString();
        }

        public string GetTransitionMatrixToString()
        {
            return _transitionMatrix.GetMatrixToString();
        }

        public string NextState()
        {
            CurrentState = _transitionMatrix.NextState((int)_currentState);
            return CurrentState;
        }
        
        public string InitStartState()
        {
            _startState =  (uint)_startProb.ColStates.FirstOrDefault(
                x => x.Value == _startProb.NextState(0))
                .Key;
            _currentState = _startState;
            return StartState;
        }        

    }
}


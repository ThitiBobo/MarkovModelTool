using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace MarkovModelTools
{
    public class MarkovMatrix
    {
        #region ATTRIBUTS
        /// <summary>
        /// nombre de lignes de la matrice
        /// </summary>
        private uint _row;
        /// <summary>
        /// nombre de colonnes de la matrice
        /// </summary>
        private uint _col;
        /// <summary>
        /// matrice de dimension (row, col) ou chaque valeurs est stokée 
        /// à l'emplacement (i,j) dans la matrice 
        /// </summary>
        private double[,] _matrix;
        /// <summary>
        /// collection contenant pour chaques états, la ligne correspondant dans la mtrice et son nom  
        /// </summary>
        private Dictionary<int,string> _states;
        #endregion

        #region GETSET
        public double[,] Matrix {
            get { return (double[,])_matrix.Clone(); }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                if (value.Length != _col * _row)
                    throw new ArgumentException();
                if (!CheckElements(value))
                    throw new ArgumentException();
                if (!CheckRows(value))
                    throw new ArgumentException();
                _matrix = value;
            }
        }
        public Dictionary<int,string> States {
            get { return new Dictionary<int, string>(_states); }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                if (value.Count != _col )
                    throw new ArgumentException();
                _states = value;
            }
        }
        #endregion

        #region CONTRUCTORS
        public MarkovMatrix(uint row, uint col, double[,] matrix, Dictionary<int,string> states)
        {
            _row = row;
            _col = col;
            Matrix = matrix;
            States = states;
        }

        public MarkovMatrix(uint row, uint col, Dictionary<int,string> states) :
            this(row, col, EmptyMatrix(row, col), states)
        {}

        public MarkovMatrix(uint row, uint col) : 
            this(row, col, EmptyMatrix(row,col), EmptyStates(col))
        {}
        #endregion

        public override String ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("MarkovMatrix (" + _row  + "," + _col + ")");
            for (int i = 0; i < _row; i++)
            {
                result.Append(_states[i] + " [ ");
                for (int j = 0; j < _col; j++)
                {
                    result.Append(_matrix[i, j] + " ");
                }
                result.AppendLine("]");
            }
            return result.ToString();
        }

        /// <summary>
        /// méthode qui génére une matrice ou la somme de chaque élèments d'une ligne
        /// vaut 1
        /// </summary>
        /// <param name="row">nombre de lignes de la matrice</param>
        /// <param name="col">nombre de colonnes de la matrice</param>
        /// <returns>retuourne une matrice de taille (row, col)</returns>

        #region STATIC METHODES
        public static double[,] EmptyMatrix(uint row, uint col)
        {
            double[,] matrix = new double[row, col];
            if (row * col != 0)
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        matrix[i, j] = 1 / (double)col;
                    }
                }
            }
            return matrix;
        }

        public static Dictionary<int,string> EmptyStates(uint col)
        {
            Dictionary<int,string> states = new Dictionary<int, string>();
            for (int i = 0; i < col; i++)
                states.Add(i, Convert.ToChar(65 + i).ToString());
            return states;
        }

        public static Boolean CheckElements(double[,] matrix)
        {
            System.Collections.IEnumerator enumerator = matrix.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if ((double)enumerator.Current < 0 || (double)enumerator.Current > 1)
                    return false;
            }
            return true;
        }

        public static Boolean CheckRows(double[,] matrix)
        {
            float result;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                result = 0;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    result += (float)matrix[i, j];

                }
                if ((int)result != 1)
                    return false;
            }
            return true;
            
        }
        #endregion

    }
}

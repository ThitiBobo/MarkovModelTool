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
        /// <summary>
        /// obtient une copie de la matrice
        /// </summary>
        public double[,] Matrix {
            get { return (double[,])_matrix.Clone(); }
            private set
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
        /// <summary>
        /// obtient une copie du dictionnaire des etats
        /// </summary>
        public Dictionary<int,string> States {
            get { return new Dictionary<int, string>(_states); }
            private set
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

        /// <summary>
        /// Initialise une nouvelle instance de la class MarkovMatrix avec une matrice de taille (row, col)
        /// </summary>
        /// <param name="row">nombre de lignes</param>
        /// <param name="col">nombre de colonnes</param>
        /// <param name="matrix">matrice</param>
        /// <param name="states">nom des états</param>
        public MarkovMatrix(uint row, uint col, double[,] matrix, Dictionary<int,string> states)
        {
            _row = row;
            _col = col;
            Matrix = matrix;
            States = states;
        }
        /// <summary>
        /// Initialise une nouvelle instance de la class MarkovMatrix avec une matrice de taille (row, col)
        /// </summary>
        /// <param name="row">nombre de lignes</param>
        /// <param name="col">nombre de colonnes</param>
        /// <param name="states">nom des états</param>
        public MarkovMatrix(uint row, uint col, Dictionary<int,string> states) :
            this(row, col, EmptyMatrix(row, col), states)
        {}
        /// <summary>
        /// Initialise une nouvelle instance de la class MarkovMatrix avec une matrice de taille (row, col)
        /// </summary>
        /// <param name="row">nombre de lignes</param>
        /// <param name="col">nombre de colonnes</param>
        public MarkovMatrix(uint row, uint col) : 
            this(row, col, EmptyMatrix(row,col), EmptyStates(col))
        {}
        #endregion

        /// <summary>
        /// retourne l'instance sous forme de string 
        /// </summary>
        /// <returns>string</returns>
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



        #region STATIC METHODES
        /// <summary>
        /// méthode qui génére une matrice ou la somme de chaque élèments d'une ligne
        /// vaut 1
        /// </summary>
        /// <param name="row">nombre de lignes de la matrice</param>
        /// <param name="col">nombre de colonnes de la matrice</param>
        /// <returns>retuourne une matrice de taille (row, col)</returns>
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
        /// <summary>
        /// renvoie une nouvelles instances Dictionary
        /// </summary>
        /// <param name="col">nombre d'élément du Dictionaire</param>
        /// <returns></returns>
        public static Dictionary<int,string> EmptyStates(uint size)
        {
            Dictionary<int,string> states = new Dictionary<int, string>();
            for (int i = 0; i < size; i++)
                states.Add(i, Convert.ToChar(65 + i).ToString());
            return states;
        }

        /// <summary>
        /// Vérifie si chaque éléments de la matrice sont compris entre 0 et 1
        /// </summary>
        /// <param name="matrix">matrice à vérifier</param>
        /// <returns>retourne true si la matrice respecte la règle sinon false</returns>
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
        /// <summary>
        /// Vérifie si la somme de chaque éléments d'une ligne de la matrice vaut 1
        /// </summary>
        /// <param name="matrix">matrice à vérifier</param>
        /// <returns>retourne true si les lignes de la matrice respectent la règle sinon false</returns>
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

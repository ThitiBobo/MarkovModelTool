using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace MarkovModelTools
{
    /// <summary>
    /// Représente une matrice de taille (n x m) de double respectant 
    /// 2 règles sur 3 des matrice stochastique (ou matrice de markov): 
    ///  - chaque élèments de la matrice est compris dans l'intervalle [0,1] 
    ///  - la somme de tout les éléments d'une ligne est égal à 1
    /// </summary>
    public class MarkovMatrix 
    {
        #region STATIC MENBERS
        /// <summary>
        /// générateur de nombre aléatoire
        /// </summary>
        private static Random RANDOM = new Random();
        #endregion

        #region MENBERS
        /// <summary>
        /// nombre de lignes de la matrice
        /// </summary>
        protected uint _row;
        /// <summary>
        /// nombre de colonnes de la matrice
        /// </summary>
        protected uint _col;
        /// <summary>
        /// matrice de dimension (row, col) ou chaque valeurs sont stokées
        /// à l'emplacement (i,j) dans la matrice 
        /// </summary>
        protected double[,] _matrix;
        /// <summary>
        /// collection contenant pour chaques états, la ligne correspondant dans la mtrice et son nom  
        /// </summary>
        protected Dictionary<int, string> _rowStates;
        /// <summary>
        /// collection contenant pour chaques états, la colonne correspondant dans la mtrice et son nom  
        /// </summary>
        protected Dictionary<int, string> _colStates;
        #endregion

        #region GETSET
        /// <summary>
        /// obtient une copie de la matrice
        /// </summary>
        public double[,] Matrix{
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
                    Console.WriteLine("marche pas");
                    //throw new ArgumentException();
                _matrix = value;
            }
        }
        /// <summary>
        /// obtient une copie du dictionnaire ligne, nom
        /// </summary>
        public Dictionary<int, string> RowStates {
            get { return new Dictionary<int, string>(_rowStates); }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException();
                if (value.Count != _row )
                    throw new ArgumentException();
                _rowStates = value;
            }
        }
        /// <summary>
        /// obtient une copie du dictionnaire colonne, nom
        /// </summary>
        public Dictionary<int, string> ColStates
        {
            get { return new Dictionary<int, string>(_colStates); }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException();
                if (value.Count != _col)
                    throw new ArgumentException();
                _colStates = value;
            }
        }

        /// <summary>
        /// Obtient le nombre de ligne de la matrice
        /// </summary>
        public uint Row {
            get { return _row; }
        }

        /// <summary>
        /// Obtient le nombre de colonne de la matrice
        /// </summary>
        public uint Col
        {
            get { return _col; }
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
        public MarkovMatrix(uint row, uint col, double[,] matrix, Dictionary<int,string> rowStates, Dictionary<int, string> colStates)
        {
            _row = row;
            _col = col;
            Matrix = matrix;
            RowStates = rowStates;
            ColStates = colStates;
        }
        /// <summary>
        /// Initialise une nouvelle instance de la class MarkovMatrix avec une matrice de taille (row, col)
        /// </summary>
        /// <param name="row">nombre de lignes</param>
        /// <param name="col">nombre de colonnes</param>
        /// <param name="matrix">matrice</param>
        public MarkovMatrix(uint row, uint col, double[,] matrix):
            this(row, col, matrix, EmptyStates(row), EmptyStates(col))
        {}
        /// <summary>
        /// Initialise une nouvelle instance de la class MarkovMatrix avec une matrice de taille (row, col)
        /// </summary>
        /// <param name="row">nombre de lignes</param>
        /// <param name="col">nombre de colonnes</param>
        /// <param name="states">nom des états</param>
        public MarkovMatrix(uint row, uint col, Dictionary<int, string> rowStates, Dictionary<int, string> colStates) :
            this(row, col, EmptyMatrix(row, col), rowStates, colStates)
        {}
        /// <summary>
        /// Initialise une nouvelle instance de la class MarkovMatrix avec une matrice de taille (row, col)
        /// </summary>
        /// <param name="row">nombre de lignes</param>
        /// <param name="col">nombre de colonnes</param>
        public MarkovMatrix(uint row, uint col) : 
            this(row, col, EmptyMatrix(row,col), EmptyStates(row), EmptyStates(col))
        {}

        public MarkovMatrix(MarkovMatrix matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException();
            _row = matrix.Row;
            _col = matrix.Col;
            Matrix = matrix.Matrix;
            RowStates = matrix.RowStates;
            ColStates = matrix.ColStates;

        }
        #endregion

        /// <summary>
        /// retourne l'instance sous forme de string 
        /// </summary>
        /// <returns>retourne une instance de string</returns>
        public override String ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("MarkovMatrix (" + _row  + "," + _col + ")");
            result.AppendLine(GetMatrixToString());
            return result.ToString();
        }

        /// <summary>
        /// retourne la matrice sous forme de chaine de caractère
        /// </summary>
        /// <returns>retourne une instance de string</returns>
        public string GetMatrixToString()
        {
            StringBuilder result = new StringBuilder();
            for (int j = 0; j < _col; j++)
            {
                result.AppendFormat("{0,7}", _colStates[j]);
            }
            result.AppendLine();
            for (int i = 0; i < _row; i++)
            {
                result.Append(_rowStates[i] + " [");
                for (int j = 0; j < _col; j++)
                {
                    result.AppendFormat("{0,7}", _matrix[i, j].ToString("0.000"));
                }
                result.AppendLine(" ]");
            }
            return result.ToString();
        }

        /// <summary>
        /// génére le prochain état à partir de l'état actuelle
        /// </summary>
        /// <param name="state">état de départ</param>
        /// <returns>retourne la valeur de l'état généré</returns>
        public string NextState(string state)
        {
            if (!_rowStates.ContainsValue(state))
                throw new ArgumentOutOfRangeException();
            int i = 0;
            double number = RANDOM.NextDouble();
            double intervalSup = _matrix[_rowStates.FirstOrDefault(x => x.Value == state).Key, i];
            while (intervalSup <= number) {
                i++;
                intervalSup += _matrix[_rowStates.FirstOrDefault(x => x.Value == state).Key, i];     
            }
            return _colStates.FirstOrDefault(x => x.Key == i).Value;
        }

        /// <summary>
        /// génére le prochain état à partir de l'état actuelle
        /// </summary>
        /// <param name="key">l'id de l'état de départ</param>
        /// <returns>retourne la valeur de l'état généré</returns>
        public string NextState(int key)
        {
            if (!_rowStates.ContainsKey(key))
                throw new ArgumentOutOfRangeException();
            int i = 0;
            double number = RANDOM.NextDouble();
            double intervalSup = _matrix[key, i];
            while (intervalSup <= number)
            {
                i++;
                intervalSup += _matrix[key, i];
            }
            return _colStates.FirstOrDefault(x => x.Key == i).Value;
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
            int colSelect = RANDOM.Next((int)col);
            double[,] matrix = new double[row, col];
            if (row * col != 0)
            {
                for (int i = 0; i < row; i++)
                {
                    //matrix[i, colSelect] += RAND.NextDouble();
                    for (int j = 0; j < col; j++)
                    {
                        matrix[i, j] = 1.0d / col;
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
            double result;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                result = 0;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    result += (double)matrix[i, j];
                }
                if (result != 1)
                    return false;
            }
            return true;
            
        }
        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MarkovModelTools;

namespace MarkovModelTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            double[,] tab = {
                {0.1,0.9,0},
                {0.5,0,0.5},
                {0,0.7,0.3}
            };

            Dictionary<int, string> state = new Dictionary<int, string>();
            state.Add(0, "A");
            state.Add(1, "B");
            state.Add(2, "C");


            MarkovModel cc = new MarkovModel(3, tab, state);
            Console.WriteLine(cc);
            string stateq = "A";
            for (int i = 0; i < 100; i++)
            {
                stateq = cc.NextState(stateq);
                Console.WriteLine(stateq);
            }
            Console.ReadLine();
        }
    }
}

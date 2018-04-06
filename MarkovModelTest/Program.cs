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
            Console.WriteLine(new MarkovMatrix(2, 2));

            
            string[] cc = { "g", "g", "g", "g", "g", "g" };
            Console.WriteLine(new MarkovMatrix(5, 5 ));
            Console.ReadLine();
        }
    }
}

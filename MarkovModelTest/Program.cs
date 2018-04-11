using System;
using System.Linq;
using RDotNet;

using MarkovModelTools;

namespace Sample1
{
    class Program
    {
        static void Main(string[] args)
        {

            MarkovMatrix mm = new MarkovMatrix(5, 6);
            Console.WriteLine(mm);
            Console.Read();
            
        }
    }
}
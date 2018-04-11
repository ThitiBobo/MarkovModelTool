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

            MarkovChain mm = new MarkovChain(5);
            Console.WriteLine(mm);
            Console.Read();
            
        }
    }
}